using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

//[Serializable]
//public class CityInfo
//{
//	public int id;
//	public string name;
//	public string state;
//	public string country;
//	public Coord coord;
//	public int count;

//	public void ToCountryString()
//	{
//		country = Utilities.GetCountryNameISO2(country);
//	}

//	public void PrintInfo()
//	{
//		Debug.Log(
//			$"ID: {id}\n" +
//			$"Name: {name}\n" +
//			$"State: {state}\n" +
//			$"Country: {country}\n" +
//			$"Longitude: {coord.lon}\n" +
//			$"Latitude: {coord.lat}"
//		);
//	}
//}

[Serializable]
public class CityInfo
{
	public string city;
	public string city_ascii;
	public float lat;
	public float lng;
	public string country;
	public string iso2;
	public string iso3;
	public string admin_name;
	public string capital;
	public long population;
	public long id;

	public override string ToString()
	{
		return $"City: {city}, Country: {country}, Lat: {lat}, Lng: {lng}, Population: {population}";
	}
}

[Serializable]
public class Cities
{
	public CityInfo[] cities;
}