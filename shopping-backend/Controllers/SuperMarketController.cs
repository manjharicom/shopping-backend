using Microsoft.AspNetCore.Mvc;
using Services.Boundaries;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace shopping_backend.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SuperMarketController : ControllerBase
	{
		private readonly ISuperMarketService _service;

		public SuperMarketController(ISuperMarketService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<ActionResult> GetSuperMarketsAsync([FromQuery] string superMarketIds = null)
		{
			var superMarkets = await _service.GetSuperMarketsAsync(superMarketIds);
			return Ok(superMarkets);
		}

		[HttpGet]
		[Route("{superMarketId}")]
		public async Task<ActionResult> GetSuperMarketAsync(int superMarketId)
		{
			var superMarket = await _service.GetSuperMarketAsync(superMarketId);
			return Ok(superMarket);
		}

		[HttpGet]
		[Route("cat/")]
		public async Task<ActionResult> GetCategorySuperMarketsAsync([FromQuery] int superMarketId)
		{
			var superMarkets = await _service.GetCategorySuperMarketsAsync(superMarketId);
			return Ok(superMarkets);
		}

		[HttpPost]
		public async Task<ActionResult> AddAsync([FromBody] AddRequest request)
		{
			var response = await _service.AddAsync(request.Name);
			return Ok(response);
		}

		[HttpPost]
		[Route("Set/{superMarketId}")]
		public async Task<ActionResult> SetSuperMarketAsync(int superMarketId)
		{
			await _service.SetSuperMarketAsync(superMarketId);
			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> UpdateAsync([FromBody] UpdateRequest request)
		{
			var response = await _service.UpdateAsync(request);
			return Ok(response);
		}

		[HttpPut]
		[Route("{superMarketId}")]
		public async Task<ActionResult> MergeCategorySuperMarketAsync(int superMarketId
			, [FromBody] IEnumerable<CategorySuperMarketRequest> categorySuperMarkets)
		{
			var response = await _service.MergeCategorySuperMarketAsync(superMarketId, categorySuperMarkets);
			return Ok(response);
		}

		[HttpDelete]
		[Route("{superMarketId}")]
		public async Task<ActionResult> DeleteAsync(int superMarketId)
		{
			await _service.DeleteAsync(superMarketId);
			return Ok();
		}
	}
}
