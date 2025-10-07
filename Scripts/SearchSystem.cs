using NUnit.Framework;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SearchSystem : MonoBehaviour
{
	public static SearchSystem Instance { get; private set; }
	[SerializeField] private Cities allCities;
	[SerializeField] private GameObject searchWindow;
	[SerializeField] private GameObject searchContent;
	[SerializeField] private GameObject cityPrefab;
	[SerializeField] private TMP_InputField searchBar;
	[SerializeField] private Button searchBtn;


	private void Awake()
	{
		if (Instance != null && Instance != this)
		{
			Destroy(this);
		}
		else
		{
			Instance = this;
		}
	}

	void Start()
	{
		//Load the file from Resources
		//TextAsset jsonFile = Resources.Load<TextAsset>("GeoInfo"); // omit ".json"
		TextAsset jsonFile = Resources.Load<TextAsset>("citylist"); // omit ".json"

		if (jsonFile == null)
		{
			Debug.LogError("Could not find cities.json in Resources!");
			return;
		}

		//Wrap it because JsonUtility needs a root object
		string wrappedJson = "{\"cities\":" + jsonFile.text + "}";

		allCities = JsonUtility.FromJson<Cities>(wrappedJson);

		foreach (CityInfo city in allCities.cities)
		{
			city.ToCountryString();
		}


			//Search("united states");//when string has changed in the text field
			searchBtn.onClick.AddListener(()=>SetSearchWindow(true));
		searchBar.onValueChanged.AddListener(OnSearchValueChanged);
	}

	// Update is called once per frame
	private void OnSearchValueChanged(string newValue)
	{

		if (string.IsNullOrWhiteSpace(newValue))
		{
			ClearSearchResults();
			return;
		}

		Search(searchBar.text);
	}


	public void Search(string keyword)
	{
		// Clear old results first
		foreach (Transform child in searchContent.transform)
		{
			Destroy(child.gameObject);
		}

		int count = 0;
		foreach (CityInfo city in allCities.cities)
		{
			if (city.country.ToLower().Contains(keyword.ToLower()) || city.name.ToLower().Contains(keyword.ToLower()))
			{
				GameObject obj = Instantiate(cityPrefab, searchContent.transform);
				obj.transform.SetParent(searchContent.transform, false);
				CityBtn currentbtn = obj.GetComponent<CityBtn>();
				
				currentbtn.SetCityInfo(city.name, city.country, city.coord.lat, city.coord.lon);

				count++;

				if (count > 100) // limit number of results
					break;
			}
		}

		//RectTransform contentRect = searchContent.GetComponent<RectTransform>();
		//LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
		Debug.Log($"Displayed {count} cities matching '{keyword}'.");
	}


	public void ClearSearchResults()
	{
		foreach (Transform child in searchContent.transform)
		{
			Destroy(child.gameObject);
		}
	}

	public void SetSearchWindow(bool isActive)
	{
		searchWindow.SetActive(isActive);
	}

	public void ClearSearchBarText()
	{
		searchBar.text = "";
	}
}
