using Microsoft.AspNetCore.Mvc;
using Services.Boundaries;
using Services.Models;
using System.Threading.Tasks;

namespace shopping_backend.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CategoryController : ControllerBase
	{
		private readonly ICategoryService _service;

		public CategoryController(ICategoryService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult> GetCategoriesAsync([FromQuery] string categoryIds = null, [FromQuery] bool includeProducts = false)
		{
			var categories = await _service.GetCategoriesAsync(categoryIds, includeProducts);
			return Ok(categories);
		}

		[HttpGet]
		[Route("{categoryId}")]
		public async Task<ActionResult> GetCategoryAsync(int categoryId)
		{
			var category = await _service.GetCategoryAsync(categoryId);
			return Ok(category);
		}

		[HttpPost]
		public async Task<ActionResult> AddCategoryAsync([FromBody] AddRequest request)
		{
			var response = await _service.AddCategoryAsync(request.Name);
			return Ok(response);
		}

		[HttpPut]
		public async Task<ActionResult> UpdateCategoryAsync([FromBody] UpdateRequest request)
		{
			var response = await _service.UpdateCategoryAsync(request);
			return Ok(response);
		}
	}
}
