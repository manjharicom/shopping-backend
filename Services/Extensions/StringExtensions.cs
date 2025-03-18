using System.Web;

namespace Services.Extensions
{
	public static class StringExtensions
	{
		public static string CombineWithVirtualPaths(this string path, params string[] paths)
		{
			if (path == null) return null;

			string result = string.Empty;
			for (int i = 0; i <= paths.Length - 1; i++)
			{
				result += paths[i].AddToStart('/');
			}

			return path.RemoveFromStart('/') + result.AddToStart('/');
		}

		/// <summary>
		/// Add this character to the start of the string if it doesn't exist
		/// </summary>
		/// <param name="text"></param>
		/// <param name="toAdd"></param>
		/// <returns></returns>
		public static string AddToStart(this string text, char toAdd)
		{
			if (text == null) return string.Empty;

			if (!text.StartsWith(toAdd))
			{
				return toAdd + text;
			}
			return text;
		}

		/// <summary>
		/// Add this character to the end of the string if it doesn't exist
		/// </summary>
		/// <param name="text"></param>
		/// <param name="toAdd"></param>
		/// <returns></returns>
		public static string AddToEnd(this string text, char toAdd)
		{
			if (text == null) return string.Empty;

			if (!text.EndsWith(toAdd))
			{
				return text + toAdd;
			}
			return text;
		}

		public static string RemoveFromStart(this string text, char toRemove)
		{
			if (text == null) return string.Empty;

			if (text.StartsWith(toRemove))
			{
				return text.Substring(1);
			}
			return text;
		}

		public static string UrlEncode(this string virtualPath)
		{
			return HttpUtility.UrlEncode(virtualPath);
		}
	}
}
