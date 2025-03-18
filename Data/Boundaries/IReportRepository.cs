using Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Boundaries
{
	public interface IReportRepository
	{
		Task<IEnumerable<FullProduct>> GetDataDump();
	}
}
