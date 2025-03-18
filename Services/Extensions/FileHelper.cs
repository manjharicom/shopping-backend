using System;

namespace Services.Extensions
{
	public static class FileHelper
	{
		public static string UniqueFileNameSuffix()
		{
			return $"{DateTime.Now:yyyy-MM-dd_hh-mm-ss-ffff}";
		}
	}
}
