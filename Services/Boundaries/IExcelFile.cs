using OfficeOpenXml;

namespace Services.Boundaries
{
	public interface IExcelFile
	{
		string SaveExcel(ExcelPackage package
			, string filename
			, string path
			, string virtualFolder);
	}
}
