using Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Boundaries
{
	public interface IShoppingListPriceService
	{
		Task<AddUpdateResponse> AddShoppingListPriceAsync(AddShoppingListPriceRequestModel request);
	}
}
