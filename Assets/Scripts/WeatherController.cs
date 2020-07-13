using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;


/// <summary>
/// Call openweathermap's API to get the current weather
/// and modify the particle effects accordingly.
/// </summary>
public class WeatherController : MonoBehaviour
{
    [Tooltip("City where to get the weather")]
    public string cityId;
    [Tooltip("How much seconds between each API call")]
    public float apiCheckMaxtime = 5 * 60.0f; // 5 min
    [Tooltip("Gameobjects with particles on them, their name " +
        "should correspond to a weather name from openweathermap")]
    public List<GameObject> particleObjects;

    /// <summary>
    /// openweathermap API key
    /// </summary>
    private const string API_KEY = "bd07091b9aaf4a7bef85d383ca6b602a";
    /// <summary>
    /// Time remaining before next API call
    /// </summary>
    private float apiCheckCountdown;

    private void Awake()
    {
        apiCheckCountdown = apiCheckMaxtime;
    }

    void Start()
    {
        StartCoroutine(GetWeather(UpdateParticleEffect));
    }

    private void Update()
    {
        apiCheckCountdown -= Time.deltaTime;
        if(apiCheckCountdown <= 0)
        {
            apiCheckCountdown = apiCheckMaxtime;
            StartCoroutine(GetWeather(UpdateParticleEffect));
        }
    }

    /// <summary>
    /// Call openweathermap API to get weather info of the city specified with <see cref="cityId"/>.
    /// </summary>
    /// <returns>Current weather info according to openweathermap</returns>
    private IEnumerator GetWeather(Action<WeatherInfo> onSuccess)
    {
        using (UnityWebRequest req = UnityWebRequest.Get(
            string.Format("api.openweathermap.org/data/2.5/weather?q={0}&appid={1}",
                cityId, API_KEY)))
        {
            yield return req.SendWebRequest();
            while(!req.isDone)
            {
                yield return null;
            }
            byte[] result = req.downloadHandler.data;
            string weatherJSON = System.Text.Encoding.Default.GetString(result);
            WeatherInfo weatherInfo = JsonUtility.FromJson<WeatherInfo>(weatherJSON);
            onSuccess(weatherInfo);
        }
    }

    /// <summary>
    /// Update particle effects displayed based on the weather
    /// </summary>
    /// <param name="weatherInfo">weather info gotten with the openweathermap API call</param>
    public void UpdateParticleEffect(WeatherInfo weatherInfo)
    {
        if(weatherInfo.weather.Count < 1)
        {
            return;
        }
        string weatherName = weatherInfo.weather[0].main;
#if DEBUG
        Debug.Log("Weather : " + weatherName);
#endif
        foreach (GameObject particleObj in particleObjects)
        {
            if(particleObj.name == weatherName)
            {
                particleObj.SetActive(true);
                //Debug.Log("Activate " + particleObj.name);
            } else
            {
                particleObj.SetActive(false);
                //Debug.Log("Deactivate " + particleObj.name);
            }
        }
    }
}
