using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CurrentWidget : MonoBehaviour
{
	//Location
	[SerializeField] private TextMeshProUGUI tCountry;
	[SerializeField] private TextMeshProUGUI tStateCity;
	[SerializeField] private TextMeshProUGUI tDate;
	[SerializeField] private TextMeshProUGUI tTime;

	//Temp
	[SerializeField] private TextMeshProUGUI tTempeture;
	[SerializeField] private TextMeshProUGUI tMaxnTemp;
	[SerializeField] private TextMeshProUGUI tMinTemp;
	[SerializeField] private TextMeshProUGUI tFeelsLike;

	//Condition
	[SerializeField] private TextMeshProUGUI tCondition;
	[SerializeField] private Image iConditionIcon;
	public void Setup(CurrentInfo weatherinfo, float highTemp, float lowTemp, string sCity = null, string sAdminName = null, string sCountry=null)//pass data to text components 
	{
		//Location
		ApplyCountry(weatherinfo.sys.country);

		tStateCity.text = string.IsNullOrEmpty(WeatherManager.Instance.adminName)
		? weatherinfo.name
		: $" {weatherinfo.name}, {WeatherManager.Instance.adminName}";

		GetLocalTime(weatherinfo.timezone);

		//Temp
		Main main = weatherinfo.main;

		tTempeture.text = $"{Math.Floor(main.temp)}¢X";

		tMaxnTemp.text = $"High: {Math.Floor(highTemp)}¢X";
		tMinTemp.text = $"Low: {Math.Floor(lowTemp)}¢X";

		tFeelsLike.text = $"Feels like: {Math.Floor(main.feels_like)}¢X";
		//tMaxnMinTemp.text = tempmin + "/" + tempmax;

		//Condition
		Weather weather = weatherinfo.weather[0];
		tCondition.text = weather.main;



		string iconPath = $"Sprite/Weather_Icon/{weather.icon}";
		Sprite iconSprite = Resources.Load<Sprite>(iconPath);
		iConditionIcon.sprite = iconSprite;
	}

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
