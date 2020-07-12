using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// Represent a weather object from the JSON response of openweathermap
/// </summary>
[Serializable]
public class Weather
{
    public int id;
    public string main;
}

/// <summary>
/// A class matching the JSON response from openweathermap
/// </summary>
[Serializable]
public class WeatherInfo
{
    public int id;
    public string name;
    public List<Weather> weather;
}
