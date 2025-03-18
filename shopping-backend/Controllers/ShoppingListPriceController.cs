using Microsoft.AspNetCore.Mvc;
using Services.Boundaries;
using Services.Models;
using System.Threading.Tasks;

namespace shopping_backend.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ShoppingListPriceController : ControllerBase
	{
		private readonly IShoppingListPriceService _service;

		public ShoppingListPriceController(IShoppingListPriceService service)
		{
			_service = service;
		}

		[HttpPost]
		public async Task<ActionResult> AddAsync([FromBody] AddShoppingListPriceRequestModel request)
		{
			var response = await _service.AddShoppingListPriceAsync(request);
			return Ok(response);
		}

	}
}
