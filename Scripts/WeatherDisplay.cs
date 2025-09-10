using TMPro;
using UnityEngine;

public class WeatherDisplay : MonoBehaviour
{
    //Page 

	[SerializeField] private CurrentWidget currentWidget;
	[SerializeField] private ForecastWidget forecastWidget;


	public void CurrentSetup(CurrentInfo currentInfo, float highTemp, float lowTemp)
    {
		currentWidget.Setup(currentInfo, highTemp, lowTemp);
	} 
	public void ForecastSetup(ForecastInfo forecastInfo)
    {
		forecastWidget.Setup(forecastInfo);
	} 
}
