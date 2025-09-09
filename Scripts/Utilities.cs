using System;

public class Utilities
{
	public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
	{
		// Unix timestamp is seconds past epoch
		DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(unixTimeStamp);
		return dateTimeOffset.UtcDateTime; // returns UTC
	}

}
