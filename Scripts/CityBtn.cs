using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CityBtn : MonoBehaviour
{
	[SerializeField] private Button thisbutton;
	[SerializeField] private TMP_Text tButtonCity;
	[SerializeField] private float lat;
	[SerializeField] private float lon;


	void Start()
	{
		tButtonCity = thisbutton.GetComponentInChildren<TMP_Text>();
		thisbutton.onClick.AddListener(UpdateLocationInfo);
	}

	public void SetCityInfo(string city, string country, float fLat, float fLon)
	{
		tButtonCity.text = city+", "+ country;
		lat = fLat;
		lon = fLon;
	}


	private void UpdateLocationInfo()
	{
		WeatherManager.Instance.WeatherUpdate(lat, lon);
		SearchSystem.Instance.ClearSearchResults();
		SearchSystem.Instance.SetSearchWindow(false);
	}

}