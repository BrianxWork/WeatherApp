using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using UnityEngine;
using static UnityEngine.Rendering.VolumeComponent;

public class ForecastWidget : MonoBehaviour
{

	[System.NonSerialized] public float tempmax;
	[System.NonSerialized] public float tempmin;

	[SerializeField] public List<HourlyInfoBox> hourlyInfoBox = new List<HourlyInfoBox>();
	[SerializeField] public List<DailyInfoBox> dailyInfoBox = new List<DailyInfoBox>();
	public void Setup(ForecastInfo weatherinfo)//pass data to text components 
	{
		//Five 3hours Box
		SetHourBox(weatherinfo);
		SetDailyBox(weatherinfo);

	}

	public void SetHourBox(ForecastInfo weatherinfo)
	{
		for (int i = 0; i < hourlyInfoBox.Count; i++)
		{
			HourlyInfoBox infoBox = hourlyInfoBox[i];

			//Set forcast tempeture
			ForecastItem forcastItem = weatherinfo.list[i];
			float temp = forcastItem.main.temp;
			infoBox.tTemp.text = $"{Math.Floor(temp)}¢X";

			//Set forcast time
			TimeSpan offset = TimeSpan.FromSeconds(weatherinfo.city.timezone);
			DateTime utc = Utilities.UnixTimeStampToDateTime(forcastItem.dt);
			DateTime datetime = utc + offset;

			string timePart = datetime.ToString("h tt", CultureInfo.InvariantCulture).ToLower();
			infoBox.tTime.text = timePart;

			//There is always only one weather in the weather list
			string iconPath = $"Sprite/Weather_Icon/{forcastItem.weather[0].icon}";
			Sprite iconSprite = Resources.Load<Sprite>(iconPath);
			infoBox.iConditionIcon.sprite = iconSprite;
		}
	}

	public List<string> GetIcon(ForecastInfo weatherinfo, List<int> indexList)
	{
		//looping eachday and add all the weather icon into iconList
		//then from iconList find the icon that appears the most for that day
		//finally add it to dailyIconList for every forcast to display

		ForecastItem[] weatherList = weatherinfo.list;

		List<string> dailyIconList = new List<string>();

		int iStarter = 0;
		for (int i = 0; i < indexList.Count; i++)
		{
			Dictionary<string, int> dIcons = Utilities.GetWeatherCodeCounts();
			List<string> iconList = new List<string>();
			for (int j = iStarter; j < indexList[i]; j++)
			{
				ForecastItem forcastItem = weatherinfo.list[j];
				string temp = forcastItem.weather[0].icon;

				iconList.Add(temp);
			}

			//find condition
			iStarter = indexList[i];

			for (int j = 0; j < iconList.Count; j++)
			{
				if (dIcons.ContainsKey(iconList[j]))
				{
					dIcons[iconList[j]] += 1;
				}
			}

			string maxKey = null;
			int maxValue = int.MinValue;

			foreach (var pair in dIcons)
			{
				if (pair.Value > maxValue)
				{
					maxValue = pair.Value;
					maxKey = pair.Key;
				}
			}

			dailyIconList.Add(maxKey);
		}

		return dailyIconList;
	}

	public List<string> GetDayOfWeek(ForecastInfo weatherinfo)
	{
		ForecastItem[] weatherList = weatherinfo.list;

		List<string> Days = new List<string>() { "Today" };
		//List<string> Days = new List<string>() { };

		//current time
		DateTime tempdate = DateTime.UtcNow + TimeSpan.FromSeconds(weatherinfo.city.timezone);

		for (int i = 0; i < weatherList.Length; i++)
		{
			TimeSpan offset = TimeSpan.FromSeconds(weatherinfo.city.timezone);
			DateTime forcastUTC = Utilities.UnixTimeStampToDateTime(weatherList[i].dt);

			DateTime localForecast = forcastUTC + offset;

			//Debug.Log(localForecast.Date);

			if (tempdate.Date != localForecast.Date)
			{
				Days.Add(localForecast.DayOfWeek.ToString());
				tempdate = localForecast.Date;
				//Debug.Log("Add!!!!!!!!!!!!!!!");
			}
		}

		return Days;
	}

