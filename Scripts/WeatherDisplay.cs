using TMPro;
using UnityEngine;

public class WeatherDisplay : MonoBehaviour
{
    //Page 

	[SerializeField] private CurrentWidget currentWidget;


	public void Setup(WeatherInfo weatherinfo)
    {
		currentWidget.Setup(weatherinfo);
	} 
}
