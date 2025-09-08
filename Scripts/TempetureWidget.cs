using TMPro;
using UnityEngine;

public class TempetureWidget : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI tTempeture;
	[SerializeField] private TextMeshProUGUI tMaxnMinTemp;
	public void Setup(WeatherInfo weatherinfo)//pass data to text components 
	{
		Main main = weatherinfo.main;

		float temp = main.temp;
		tTempeture.text = $"{temp:F1}¢XC";

		string tempmin = $"{main.temp_min:F0}¢X";	
		string tempmax = $"{main.temp_max:F0}¢X";
		tMaxnMinTemp.text = tempmin + "/" + tempmax;
	}
}
