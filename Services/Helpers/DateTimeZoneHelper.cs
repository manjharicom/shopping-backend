using System;

namespace Services.Helpers
{
	internal static class DateTimeZoneHelper
	{
		public static DateTime ConvertTimeFromUtc(DateTime utcDateTime)
		{
			if (TryConvertTimeFromUtc(utcDateTime, out DateTime converted))
				return converted;
			return utcDateTime;
		}

		private static bool TryConvertTimeFromUtc(DateTime utcDateTime, out DateTime convertedDateTime)
		{
			try
			{
				convertedDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, TimeZoneInfo.Local);
				return true;
			}
			catch (Exception e)
			{
			}

			convertedDateTime = utcDateTime;
			return false;
		}
	}
}
