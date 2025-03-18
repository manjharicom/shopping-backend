using OfficeOpenXml;
using Services.Boundaries;

namespace Services.Interactors
{
	public class ExcelService : IExcelService
	{
		public ExcelPackage SetupExcelPackage(string title)
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			var package = new ExcelPackage();
			package.Workbook.Properties.Title = title;
			return package;
		}

		public ExcelWorksheet GetWorksheet(ExcelPackage package, string title, bool showGridlines)
		{
			var worksheet = package.Workbook.Worksheets.Add(title);
			worksheet.View.ShowGridLines = showGridlines;
			return worksheet;
		}
	}
}
