using System;
using UnityEngine;

public class ForecastInfo
{
	public string cod;
	public int message;
	public int cnt;
	public ForecastItem[] list;
	public City city;

	public string ToDisplayString()
	{
		if (list == null || list.Length == 0)
			return "No forecast data available.";

		//  Add city info header
		string info = $"City: {city?.name}, {city?.country}\n" +
					  $"Coordinates: {city?.coord?.lat}, {city?.coord?.lon}\n" +
					  $"Population: {city?.population}\n" +
					  $"Timezone: {city?.timezone}\n" +
					  $"--------------------------------\n";

		info += $"Forecast Count: {cnt}\n";

		foreach (var item in list)
		{
			string weatherMain = item.weather != null && item.weather.Length > 0 ? item.weather[0].main : "N/A";
			string weatherDesc = item.weather != null && item.weather.Length > 0 ? item.weather[0].description : "N/A";

			//Getting local time of the forcast from UTC
			DateTime time = Utilities.UnixTimeStampToDateTime(item.dt);
			TimeSpan offset = TimeSpan.FromSeconds(city.timezone);
			DateTime datetime = time + offset;

			info +=
				$"Datetime: {datetime}\n" +
				$"  Temp: {item.main.temp}¢XC (Feels like {item.main.feels_like}¢XC)\n" +
				$"  Min/Max: {item.main.temp_min}¢XC / {item.main.temp_max}¢XC\n" +
				$"  Weather: {weatherMain} ({weatherDesc}) Icon:{item.weather[0].description}, Icon:{item.weather[0].icon}\n" +
				$"  Clouds: {item.clouds.all}% | Wind: {item.wind.speed} m/s @ {item.wind.deg}¢X\n" +
				$"  Humidity: {item.main.humidity}% | Pressure: {item.main.pressure} hPa\n" +
				$"  POP: {item.pop * 100}% | Visibility: {item.visibility} m\n" +
				$"--------------------------------\n";
		}

		Debug.Log(info);
		return info;
	}
}

[Serializable]
public class ForecastItem
{
	public long dt;
	public ForecastMain main;
	public Weather[] weather;
	public Clouds clouds;
	public Wind wind;
	public int visibility;
	public float pop;
	public ForecastRain rain;
	public ForecastSys sys;
	public string dt_txt;
}

[Serializable]
public class ForecastMain
{
	public float temp;
	public float feels_like;
	public float temp_min;
	public float temp_max;
	public int pressure;
	public int sea_level;
	public int grnd_level;
	public int humidity;
	public float temp_kf;
}

[Serializable]
public class ForecastRain
{
	public float _3h; // "3h" is not a valid C# field name, map to _3h
}

[Serializable]
public class ForecastSys
{
	public string pod;
}

[Serializable]
public class City
{
	public int id;
	public string name;
	public Coord coord;
	public string country;
	public int population;
	public int timezone;
	public long sunrise;
	public long sunset;
}
