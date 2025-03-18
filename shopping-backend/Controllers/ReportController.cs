using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services.Boundaries;
using Services.Models;
using System.Threading.Tasks;

namespace shopping_backend.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ReportController : ControllerBase
	{
		private readonly IExportProductsService _service;
		private readonly Uploads _connectionOptions;

		public ReportController(IExportProductsService service
			, IOptions<Uploads> connectionOptions)
		{
			_service = service;
			_connectionOptions = connectionOptions.Value;
		}


		[HttpPost]
		public async Task<ActionResult> ExportProductsAsync([FromBody] ExportProductsRequest request)
		{
			var path = _connectionOptions.Folder;
			var virtualFolder = _connectionOptions.Virtual;
			var file = await _service.ExportProductsAsync(path, virtualFolder, request);
			return Ok(new ExportResponse(file));

		}
	}
}
