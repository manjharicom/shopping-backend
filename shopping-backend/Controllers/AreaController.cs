using Microsoft.AspNetCore.Mvc;
using Services.Boundaries;
using Services.Models;
using System.Threading.Tasks;

namespace shopping_backend.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AreaController : ControllerBase
	{
		private readonly IAreaService _service;
		
		public AreaController(IAreaService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult> GetAreasAsync([FromQuery] string areas = null, [FromQuery] bool includeProducts = false)
		{
			var categories = await _service.GetAreasAsync(areas, includeProducts);
			return Ok(categories);
		}

		[HttpGet]
		[Route("{areaId}")]
		public async Task<ActionResult> GetAreaAsync(int areaId)
		{
			var category = await _service.GetAreaAsync(areaId);
			return Ok(category);
		}

		[HttpPost]
		public async Task<ActionResult> AddAreaAsync([FromBody] AddRequest request)
		{
			var response = await _service.AddAreaAsync(request.Name);
			return Ok(response);
		}

		[HttpPut]
		public async Task<ActionResult> UpdateAreaAsync([FromBody] UpdateRequest request)
		{
			var response = await _service.UpdateAreaAsync(request);
			return Ok(response);
		}
	}
}
