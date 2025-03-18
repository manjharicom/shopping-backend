using OfficeOpenXml;

namespace Services.Boundaries
{
	public interface IExcelService
	{
		ExcelPackage SetupExcelPackage(string title);
		ExcelWorksheet GetWorksheet(ExcelPackage package, string title, bool showGridlines);
	}
}
