using Services.Boundaries;
using System.Threading.Tasks;
using System.Linq;

namespace Services.Interactors
{
	public class ListReconciliationService : IListReconciliationService
	{
		private readonly IProductService _productService;
		private readonly ITrelloQueryService _trelloQueryService;
		private readonly ITrelloCommandService _trelloCommandService;
		private readonly IShoppingListService _shoppingListService;

		public ListReconciliationService(IProductService productService
			, ITrelloQueryService trelloQueryService
			, ITrelloCommandService trelloCommandService
			, IShoppingListService shoppingListService)
		{
			_productService = productService;
			_trelloQueryService = trelloQueryService;
			_trelloCommandService = trelloCommandService;
			_shoppingListService = shoppingListService;
		}

		public async Task ReconcileListAsync(int shoppingListId)
		{
			var shoppingList = await _shoppingListService.GetAsync(shoppingListId);

			var shoppingListProducts = await _productService.GetShoppingListProductsAsync(shoppingListId);
			var checkListItems = await _trelloQueryService.GetChecklistItemsAsync(shoppingList.CheckListId);

			if (!(shoppingListProducts?.Any() ?? false) || !(checkListItems?.Any() ?? false))
				return;

			var completed = checkListItems.Where(c => c.State == "complete");
			var productIds = shoppingListProducts.Where(s => completed.Any(c => c.Name.Substring(0, c.Name.IndexOf(" (")) == s.Name && c.State == "complete")).Select(s => s.ProductId);
			if (productIds.Any())
				await _productService.DeleteShoppingListItemsAsync(shoppingListId, productIds);

			var checkListItemIds = completed.Select(c => c.Id);
			if (checkListItemIds?.Any() ?? false)
				await _trelloCommandService.DeleteCheckListItemsAsync(shoppingList.CheckListId, checkListItemIds);
		}
	}
}
