using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

public class Utilities
{
	public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
	{
		// Unix timestamp is seconds past epoch
		DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp);
		return dateTimeOffset.UtcDateTime; // returns UTC
	}


	public static DateTime GetLocalTime(long timestamp, int timezone)
	{
		TimeSpan offset = TimeSpan.FromSeconds(timezone);
		DateTime utc = Utilities.UnixTimeStampToDateTime(timestamp);

		DateTime localtime = utc + offset;

		return localtime;
	}


	//Ask Kevin
	public static string GetCountryNameISO2(string iso2)
	{
		try
		{
			RegionInfo region = new RegionInfo(iso2.ToUpper());
			return region.EnglishName;
		}
		catch
		{
			// fallback: try via culture
			var culture = CultureInfo
				.GetCultures(CultureTypes.SpecificCultures)
				.FirstOrDefault(c =>
				{
					try { return new RegionInfo(c.LCID).TwoLetterISORegionName == iso2.ToUpper(); }
					catch { return false; }
				});

			if (culture != null)
				return new RegionInfo(culture.LCID).EnglishName;

			return iso2; // still return something instead of crashing
		}
	}


	public static Dictionary<string, int> GetWeatherCodeCounts()
	{
		return new Dictionary<string, int>
		{
			{ "01d", 0 },
			{ "01n", 0 },
			{ "02d", 0 },
			{ "02n", 0 },
			{ "03d", 0 },
			{ "03n", 0 },
			{ "04d", 0 },
			{ "04n", 0 },
			{ "09d", 0 },
			{ "09n", 0 },
			{ "10d", 0 },
			{ "10n", 0 },
			{ "11d", 0 },
			{ "11n", 0 },
			{ "13d", 0 },
			{ "13n", 0 },
			{ "50d", 0 },
			{ "50n", 0 }
		};
	}
}
