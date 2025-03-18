using Microsoft.AspNetCore.Mvc;
using Services.Boundaries;
using Services.Models;
using System.Threading.Tasks;

namespace shopping_backend.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ShoppingListController : ControllerBase
	{
		private readonly IShoppingListService _service;

		public ShoppingListController(IShoppingListService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult> GetAsync([FromQuery] string shoppingListIds = null)
		{
			var shoppingLists = await _service.GetAsync(shoppingListIds);
			return Ok(shoppingLists);
		}

		[HttpGet]
		[Route("{shoppingListId}")]
		public async Task<ActionResult> GetAsync(int shoppingListId)
		{
			var shoppingList = await _service.GetAsync(shoppingListId);
			return Ok(shoppingList);
		}

		[HttpPost]
		public async Task<ActionResult> AddAsync([FromBody] CreateShoppingListRequest request)
		{
			var response = await _service.CreateShoppingListAsync(request);
			return Ok(response);
		}

		[HttpPut]
		public async Task<ActionResult> UpdateAsync([FromBody] UpdateShoppingListRequest request)
		{
			var response = await _service.UpdateShoppingListAsync(request);
			return Ok(response);
		}

		[HttpDelete]
		[Route("{shoppingListId}")]
		public async Task<ActionResult> DeleteShoppingListAsync(int shoppingListId)
		{
			await _service.DeleteShoppingListAsync(shoppingListId);
			return Ok();
		}

		[HttpDelete]
		[Route("products/{shoppingListId}")]
		public async Task<ActionResult> DeleteShoppingListItemsAsync(int shoppingListId, [FromQuery] string shoppingListIds = null)
		{
			await _service.DeleteShoppingListItemsAsync(shoppingListId, shoppingListIds);
			return Ok();
		}
	}
}
