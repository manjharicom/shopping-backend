using AutoMapper;
using Data.Entities;
using Newtonsoft.Json;
using Services.Boundaries;
using System.Collections.Generic;
using System.Linq;

namespace Services.Models
{
	public class MenuModel : IMapFrom<Menu>
	{
		public int MenuId { get; set; }
		public string Name { get; set; }
		public string CookingInstructions { get; set; }
		public IEnumerable<RecipeMenuProductModel> Products { get; set; }
		public IEnumerable<RecipeModelSmall> Recipes { get; set; }

		public IEnumerable<Ingredient> Ingredients => GetIngredients();
		public IEnumerable<CookingInstruction> CookingInstructionsList => GetCookingInstructions();

		public void Mapping(Profile profile)
		{
			profile.CreateMap<Menu, MenuModel>()
				.ForMember(d => d.Products, o => o.Ignore())
				.AfterMap((s, d) =>
				{
					if (!string.IsNullOrEmpty(s.MenuProductsJson))
						d.Products = JsonConvert.DeserializeObject<IEnumerable<RecipeMenuProductModel>>(s.MenuProductsJson);
					if (!string.IsNullOrEmpty(s.MenuRecipesJson))
						d.Recipes = JsonConvert.DeserializeObject<IEnumerable<RecipeModelSmall>>(s.MenuRecipesJson);
				});
		}

		private IEnumerable<Ingredient> GetIngredients()
		{
			var list = new List<Ingredient>
			{
				new Ingredient
				{
					Id = MenuId,
					Name = Name,
					Products = Products
				}
			};
			if (Recipes?.Any() ?? false)
				list.AddRange(Recipes.Select(r => new Ingredient
				{
					Id = r.RecipeId,
					Name = r.Name,
					Products = r.Products
				}));
			return list;
		}

		private IEnumerable<CookingInstruction> GetCookingInstructions()
		{
			var list = new List<CookingInstruction>
			{
				new CookingInstruction
				{
					Id= MenuId,
					Name = Name,
					CookingInstructions = CookingInstructions
				}
			};

			if (Recipes?.Any() ?? false)
				list.AddRange(Recipes.Select(r => new CookingInstruction
				{
					Id = r.RecipeId,
					Name = r.Name,
					CookingInstructions = r.CookingInstructions
				}));

			return list;
		}
	}
}
