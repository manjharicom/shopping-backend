using Services.Boundaries;
using Services.Helpers;
using Services.Models.Trello;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Interactors
{
	public class TrelloCommandService : ITrelloCommandService
	{
		private readonly ITrelloApiService _trelloApiService;
		private readonly ITrelloQueryService _trelloQueryService;
		private readonly IProductService _productService;
		private readonly IShoppingListService _shoppingListService;

		private const string ChecklistApiUrl = "checklists";

		public TrelloCommandService(ITrelloApiService trelloApiService
			, ITrelloQueryService trelloQueryService
			, IProductService productService
			, IShoppingListService shoppingListService)
		{
			_trelloApiService = trelloApiService;
			_trelloQueryService = trelloQueryService;
			_productService = productService;
			_shoppingListService = shoppingListService;
		}

		public async Task CreateCheckListAsync(CreateCheckListRequest request)
		{
			if (string.IsNullOrEmpty(request.name) || string.IsNullOrEmpty(request.idCard))
				return;
			var url = $"{ChecklistApiUrl}";
			await _trelloApiService.PostDataAsync(url, request);
		}


		public async Task CreateCheckListItemsAsync(int shoppingListId, bool deleteListFirst)
		{
			var shoppingList = await _shoppingListService.GetAsync(shoppingListId);
			var shoppingListProducts = await _productService.GetShoppingListProductsAsync(shoppingListId);

			var items = shoppingListProducts.Select(s => new CreateChecklistItemRequest
			{
				name= ChecklistItemNameHelper.ChecklistItemName(s)
			});

			var request = new CreateChecklistItemsRequest
			{
				CheckListId = shoppingList.CheckListId,
				DeleteListFirst = deleteListFirst,
				Items = items
			};

			await CreateCheckListItemsAsync(request);
		}

		public async Task DeleteCheckListItemsAsync(string checkListId, IEnumerable<string> checkListItemIds)
		{
			foreach (var checkListItemId in checkListItemIds)
				await DeleteCheckListItemAsync(checkListId, checkListItemId);
		}

		public async Task UpdateCheckListAsync(string checkListId, UpdateCheckListRequest request)
		{
			if (request == null)
				return;
			var url = $"{ChecklistApiUrl}/{checkListId}";
			await _trelloApiService.PutDataAsync(url, request);
		}

		#region private
		private async Task CreateCheckListItemAsync(string checkListId, CreateChecklistItemRequest request)
		{
			var url = $"{ChecklistApiUrl}/{checkListId}/checkItems";
			await _trelloApiService.PostDataAsync(url, request);
		}

		private async Task CreateCheckListItemsAsync(CreateChecklistItemsRequest request)
		{
			if (!request?.Items?.Any() ?? false)
				return;
			if (request.DeleteListFirst)
				await DeleteCheckListItemsAsync(request.CheckListId);

			foreach (var item in request.Items)
				await CreateCheckListItemAsync(request.CheckListId, item);
		}

		private async Task DeleteCheckListItemAsync(string checkListId, string checkListItemId)
		{
			var url = $"{ChecklistApiUrl}/{checkListId}/checkItems/{checkListItemId}";
			await _trelloApiService.DeleteDataAsync(url);
		}

		private async Task DeleteCheckListItemsAsync(string checkListId)
		{
			var checkListItems = await _trelloQueryService.GetChecklistItemsAsync(checkListId);
			if (checkListItems?.Any() ?? false)
			{
				foreach (var item in checkListItems)
				{
					await DeleteCheckListItemAsync(checkListId, item.Id);
				}
			}
		}
		#endregion
	}
}
