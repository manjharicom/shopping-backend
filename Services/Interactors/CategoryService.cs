using AutoMapper;
using Data.Boundaries;
using Services.Boundaries;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Interactors
{
	public class CategoryService : ICategoryService
	{
		private readonly ICategoryRepository _categoryRepository;
		private readonly IMapper _mapper;

		public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
		{
			_categoryRepository = categoryRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<CategoryModel>> GetCategoriesAsync(string categoryIds = null, bool incldueProducts = false)
		{
			IEnumerable<int> catIds = null;
			if (categoryIds != null)
				catIds = categoryIds.Split(',').Select(s => Convert.ToInt32(s));
			var categories = await _categoryRepository.GetCategoriesAsync(catIds, incldueProducts);
			return _mapper.Map<IEnumerable<CategoryModel>>(categories);
		}

		public async Task<CategoryModel> GetCategoryAsync(int categoryId)
		{
			var category = await _categoryRepository.GetCategoryAsync(categoryId);
			return _mapper.Map<CategoryModel>(category);
		}

		public async Task<AddUpdateResponse> AddCategoryAsync(string name)
		{
			var ret = await _categoryRepository.AddCategoryAsync(name);
			var response = new AddUpdateResponse { ResponseCode = ret, ResponseMessage = "Category Successfully Added" };

			if (ret == -1)
				response.ResponseMessage = "Name is Null or Empty";

			if (ret == -2)
				response.ResponseMessage = "Category already exists";

			return response;
		}

		public async Task<AddUpdateResponse> UpdateCategoryAsync(UpdateRequest request)
		{
			var ret = await _categoryRepository.UpdateCategoryAsync(request.Id, request.Name);
			var response = new AddUpdateResponse { ResponseCode = ret, ResponseMessage = "Category Successfully Updated" };

			if (ret == -1)
				response.ResponseMessage = "Category doesn't exist";

			if (ret == -2)
				response.ResponseMessage = "Cannot Update Shipped Category";

			return response;
		}
	}
}
