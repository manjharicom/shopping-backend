using AutoMapper;
using Data.Entities;
using Services.Boundaries;

namespace Services.Models
{
	public class ShoppingListPriceRequest : IMapTo<AddShoppingListPriceRequest>
	{
		public int ProductId { get; set; }
		public string Comment { get; set; }
		public decimal Quantity { get; set; }
		public decimal Price { get; set; }
		public decimal Total { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<ShoppingListPriceRequest, AddShoppingListPriceRequest>();
		}
	}
}
