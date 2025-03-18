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
	public class CategoryRepository : SqlRepository, ICategoryRepository
	{
		public CategoryRepository(IDbConnection dbConnection) : base(dbConnection)
		{
		}

		public async Task<IEnumerable<Category>> GetCategoriesAsync(IEnumerable<int> categoryIds = null, bool includeProducts = false)
		{
			var parameters = new DynamicParameters();
			parameters.Add("IncludeProducts", includeProducts);
			if (categoryIds?.Any() ?? false)
			{
				var cats = categoryIds.GetTableValuedParameter("dbo.UdtId");
				parameters.Add("Categories", cats);
			}
			return await QueryAsync<Category>("[dbo].[GetCategories]", parameters);
		}

		public async Task<Category> GetCategoryAsync(int categoryId)
		{
			var parameters = new DynamicParameters();
			parameters.Add("CategoryId", categoryId);
			return await QuerySingleAsync<Category>("[dbo].[GetCategory]", parameters);
		}

		public async Task<int> AddCategoryAsync(string name)
		{
			var parameters = new DynamicParameters();
			parameters.Add("Name", name);
			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);

			await ExecuteAsync("[dbo].[AddCategory]", parameters);

			var returnCode = parameters.Get<int>("ReturnCode");
			return returnCode;
		}

		public async Task<int> UpdateCategoryAsync(int categoryId, string name)
		{
			var parameters = new DynamicParameters();
			parameters.Add("CategoryId", categoryId);
			parameters.Add("Name", name);
			parameters.Add("ReturnCode", null, DbType.Int32, ParameterDirection.Output);
			
			await ExecuteAsync("[dbo].[UpdateCategory]", parameters);
			
			var returnCode = parameters.Get<int>("ReturnCode");
			return returnCode;
		}
	}
}
