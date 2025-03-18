using AutoMapper;
using Data.Entities;
using Services.Boundaries;

namespace Services.Models
{
	public class CategorySuperMarketRequest : IMapTo<UdtCategorySuperMarket>
	{
		public int CategoryId { get; set; }
		public string AisleLabel { get; set; }
		public int? Sequence { get; set; }
		public bool Include { get; set; } = true;

		public void Mapping(Profile profile)
		{
			profile.CreateMap<CategorySuperMarketRequest, UdtCategorySuperMarket>()
				.ForMember(d => d.Sequence, o => o.Ignore())
				.AfterMap((s, d) =>
				{
					if (s.Sequence.HasValue)
						d.Sequence= s.Sequence.Value;
					else
						d.Sequence= 0;
				});
		}
	}
}