	public List<int> GetIndexList(ForecastInfo weatherinfo)
	{
		ForecastItem[] weatherList = weatherinfo.list;

		List<int> indexs = new List<int>() { };
		List<DateTime> datetime = new List<DateTime>();

		//current time
		DateTime tempdate = DateTime.UtcNow + TimeSpan.FromSeconds(weatherinfo.city.timezone);


		for (int i = 0; i < weatherList.Length; i++)
		{
			TimeSpan offset = TimeSpan.FromSeconds(weatherinfo.city.timezone);
			DateTime forcastUTC = Utilities.UnixTimeStampToDateTime(weatherList[i].dt);

			DateTime localForecast = forcastUTC + offset;
			datetime.Add(localForecast);
			//Debug.Log(localForecast.Date);

			if (tempdate.Date != localForecast.Date)
			{
				//indexs.Add(i-1);//save the previous date's index !!CHECK!!
				indexs.Add(i);//save the previous date's index !!CHECK!!
				tempdate = localForecast.Date;
				//Debug.Log("Add!!!!!!!!!!!!!!!");
			}
		}
		Debug.Log("My DateTimeList:\n " + string.Join(", \n", datetime));
		indexs.Add(weatherList.Length-1);
		Debug.Log("My IndexList: \n" + string.Join(", ", indexs));
		return indexs;
	}

	public List<(float, float)> FindDailyHighlow(ForecastInfo weatherinfo, List<int> indexList)
	{

		ForecastItem[] weatherList = weatherinfo.list;
		List<(float high, float low)> fivedailyhighlow = new List<(float, float)>();
		int iStarter = 0;

		for (int i = 0; i < indexList.Count; i++)
		{
			List<float> temps = new List<float>();

			//Break when loop to last index of the indexList
			if(iStarter== indexList[indexList.Count-1])
			{
				break;
			}

			for (int j = iStarter; j < indexList[i]; j++) //every indexList[i] means next day in weatherinfolist
			{
				ForecastItem forcastItem = weatherinfo.list[j];
				float temp = forcastItem.main.temp;

				temps.Add(temp);
				iStarter = indexList[i];
			}

			float tempmax = -999;
			float tempmin = -999;

			if(temps.Count >0)
			{
				tempmax = temps.Max();
				tempmin = temps.Min();
			}
			

			fivedailyhighlow.Add((tempmax, tempmin));
		}

		Debug.Log("My Forcast HighLowList:\n " + string.Join(", \n", fivedailyhighlow));
		return fivedailyhighlow;
	}


	public void SetDailyBox(ForecastInfo weatherinfo)
	{
		//FindHighLow
		List<(float, float)> highlowlist = new List<(float, float)>();
		List<int> indexList = GetIndexList(weatherinfo);
		highlowlist = FindDailyHighlow(weatherinfo, indexList);

		//Find Days
		List<string> Days = GetDayOfWeek(weatherinfo);

		//FindIcon
		List<string> icons = GetIcon(weatherinfo, indexList);

		bool hastodayforecast = indexList[0] != 0;

		for (int i = 0; i < dailyInfoBox.Count; i++)
		{
			//DayofWeek
			dailyInfoBox[i].tDay.text = Days[i];

			//WeatherIcon
			string iconPath = $"Sprite/Weather_Icon_forecast/{icons[i]}";
			Sprite iconSprite = Resources.Load<Sprite>(iconPath);
			dailyInfoBox[i].iConditionIcon.sprite = iconSprite;

			//HighLow Tempeture
			(float high, float low) = highlowlist[i];
			dailyInfoBox[i].tTempeture.text = $"{Math.Floor(high)}¢X/{Math.Floor(low)}¢X";
		}
	}














	//next 24 hours

	//Me Practicing Finding Min and Sorting 
	//public static int GetMin(List<float> list)
	//{
	//	int index = 0;

	//	for (int i = 0; i < list.Count-1; i++)
	//	{ 
	//		if(list[index]>list[i+1])
	//		{
	//			index = i+1;
	//		}
	//	}

	//	return index;
	//}

	//public static List<float> Sort(List<float>og)
	//{
	//	List<float> list = new List<float>();

	//	while (og.Count>0)
	//	{
	//		int indexMin = GetMin(og);
	//		list.Add(og[indexMin]);
	//		og.RemoveAt(indexMin);
	//	}

	//	return list;
	//}
}
