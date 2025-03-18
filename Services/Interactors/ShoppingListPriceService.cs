using AutoMapper;
using Data.Boundaries;
using Data.Entities;
using Services.Boundaries;
using Services.Helpers;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interactors
{
	public class ShoppingListPriceService : IShoppingListPriceService
	{
		private readonly IShoppingListPriceRepository _shoppingListPriceRepository;
		private readonly IMapper _mapper;

		public ShoppingListPriceService(IShoppingListPriceRepository shoppingListPriceRepository
			, IMapper mapper)
		{
			_shoppingListPriceRepository = shoppingListPriceRepository;
			_mapper = mapper;
		}

		public async Task<AddUpdateResponse> AddShoppingListPriceAsync(AddShoppingListPriceRequestModel request)
		{
			var prices = _mapper.Map<IEnumerable<AddShoppingListPriceRequest>>(request.Prices);

			var shoppingDate = DateTimeZoneHelper.ConvertTimeFromUtc(request.ShoppingDate);
			var ret = await _shoppingListPriceRepository.AddShoppingListPriceAsync(request.ShoppingListId, shoppingDate.Date, prices);

			var response = new AddUpdateResponse{ ResponseCode = ret, ResponseMessage = "Prices Successfully Added" };
			
			if (ret == -1)
				response.ResponseMessage = "Shoppinglist doesn't exist";

			if (ret == -2)
				response.ResponseMessage = "Prices array is empty";

			return response;
		}
	}
}
