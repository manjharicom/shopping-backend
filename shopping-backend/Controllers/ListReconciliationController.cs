using Microsoft.AspNetCore.Mvc;
using Services.Boundaries;
using System.Threading.Tasks;

namespace shopping_backend.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ListReconciliationController : ControllerBase
	{
		private readonly IListReconciliationService _listReconciliationService;

		public ListReconciliationController(IListReconciliationService listReconciliationService)
		{
			_listReconciliationService = listReconciliationService;
		}

		[HttpPost]
		[Route("{shoppingListId}")]
		public async Task<ActionResult> ReconcileListAsync(int shoppingListId)
		{
			await _listReconciliationService.ReconcileListAsync(shoppingListId);
			return Ok();
		}

	}
}
