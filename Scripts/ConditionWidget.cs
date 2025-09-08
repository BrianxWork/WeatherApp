using System.Drawing.Imaging;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConditionWidget : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI tCondition;
	[SerializeField] private Image iConditionIcon;
	[SerializeField] private Sprite sprite;

 	//https://openweathermap.org/weather-conditions

	public void Setup(WeatherInfo weatherinfo)//pass data to text components 
	{
		Weather weather = weatherinfo.weather[0];
		tCondition.text= weather.main;


		string iconPath = $"Sprite/Weather_Icon/{weather.icon}";
		Sprite iconSprite = Resources.Load<Sprite>(iconPath);
		iConditionIcon.sprite = iconSprite;
	}
}
