using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class CityBtn : MonoBehaviour
{
	[SerializeField] private Button thisbutton;
	[SerializeField] private TMP_Text tButtonCity;
	[SerializeField] private string sCity;
	[SerializeField] private string sAdminName;
	[SerializeField] private string sCountry;


	void Start()
	{
		tButtonCity = thisbutton.GetComponentInChildren<TMP_Text>();
		thisbutton.onClick.AddListener(UpdateLocationInfo);
	}

	public void SetCityInfo(string city, string adminName, string country)
	{
		// Example output: "Kolkata, West Bengal, India"
		if (city != adminName)
		{
			tButtonCity.text = $"{city}, {adminName}, {country}";
		}
		else
		{
			tButtonCity.text = $"{city}, {country}";
		}

			// Store the data for later use
			sCity = city;
		sCountry = country;
		sAdminName = adminName; // (optional: add this field if you want to keep it)
	}


	private void UpdateLocationInfo()
	{
		WeatherManager.Instance.WeatherUpdate(sCity, sAdminName, sCountry);//!!
		SearchSystem.Instance.ClearSearchResults();
		SearchSystem.Instance.ClearSearchBarText();
		SearchSystem.Instance.SetSearchWindow(false);
	}

}