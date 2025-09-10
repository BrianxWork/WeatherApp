using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

public class ForecastWidget : MonoBehaviour
{

	public float tempmax;
	public float tempmin;

	[SerializeField] public List<HourlyInfoBox> hourlyInfoBox = new List<HourlyInfoBox>();
	public void Setup(ForecastInfo weatherinfo)//pass data to text components 
	{
		//for five 3hours slot
		for (int i = 0; i < hourlyInfoBox.Count; i++)
		{
			HourlyInfoBox infoBox = hourlyInfoBox[i];

			//Set forcast tempeture
			ForecastItem forcastItem = weatherinfo.list[i];
			float temp = forcastItem.main.temp;
			infoBox.tTemp.text = $"{temp:F1}¢X";

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
