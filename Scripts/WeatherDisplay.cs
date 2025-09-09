using TMPro;
using UnityEngine;

public class WeatherDisplay : MonoBehaviour
{
    //Page 

	[SerializeField] private CurrentWidget currentWidget;


	public void Setup(CurrentInfo weatherinfo)
    {
		currentWidget.Setup(weatherinfo);
	} 
}
