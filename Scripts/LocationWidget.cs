using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class LocationWidget : MonoBehaviour
{

	[SerializeField] private TextMeshProUGUI tCountry;
	[SerializeField] private TextMeshProUGUI tCity;
	[SerializeField] private TextMeshProUGUI tDate;
	[SerializeField] private TextMeshProUGUI tTime;
	public void Setup(WeatherInfo weatherinfo)//pass data to text components 
	{
		ApplyCountry(weatherinfo.sys.country);

		tCity.text = weatherinfo.name;

		GetLocalTime(weatherinfo.timezone);
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
