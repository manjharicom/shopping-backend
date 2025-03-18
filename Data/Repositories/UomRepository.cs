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
	public class UomRepository : SqlRepository, IUomRepository
	{
		public UomRepository(IDbConnection connection) : base(connection)
		{
		}

		public async Task<int> AddUomAsync(string name, bool allowDecimalQuantity)
		{
			var parameters = new DynamicParameters();
			parameters.Add("Name", name);
			parameters.Add("AllowDecimalQuantity", allowDecimalQuantity);
			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);

			await ExecuteAsync("[dbo].[AddUom]", parameters);

			var returnCode = parameters.Get<int>("ReturnCode");
			return returnCode;
		}

		public async Task<UnitOfMeasure> GetUomAsync(int uom)
		{
			var parameters = new DynamicParameters();
			parameters.Add("uomId", uom);
			return await QuerySingleAsync<UnitOfMeasure>("[dbo].[GetUom]", parameters);
		}

		public async Task<IEnumerable<UnitOfMeasure>> GetUomsAsync(IEnumerable<int> uoms = null)
		{
			var parameters = new DynamicParameters();
			if (uoms?.Any() ?? false)
			{
				var uomList = uoms.GetTableValuedParameter("dbo.UdtId");
				parameters.Add("Uoms", uomList);
			}
			return await QueryAsync<UnitOfMeasure>("[dbo].[GetUoms]", parameters);
		}

		public async Task<int> UpdateUomAsync(int uomId, string name, bool allowDecimalQuantity)
		{
			var parameters = new DynamicParameters();
			parameters.Add("UomId", uomId);
			parameters.Add("Name", name);
			parameters.Add("AllowDecimalQuantity", allowDecimalQuantity);
			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);

			await ExecuteAsync("[dbo].[UpdateUom]", parameters);

			var returnCode = parameters.Get<int>("ReturnCode");
			return returnCode;
		}
	}
}
