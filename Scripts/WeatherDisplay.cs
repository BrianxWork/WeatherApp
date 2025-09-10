using TMPro;
using UnityEngine;

public class WeatherDisplay : MonoBehaviour
{
    //Page 

	[SerializeField] private CurrentWidget currentWidget;
	[SerializeField] private ForecastWidget forecastWidget;


	public void CurrentSetup(CurrentInfo currentInfo)
    {
		currentWidget.Setup(currentInfo);
	} 
	public void ForecastSetup(ForecastInfo forecastInfo)
    {
		forecastWidget.Setup(forecastInfo);
	} 


}
