using AutoMapper;
using Data.Entities;
using Newtonsoft.Json;
using Services.Boundaries;
using System.Collections.Generic;
using System.Linq;

namespace Services.Models
{
	public class CategoryModel : IMapFrom<Category>
	{
		public int CategoryId { get; set; }
		public string Name { get; set; }
		public bool IsShipped { get; set; }
		public IEnumerable<ProductModel> Products { get; set; }
		public bool HasProducts => (Products?.Any() ?? false);

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Category, CategoryModel>()
				.ForMember(d => d.Products, o => o.Ignore())
				.AfterMap((s, d) =>
				{
					if (!string.IsNullOrEmpty(s.ProductsJson))
					{
						d.Products = JsonConvert.DeserializeObject<IEnumerable<ProductModel>>(s.ProductsJson);
					}
				});
		}
	}
}
