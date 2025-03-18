using AutoMapper;
using Data.Entities;
using Newtonsoft.Json;
using Services.Boundaries;
using System.Collections.Generic;

namespace Services.Models
{
	public class RecipeModel : IMapFrom<Recipe>
	{
		public int RecipeId { get; set; }
		public string Name { get; set; }
		public string CookingInstructions { get; set; }
		public IEnumerable<RecipeMenuProductModel> Products { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Recipe, RecipeModel>()
				.ForMember(d => d.Products, o => o.Ignore())
				.AfterMap((s, d) =>
				{
					if (!string.IsNullOrEmpty(s.RecipeProductsJson))
						d.Products = JsonConvert.DeserializeObject<IEnumerable<RecipeMenuProductModel>>(s.RecipeProductsJson);
				});
		}
	}
}
