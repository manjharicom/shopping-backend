using AutoMapper;
using Data.Boundaries;
using Services.Boundaries;
using Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interactors
{
	public class RecipeService : IRecipeService
	{
		private readonly IRecipeRepository _repo;
		private readonly IMapper _mapper;

		public RecipeService(IRecipeRepository repo, IMapper mapper)
		{
			_repo = repo; ;
			_mapper = mapper;
		}

		public async Task<AddUpdateResponse> AddAsync(AddRecipeMenuRequest request)
		{
			var ret = await _repo.AddAsync(request.Name, request.CookingInstructions);
			var response = new AddUpdateResponse { ResponseCode = ret, ResponseMessage = "Recipe Successfully Added" };
			if (ret == -1)
				response.ResponseMessage = "Name is Null or Empty";

			if (ret == -2)
				response.ResponseMessage = "Recipe already exists";

			return response;
		}

		public async Task<AddUpdateResponse> AddProductAsync(int recipeId, RecipeMenuProductModel request)
		{
			await _repo.AddProductAsync(recipeId, request.ProductId, request.Measurement);
			var response = new AddUpdateResponse { ResponseCode = 0, ResponseMessage = "Product Successfully Added" };
			return response;
		}

		public async Task<AddUpdateResponse> DeleteProductAsync(int recipeId, int productId)
		{
			await _repo.DeleteProductAsync(recipeId, productId);
			var response = new AddUpdateResponse { ResponseCode = 0, ResponseMessage = "Product Successfully Removed" };
			return response;
		}

		public async Task<IEnumerable<RecipeModel>> GetAsync(string name)
		{
			var ret = await _repo.GetAsync(name);
			return _mapper.Map<IEnumerable<RecipeModel>>(ret);
		}

		public async Task<RecipeModel> GetAsync(int recipeId)
		{
			var ret = await _repo.GetAsync(recipeId);
			return _mapper.Map<RecipeModel>(ret);
		}

		public async Task<AddUpdateResponse> UpdateAsync(UpdateRecipeRequest request)
		{
			var ret = await _repo.UpdateAsync(request.RecipeId, request.Name, request.CookingInstructions);
			var response = new AddUpdateResponse { ResponseCode = ret, ResponseMessage = "Recipe Successfully Updated" };
			if (ret == -1)
				response.ResponseMessage = "Recipe doesn't exist";
			return response;
		}

		public async Task<IEnumerable<RecipeModel>> SearchAsync(string searchText)
		{
			var ret = await _repo.SearchAsync(searchText);
			return _mapper.Map<IEnumerable<RecipeModel>>(ret);
		}
	}
}
