using Microsoft.AspNetCore.Mvc;
using Services.Boundaries;
using Services.Models;
using System.Threading.Tasks;

namespace shopping_backend.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class MenuController : ControllerBase
	{
		private readonly IMenuService _service;

		public MenuController(IMenuService service)
		{
			_service = service;
		}
	
		[HttpGet]
		public async Task<ActionResult> GetAsync([FromQuery] string name)
		{
			var resp = await _service.GetAsync(name);
			return Ok(resp);
		}

		[HttpGet]
		[Route("{menuId}")]
		public async Task<ActionResult> GetAsync(int menuId)
		{
			var resp = await _service.GetAsync(menuId);
			return Ok(resp);
		}

		[HttpPost]
		public async Task<ActionResult> AddAsync([FromBody] AddRecipeMenuRequest request)
		{
			var response = await _service.AddAsync(request);
			return Ok(response);
		}

		[HttpPost]
		[Route("{menuId}")]
		public async Task<ActionResult> AddProductAsync(int menuId, [FromBody] RecipeMenuProductModel request)
		{
			var response = await _service.AddProductAsync(menuId, request);
			return Ok(response);
		}

		[HttpPost]
		[Route("{menuId}/{recipeId}")]
		public async Task<ActionResult> AddRecipeAsync(int menuId, int recipeId)
		{
			var response = await _service.AddRecipeAsync(menuId, recipeId);
			return Ok(response);
		}

		[HttpPut]
		public async Task<ActionResult> UpdateAsync([FromBody] UpdateMenuRequest request)
		{
			var response = await _service.UpdateAsync(request);
			return Ok(response);
		}

		[HttpDelete]
		[Route("deleteproduct/{menuId}/{productId}")]
		public async Task<ActionResult> DeleteProductAsync(int menuId, int productId)
		{
			var response = await _service.DeleteProductAsync(menuId, productId);
			return Ok(response);
		}

		[HttpDelete]
		[Route("deleterecipe/{menuId}/{recipeId}")]
		public async Task<ActionResult> DeleteRecipeAsync(int menuId, int recipeId)
		{
			var response = await _service.DeleteRecipeAsync(menuId, recipeId);
			return Ok(response);
		}
	}
}
