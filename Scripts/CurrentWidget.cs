using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrentWidget : MonoBehaviour 
{
	//Location
	[SerializeField] private TextMeshProUGUI tCountry;
	[SerializeField] private TextMeshProUGUI tCity;
	[SerializeField] private TextMeshProUGUI tDate;
	[SerializeField] private TextMeshProUGUI tTime;

	//Temp
	[SerializeField] private TextMeshProUGUI tTempeture;
	[SerializeField] private TextMeshProUGUI tMaxnTemp;
	[SerializeField] private TextMeshProUGUI tMinTemp;

	//Condition
	[SerializeField] private TextMeshProUGUI tCondition;
	[SerializeField] private Image iConditionIcon;
	public void Setup(WeatherInfo weatherinfo)//pass data to text components 
	{
		//Location
		ApplyCountry(weatherinfo.sys.country);
		tCity.text = weatherinfo.name;
		GetLocalTime(weatherinfo.timezone);

		//Temp
		Main main = weatherinfo.main;
		float temp = main.temp;
		tTempeture.text = $"{temp:F1}¢XC";

		tMinTemp.text = $"{main.temp_min:F0}¢X";
		tMaxnTemp.text = $"{main.temp_max:F0}¢X";
		//tMaxnMinTemp.text = tempmin + "/" + tempmax;

		//Condition
		Weather weather = weatherinfo.weather[0];
		tCondition.text = weather.main;

		string iconPath = $"Sprite/Weather_Icon/{weather.icon}";
		Sprite iconSprite = Resources.Load<Sprite>(iconPath);
		iConditionIcon.sprite = iconSprite;
	}

	//testing







	void ApplyCountry(string countryCode)
	{
		RegionInfo region = new RegionInfo(countryCode);
		tCountry.text = region.EnglishName; // "Taiwan"
	}

	void GetLocalTime(int timezone)
	{
		TimeSpan offset = TimeSpan.FromSeconds(timezone);
		DateTime datetime = DateTime.UtcNow + offset;

		// Date part (yyyy-MM-dd)
		string datePart = datetime.ToString("yyyy-MM-dd");
		tDate.text = datePart;

		// Time part (hh:mmtt -> lowercase am/pm)
		string timePart = datetime.ToString("hh:mmtt", CultureInfo.InvariantCulture).ToLower();
		tTime.text = timePart;

	}

}
