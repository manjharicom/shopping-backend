using OfficeOpenXml;
using Services.Boundaries;
using Services.Extensions;
using System.IO;

namespace Services.Interactors
{
	public class ExcelFile : IExcelFile
	{
		public string SaveExcel(ExcelPackage package, string filename, string path, string virtualFolder)
		{
			var fi = new FileInfo(filename);

			if (!fi.Directory.Exists)
			{
				fi.Directory.Create();
			}

			package.SaveAs(fi);
			filename = GetVirtualFileName(virtualFolder, filename, path);
			return filename;
		}

		private string GetVirtualFileName(string virtualFolder, string filename, string uploadsPath)
		{
			return $"{virtualFolder}/{filename.Replace(uploadsPath, "").Replace("\\", "/").RemoveFromStart('/')}";
		}
	}
}
