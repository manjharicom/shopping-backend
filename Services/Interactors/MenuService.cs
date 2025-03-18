using AutoMapper;
using Azure.Core;
using Data.Boundaries;
using Data.Entities;
using Services.Boundaries;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interactors
{
	public class MenuService : IMenuService
	{
		private readonly IMenuRepository _repo;
		private readonly IMapper _mapper;

		public MenuService(IMenuRepository repo, IMapper mapper)
		{
			_repo = repo; ;
			_mapper = mapper;
		}

		public async Task<AddUpdateResponse> AddAsync(AddRecipeMenuRequest request)
		{
			var ret = await _repo.AddAsync(request.Name, request.CookingInstructions);
			var response = new AddUpdateResponse { ResponseCode = ret, ResponseMessage = "Menu Successfully Added" };
			if (ret == -1)
				response.ResponseMessage = "Name is Null or Empty";

			if (ret == -2)
				response.ResponseMessage = "Menu already exists";

			return response;
		}

		public async Task<AddUpdateResponse> AddProductAsync(int menuId, RecipeMenuProductModel request)
		{
			await _repo.AddProductAsync(menuId, request.ProductId, request.Measurement);
			var response = new AddUpdateResponse { ResponseCode = 0, ResponseMessage = "Product Successfully Added" };
			return response;
		}

		public async Task<AddUpdateResponse> AddRecipeAsync(int menuId, int recipeId)
		{
			await _repo.AddRecipeAsync(menuId, recipeId);
			var response = new AddUpdateResponse { ResponseCode = 0, ResponseMessage = "Recipe Successfully Added" };
			return response;
		}

		public async Task<AddUpdateResponse> DeleteProductAsync(int menuId, int productId)
		{
			await _repo.DeleteProductAsync(menuId, productId);
			var response = new AddUpdateResponse { ResponseCode = 0, ResponseMessage = "Product Successfully Removed" };
			return response;
		}

		public async Task<AddUpdateResponse> DeleteRecipeAsync(int menuId, int recipeId)
		{
			await _repo.DeleteRecipeAsync(menuId, recipeId);
			var response = new AddUpdateResponse { ResponseCode = 0, ResponseMessage = "Recipe Successfully Removed" };
			return response;
		}

		public async Task<IEnumerable<MenuModel>> GetAsync(string name)
		{
			var ret = await _repo.GetAsync(name);
			return _mapper.Map<IEnumerable<MenuModel>>(ret);
		}

		public async Task<MenuModel> GetAsync(int menuId)
		{
			var ret = await _repo.GetAsync(menuId);
			return _mapper.Map<MenuModel>(ret);
		}

		public async Task<AddUpdateResponse> UpdateAsync(UpdateMenuRequest request)
		{
			var ret = await _repo.UpdateAsync(request.MenuId, request.Name, request.CookingInstructions);
			var response = new AddUpdateResponse { ResponseCode = ret, ResponseMessage = "Menu Successfully Updated" };
			if (ret == -1)
				response.ResponseMessage = "Menu doesn't exist";
			return response;
		}
	}
}
