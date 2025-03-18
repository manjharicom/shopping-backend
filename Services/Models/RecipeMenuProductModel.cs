using AutoMapper;
using Data.Entities;
using Services.Boundaries;

namespace Services.Models
{
	public class RecipeMenuProductModel : IMapTo<RecipeMenuProduct>
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public string Measurement { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<RecipeMenuProductModel, RecipeMenuProduct>();
		}
	}
}
