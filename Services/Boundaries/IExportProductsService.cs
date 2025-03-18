using Services.Models;
using System.Threading.Tasks;

namespace Services.Boundaries
{
	public interface IExportProductsService
	{
		Task<string> ExportProductsAsync(string uploadsPath, string virtualFolder, ExportProductsRequest request);
	}
}
