using Microsoft.AspNetCore.Mvc;
using Services.Boundaries;
using Services.Enums;
using Services.Models;
using System.Threading.Tasks;

namespace shopping_backend.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

		[HttpGet]
        [Route("GetProducts")]
        public async Task<ActionResult> GetProductsAsync([FromQuery] OrderBy orderBy, [FromQuery] string searchText, [FromQuery] int? categoryId, [FromQuery] int? areaId)
        {
            var categories = await _service.GetProductsAsync(orderBy, searchText, categoryId, areaId);
            return Ok(categories);
        }

        [HttpGet]
        [Route("GetProduct")]
        public async Task<ActionResult> GetProductAsync([FromQuery] int productId)
        {
            var category = await _service.GetProductAsync(productId);
            return Ok(category);
        }

        [HttpGet]
        [Route("GetProductsForCategory")]
        public async Task<ActionResult> GetProductsForCategoryAsync([FromQuery] int categoryId)
        {
            var categories = await _service.GetProductsForCategoryAsync(categoryId);
            return Ok(categories);
        }

        [HttpGet]
		[Route("Search")]
		public async Task<ActionResult> SearchProductsAsync([FromQuery] string searchText, [FromQuery] int? categoryId, [FromQuery] int? areaId)
        {
			var products = await _service.SearchProductsAsync(searchText, categoryId, areaId);
			return Ok(products);
		}

        [HttpPost]
		[Route("Add/{shoppingListId}/{productId}/{quantity}/{purchased?}")]
		public async Task<ActionResult> AddProductToListAsync(int shoppingListId, int productId, int quantity, bool purchased = false)
        {
            await _service.AddProductToListAsync(shoppingListId, productId, quantity, purchased);
            return Ok();
		}

		[HttpGet]
		[Route("ShoppingList")]
		public async Task<ActionResult> GetShoppingListProductsAsync([FromQuery] int? shoppingListId = null, [FromQuery] bool isPurchased = false)
		{
			var products = await _service.GetShoppingListProductsAsync(shoppingListId, isPurchased);
			return Ok(products);
		}

        [HttpDelete]
		[Route("Delete/{shoppingListId}/{productId}")]
		public async Task<ActionResult> RemoveProductFromListAsync(int shoppingListId, int productId)
		{
			await _service.RemoveProductFromListAsync(shoppingListId, productId);
			return Ok();
		}

		[HttpPost]
		[Route("Add")]
		public async Task<ActionResult> AddProductAsync([FromBody] AddProductRequest request)
		{
			var response = await _service.AddProductAsync(request);
			return Ok(response);
		}

		[HttpPut]
		[Route("Update/{shoppingListId}/{productId}/{quantity}")]
		public async Task<ActionResult> UpdateProductQuanityListAsync(int shoppingListId, int productId, int quantity)
		{
			await _service.UpdateProductQuanityListAsync(shoppingListId, productId, quantity);
			return Ok();
		}

		[HttpPut]
		[Route("Update")]
		public async Task<ActionResult> UpdateProductAsync([FromBody] UpdateProductRequest request)
		{
			var response = await _service.UpdateProductAsync(request);
			return Ok(response);
		}
	}
}
