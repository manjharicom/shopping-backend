using Microsoft.AspNetCore.Mvc;
using Services.Boundaries;
using Services.Models;
using System.Threading.Tasks;

namespace shopping_backend.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UomController : ControllerBase
	{
		private readonly IUomService _service;

		public UomController(IUomService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult> GetUomsAsync([FromQuery] string uoms = null)
		{
			var list = await _service.GetUomsAsync(uoms);
			return Ok(list);
		}

		[HttpGet]
		[Route("{uom}")]
		public async Task<ActionResult> GetUomAsync(int uom)
		{
			var category = await _service.GetUomAsync(uom);
			return Ok(category);
		}

		[HttpPost]
		public async Task<ActionResult> AddUomAsync([FromBody] AddUomRequest request)
		{
			var response = await _service.AddUomAsync(request);
			return Ok(response);
		}

		[HttpPut]
		public async Task<ActionResult> UpdateUomAsync([FromBody] UpdateUomRequest request)
		{
			var response = await _service.UpdateUomAsync(request);
			return Ok(response);
		}
	}
}
