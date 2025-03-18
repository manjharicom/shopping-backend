using Dapper;
using Data.Boundaries;
using Data.Entities;
using Data.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
	public class AreaRepository : SqlRepository, IAreaRepository
	{
		public AreaRepository(IDbConnection dbConnection) : base(dbConnection)
		{
		}

		public async Task<int> AddAreaAsync(string name)
		{
			var parameters = new DynamicParameters();
			parameters.Add("Name", name);
			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);

			await ExecuteAsync("[dbo].[AddArea]", parameters);

			var returnCode = parameters.Get<int>("ReturnCode");
			return returnCode;
		}

		public async Task<IEnumerable<Area>> GetAreasAsync(IEnumerable<int> areaIds = null, bool includeProducts = false)
		{
			var parameters = new DynamicParameters();
			parameters.Add("IncludeProducts", includeProducts);
			if (areaIds?.Any() ?? false)
			{
				var cats = areaIds.GetTableValuedParameter("dbo.UdtId");
				parameters.Add("Areas", cats);
			}
			return await QueryAsync<Area>("[dbo].[GetAreas]", parameters);
		}

		public async Task<Area> GetAreaAsync(int areaId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("areaId", areaId);
			return await QuerySingleAsync<Area>("[dbo].[GetArea]", parameters);
		}

		public async Task<int> UpdateAreaAsync(int areaId, string name)
		{
			var parameters = new DynamicParameters();
			parameters.Add("AreaId", areaId);
			parameters.Add("Name", name);
			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);

			await ExecuteAsync("[dbo].[UpdateArea]", parameters);

			var returnCode = parameters.Get<int>("ReturnCode");
			return returnCode;
		}
	}
}
