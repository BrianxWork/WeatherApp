using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TMPro;
using UnityEditor.VisionOS;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static System.Net.WebRequestMethods;


public class WeatherManager : MonoBehaviour
{
	public static WeatherManager Instance { get; private set; }
	private string currentUrl = "https://api.openweathermap.org/data/2.5/weather?lat=35.6895&lon=139.6917&appid=0a313a23d526c8f3aef2fb1ed72cd79c&units=metric";
	private string forecastUrl = "https://api.openweathermap.org/data/2.5/forecast?lat=35.6895&lon=139.6917&appid=0a313a23d526c8f3aef2fb1ed72cd79c&units=metric";
	private string geoUrl = "https://ipapi.co/json/";
	private string apiKey = "0a313a23d526c8f3aef2fb1ed72cd79c";
	[SerializeField] private CurrentInfo currentInfo;
	[SerializeField] private ForecastInfo forecastInfo;
	[SerializeField] private IPInfo ipInfo;
	[SerializeField] private WeatherDisplay WeatherDisplay;

	[SerializeField] private float highTemp;
	[SerializeField] private float lowTemp;

	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
	}

	private void Start()
	{
		StartCoroutine(InitWeather());
	}


	void WeatherUpdate() => StartCoroutine(InitWeather());


	private IEnumerator InitWeather()
	{
		// Try IP-based location first
		yield return GetLocationByIP((lat, lon, country) =>
		{
			currentUrl = ModifyAPIRequest(lat, lon);
			forecastUrl = ModifyAPIRequest(lat, lon, true);
			Debug.Log($"Using {country} for default location");
		});

		if (string.IsNullOrEmpty(currentUrl) || string.IsNullOrEmpty(forecastUrl))
		{
			Debug.LogError("IP location failed, using Tokyo as fallback");
		}

		// Now load weather with final URL
		yield return ShowLoadWeatherdata(forecastUrl, true);
		
		yield return ShowLoadWeatherdata(currentUrl);
	}

	//private void Locationcallback(float lat, float lon, string country)
	//{
	//	currentUrl = ModifyAPIRequest(lat, lon);
	//	Debug.Log($"Using {country} for default location");
	//}

	IEnumerator ShowLoadWeatherdata(string apirequest, bool isForcast = false)
	{

		UnityWebRequest weatherApi = new UnityWebRequest(apirequest);
		weatherApi.downloadHandler = new DownloadHandlerBuffer();
		yield return weatherApi.SendWebRequest();//wait for weatherApi to return result

		if (weatherApi.result != UnityWebRequest.Result.Success)//Check if result is returned successfully
		{
			Debug.LogError(weatherApi.error);
		}
		else
		{
			if (!isForcast)
			{
				Debug.Log("JSON: " + weatherApi.downloadHandler.text);
				string weathertext = weatherApi.downloadHandler.text;
				currentInfo = JsonUtility.FromJson<CurrentInfo>(weathertext);
				currentInfo.ToDisplayString();
				WeatherDisplay.CurrentSetup(currentInfo, highTemp, lowTemp);
			}
			else
			{
				Debug.Log("JSON: " + weatherApi.downloadHandler.text);
				string weathertext = weatherApi.downloadHandler.text;
				forecastInfo = JsonUtility.FromJson<ForecastInfo>(weathertext);
				forecastInfo.ToDisplayString();
				WeatherDisplay.ForecastSetup(forecastInfo);
				(highTemp,lowTemp)=GetHighnLowTemp(forecastInfo);
			}
		}
	}

	//For Default location details
	public IEnumerator GetLocationByIP(System.Action<float, float, string> callback)
	{
		using (UnityWebRequest request = UnityWebRequest.Get(geoUrl))
		{
			yield return request.SendWebRequest();

			if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError("Geo lookup failed: " + request.error);
				// fallback: Tokyo
				callback?.Invoke(35.6895f, 139.6917f, "Japan");
			}
			else
			{
				string json = request.downloadHandler.text;
				ipInfo = JsonUtility.FromJson<IPInfo>(json);

				Debug.Log($"Detected country: {ipInfo.country} ({ipInfo.latitude}, {ipInfo.longitude})");
				callback?.Invoke(ipInfo.latitude, ipInfo.longitude, ipInfo.country_name);
			}
		}
	}

	public string ModifyAPIRequest(double lat, double lon, bool isForcast = false, string dt = null, string date = null, string exclude = null)
	{

		string tempURL;

		if (!isForcast)
		{
			tempURL = "https://api.openweathermap.org/data/2.5/weather?" +
			$"lat={lat}&lon={lon}&appid={apiKey}&units=metric";
		}
		else
		{
			tempURL = "https://api.openweathermap.org/data/2.5/forecast?" +
			$"lat={lat}&lon={lon}&appid={apiKey}&units=metric";
		}


		// Only add optional params if not null/empty
		if (!string.IsNullOrEmpty(date))
			tempURL += $"&date={date}";

		if (!string.IsNullOrEmpty(dt))
			tempURL += $"&dt={dt}";

		if (!string.IsNullOrEmpty(exclude))
			tempURL += $"&exclude={exclude}";

		return tempURL;
	}

	public (float, float) GetHighnLowTemp(ForecastInfo weatherinfo)
	{
		List<float> temps = new List<float>();

		// one forcastItem represents every 3 hours, therefore I need 8 of it to get 24hour data
		for (int i = 0; i < 8; i++)
		{
			ForecastItem forcastItem = weatherinfo.list[i];
			float temp = forcastItem.main.temp;

			temps.Add(temp);
		}

		float tempmax = temps.Max();
		float tempmin = temps.Min();

		return (tempmax, tempmin);
	}

	//public IEnumerator GetCurrentLocation(Action<float,float> callback)
	//{
	//	// First, check if the user has location service enabled
	//	if (!Input.location.isEnabledByUser)
	//	{
	//		Debug.Log("Location not enabled by user");
	//		yield break;
	//	}

	//	// Start location service
	//	Input.location.Start();

	//	// Wait until service initializes (max 20s)
	//	int maxWait = 20;
	//	while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
	//	{
	//		yield return new WaitForSeconds(1);
	//		maxWait--;
	//	}

	//	if (maxWait < 1)
	//	{
	//		Debug.Log("Location service timed out");
	//		yield break;
	//	}

	//	if (Input.location.status == LocationServiceStatus.Failed)
	//	{
	//		Debug.Log("Unable to determine device location");
	//		yield break;
	//	}
	//	else
	//	{
	//		// Success: grab location
	//		float latitude = Input.location.lastData.latitude;
	//		float longitude = Input.location.lastData.longitude;
	//		Debug.Log($"Location: {latitude}, {longitude}");
	//		callback(latitude, longitude);
	//	}
	//	// Stop service to save battery
	//	Input.location.Stop();
	//}


}


