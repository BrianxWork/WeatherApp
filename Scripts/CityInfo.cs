using System;
using System.Collections.Generic;
using UnityEngine;

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