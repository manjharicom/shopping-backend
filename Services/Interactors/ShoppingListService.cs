using AutoMapper;
using Data.Boundaries;
using Services.Boundaries;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Interactors
{
    public class ShoppingListService : IShoppingListService
	{
		private readonly IShoppingListRepository _repo;
		private readonly IMapper _mapper;

		public ShoppingListService(IShoppingListRepository repo , IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}
		
		public async Task<AddUpdateResponse> CreateShoppingListAsync(CreateShoppingListRequest request)
		{
			var ret = await _repo.CreateShoppingListAsync(request.SuperMarketId, request.CheckListId, request.BoardId, request.Name);
			var response = new AddUpdateResponse { ResponseCode = ret, ResponseMessage = "Shopping List Successfully Created" };

			if (ret == -1)
				response.ResponseMessage = "Name is Null or Empty";

			if (ret == -2)
				response.ResponseMessage = "Shopping List already exists";

			return response;
		}

		public async Task<AddUpdateResponse> UpdateShoppingListAsync(UpdateShoppingListRequest request)
		{
			var ret = await _repo.UpdateShoppingListAsync(request.ShoppingListId, request.CheckListId, request.BoardId, request.Name);
			var response = new AddUpdateResponse { ResponseCode = ret, ResponseMessage = "Area Successfully Updated" };

			if (ret == -1)
				response.ResponseMessage = "Shopping List doesn't exist";

			return response;
		}

		public async Task DeleteShoppingListAsync(int shoppingListId)
		{
			await _repo.DeleteShoppingListAsync(shoppingListId);
		}

		public async Task DeleteShoppingListItemsAsync(int shoppingListId, string shoppingListIds  = null)
		{
			IEnumerable<int> ids = null;
			if (shoppingListIds != null)
				ids = shoppingListIds.Split(',').Select(s => Convert.ToInt32(s));
			await _repo.DeleteShoppingListItemsAsync(shoppingListId, ids);
		}

		public async Task<ShoppingListModel> GetAsync(int shoppingListId)
		{
			var area = await _repo.GetAsync(shoppingListId);
			return _mapper.Map<ShoppingListModel>(area);
		}

		public async Task<IEnumerable<ShoppingListModel>> GetAsync(string shoppingListIds = null)
		{
			IEnumerable<int> ids = null;
			if (shoppingListIds != null)
				ids = shoppingListIds.Split(',').Select(s => Convert.ToInt32(s));
			var categories = await _repo.GetAsync(ids);
			return _mapper.Map<IEnumerable<ShoppingListModel>>(categories);
		}
	}
}
