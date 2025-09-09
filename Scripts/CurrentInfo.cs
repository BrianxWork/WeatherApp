using System;
using UnityEngine;

public class CurrentInfo //to read JSON
{
	public Coord coord;
	public Weather[] weather;
	public Wind wind;
	public Main main;
	public Clouds clouds;
	public Sys sys;
	public string @base;
	public int visibility;
	public long dt;
	public int timezone;
	public int id;
	public string name;
	public int cod;


	public string sCompleteInfo;

	public string ToDisplayString()
	{
		// If nothing meaningful is filled in
		if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(sys.country) && string.IsNullOrEmpty(weather[0].main))//weather[0] only display the fist one
		{
			return "No weather data available.";
		}


		string weatherMain = weather[0].main;
		string weatherDesc = weather[0].description;
		string weatherIcon = weather[0].icon;


		string sCompleteInfo =
			$"City: {name}, Country: {sys?.country}\n" +
			$"Coordinates: Lon={coord?.lon}, Lat={coord?.lat}\n" +
			$"Weather: {weatherMain} ({weatherDesc}), Icon={weatherIcon}\n" +
			$"Temperature: {main?.temp}¢XC (Feels like {main?.feels_like}¢XC)\n" +
			$"Min/Max Temp: {main?.temp_min}¢XC / {main?.temp_max}¢XC\n" +
			$"Pressure: {main?.pressure} hPa, Humidity: {main?.humidity}%\n" +
			$"Sea Level: {main?.sea_level}, Ground Level: {main?.grnd_level}\n" +
			$"Visibility: {visibility} m\n" +
			$"Wind: {wind?.speed} m/s, Deg: {wind?.deg}, Gust: {wind?.gust}\n" +
			$"Clouds: {clouds?.all}%\n" +
			$"Timestamp: {dt}, Timezone Offset: {timezone}\n" +
			$"Sunrise: {sys?.sunrise}, Sunset: {sys?.sunset}\n" +
			$"ID: {id}, Code: {cod}";

		Debug.Log(sCompleteInfo);

		return sCompleteInfo;
	}
}

[Serializable]
public class Coord
{
	public float lon;
	public float lat;
}

[Serializable]
public class Weather
{
	public int id;
	public string main;
	public string description;
	public string icon;
}

[Serializable]
public class Main
{
	public float temp;
	public float feels_like;
	public float temp_min;
	public float temp_max;
	public int pressure;
	public int humidity;
	public int sea_level;
	public int grnd_level;
}

[Serializable]
public class Wind
{
	public float speed;
	public int deg;
	public float gust;
}

[Serializable]
public class Clouds
{
	public int all;
}

[Serializable]
public class Sys
{
	public string country;
	public long sunrise;
	public long sunset;
}


