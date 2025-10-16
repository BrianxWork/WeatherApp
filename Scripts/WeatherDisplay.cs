using TMPro;
using UnityEngine;

public class WeatherDisplay : MonoBehaviour
{
    //Page 

	[SerializeField] private CurrentWidget currentWidget;
	[SerializeField] private ForecastWidget forecastWidget;



	void Start()
	{
		
	}

	public void CurrentSetup(CurrentInfo currentInfo, float highTemp, float lowTemp,string sCity =null,string sAdminName=null,string sCountry=null)
    {
		currentWidget.Setup(currentInfo, highTemp, lowTemp, sAdminName: sAdminName);
	} 
	public void ForecastSetup(ForecastInfo forecastInfo)
    {
		forecastWidget.Setup(forecastInfo);
	} 

	private void NewLocationUpdateDisplay(float lat, float lon)
	{
		
	}
}
