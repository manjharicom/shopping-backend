using Data.Entities;
using Services.Boundaries;

namespace Services.Models
{
    public class SuperMarketModel : IMapFrom<SuperMarket>
    {
        public int SuperMarketId { get; set; }
        public string Name { get; set; }
    }
}
