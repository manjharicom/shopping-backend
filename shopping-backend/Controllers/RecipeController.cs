using Azure.Core;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Boundaries;
using Services.Models;
using System.Threading.Tasks;

namespace shopping_backend.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RecipeController : ControllerBase
	{
		private readonly IRecipeService _service;

		public RecipeController(IRecipeService service)
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
		[Route("{recipeId}")]
		public async Task<ActionResult> GetAsync(int recipeId)
		{
			var resp = await _service.GetAsync(recipeId);
			return Ok(resp);
		}

		[HttpGet]
		[Route("search")]
		public async Task<ActionResult> SearchAsync([FromQuery] string searchText)
		{
			var resp = await _service.SearchAsync(searchText);
			return Ok(resp);
		}

		[HttpPost]
		public async Task<ActionResult> AddAsync([FromBody] AddRecipeMenuRequest request)
		{
			var response = await _service.AddAsync(request);
			return Ok(response);
		}

		[HttpPost]
		[Route("{recipeId}")]
		public async Task<ActionResult> AddProductAsync(int recipeId, [FromBody] RecipeMenuProductModel request)
		{
			var response = await _service.AddProductAsync(recipeId, request);
			return Ok(response);
		}

		[HttpPut]
		public async Task<ActionResult> UpdateAsync([FromBody] UpdateRecipeRequest request)
		{
			var response = await _service.UpdateAsync(request);
			return Ok(response);
		}

		[HttpDelete]
		[Route("{recipeId}/{productId}")]
		public async Task<ActionResult> DeleteProductAsync(int recipeId, int productId)
		{
			var response = await _service.DeleteProductAsync(recipeId, productId);
			return Ok(response);
		}
	}
}
