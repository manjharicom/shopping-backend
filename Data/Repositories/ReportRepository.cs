using Data.Boundaries;
using Data.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Data.Repositories
{
	public class ReportRepository : SqlRepository, IReportRepository
	{
		public ReportRepository(IDbConnection connection) : base(connection)
		{
		}

		public async Task<IEnumerable<FullProduct>> GetDataDump()
		{
			return await QueryAsync<FullProduct>("[dbo].[DataDump]");
		}
	}
}
