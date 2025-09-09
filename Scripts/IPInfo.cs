public class IPInfo
{
	public string ip;
	public string network;
	public string version;
	public string city;
	public string region;
	public string region_code;
	public string country;
	public string country_name;
	public string country_code;
	public string country_code_iso3;
	public string country_capital;
	public string country_tld;
	public string continent_code;
	public bool in_eu;
	public string postal;
	public float latitude;
	public float longitude;
	public string timezone;
	public string utc_offset;
	public string country_calling_code;
	public string currency;
	public string currency_name;
	public string languages;
	public float country_area;
	public int country_population;
	public string asn;
	public string org;

	public override string ToString()
	{
		return
			$"ip: {ip}\n" +
			$"network: {network}\n" +
			$"version: {version}\n" +
			$"city: {city}\n" +
			$"region: {region}\n" +
			$"region_code: {region_code}\n" +
			$"country: {country}\n" +
			$"country_name: {country_name}\n" +
			$"country_code: {country_code}\n" +
			$"country_code_iso3: {country_code_iso3}\n" +
			$"country_capital: {country_capital}\n" +
			$"country_tld: {country_tld}\n" +
			$"continent_code: {continent_code}\n" +
			$"in_eu: {in_eu}\n" +
			$"postal: {postal}\n" +
			$"latitude: {latitude}\n" +
			$"longitude: {longitude}\n" +
			$"timezone: {timezone}\n" +
			$"utc_offset: {utc_offset}\n" +
			$"country_calling_code: {country_calling_code}\n" +
			$"currency: {currency}\n" +
			$"currency_name: {currency_name}\n" +
			$"languages: {languages}\n" +
			$"country_area: {country_area}\n" +
			$"country_population: {country_population}\n" +
			$"asn: {asn}\n" +
			$"org: {org}";
	}
}


