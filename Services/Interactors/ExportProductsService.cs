using OfficeOpenXml;
using OfficeOpenXml.Style;
using Services.Boundaries;
using Services.Extensions;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interactors
{
	public class ExportProductsService : IExportProductsService
	{
		private readonly IProductService _productService;
		private readonly IExcelService _excelService;
		private readonly IExcelFile _excelFile;
		private const decimal TopMarginCm = 1.91m;
        private const decimal BottomMarginCm = 1.91m;
        private const decimal LeftMarginCm = 0.64m;
        private const decimal RightMarginCm = 0.64m;
		private const decimal Conversion = 2.54m;

        public ExportProductsService(IProductService productService
			, IExcelService excelService
			, IExcelFile excelFile)
		{
			_productService = productService;
			_excelService = excelService;
			_excelFile = excelFile;
		}

		public async Task<string> ExportProductsAsync(string uploadsPath, string virtualFolder, ExportProductsRequest request)
		{
			var products = await _productService.GetProductsAsync(Enums.OrderBy.Area, request.SearchText, request.CategoryId, request.AreaId);

			using (var package = _excelService.SetupExcelPackage("Products"))
			{
				using (var worksheet = _excelService.GetWorksheet(package, "Products", true))
				{
					AddContent(worksheet, products);
					var filename = SaveExcel(package, uploadsPath, virtualFolder, "Products");
					return filename;
				}
			}
		}

		private void AddContent(ExcelWorksheet worksheet, IEnumerable<FullProductModel> products)
		{
			var row = AddColumnHeaders(worksheet);
			AddRows(row, worksheet, products);
			worksheet.Cells.AutoFitColumns();
			worksheet.View.FreezePanes(2, 1);
            worksheet.PrinterSettings.TopMargin = TopMarginCm / Conversion;
            worksheet.PrinterSettings.BottomMargin = BottomMarginCm / Conversion;
            worksheet.PrinterSettings.LeftMargin = LeftMarginCm / Conversion;
            worksheet.PrinterSettings.RightMargin = RightMarginCm / Conversion;
        }

		private int AddColumnHeaders(ExcelWorksheet worksheet)
		{
			var row = 1;
			var cell = 1;
			worksheet.Cells[row, cell].Value = "Name";
			worksheet.Cells[row, ++cell].Value = "Category";
			worksheet.Cells[row, ++cell].Value = "Storage Area";
			worksheet.Cells[row, ++cell].Value = "Uom";

			for (var i = 1; i <= cell; i++)
			{
				worksheet.Cells[row, i].Style.Font.Bold = true;
				worksheet.Cells[row, i].Style.Border.BorderAround(ExcelBorderStyle.Thin);
			}

			return ++row;
		}

		private void AddRows(int startRow, ExcelWorksheet worksheet, IEnumerable<FullProductModel> products)
		{
			var row = startRow;
			foreach (var product in products)
			{
				var cell = 1;
				worksheet.Cells[row, cell].Value = $"{product.Name}   ({product.Uom})";
				worksheet.Cells[row, cell].Style.Border.BorderAround(ExcelBorderStyle.Thin);

				worksheet.Cells[row, ++cell].Value = product.Category;
				worksheet.Cells[row, cell].Style.Border.BorderAround(ExcelBorderStyle.Thin);

				worksheet.Cells[row, ++cell].Value = product.Area;
				worksheet.Cells[row, cell].Style.Border.BorderAround(ExcelBorderStyle.Thin);

				worksheet.Cells[row, ++cell].Value = product.Uom;
				worksheet.Cells[row, cell].Style.Border.BorderAround(ExcelBorderStyle.Thin);

				row++;
			}
		}

		private string SaveExcel(ExcelPackage package, string uploadsPath, string virtualFolder, string title)
		{
			var reportPrefix = title.Replace(" ", "").Replace("/", "").Replace("\\", "");
			var filename = $"{uploadsPath}\\{reportPrefix}_{FileHelper.UniqueFileNameSuffix()}.xlsx";
			return _excelFile.SaveExcel(package, filename, uploadsPath, virtualFolder);
		}
	}
}
