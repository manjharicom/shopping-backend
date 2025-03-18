using AutoMapper;
using Azure.Core;
using Data.Boundaries;
using Data.Entities;
using Services.Boundaries;
using Services.Enums;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Interactors
{
	public class ProductService : IProductService
	{
		private readonly IProductRepository _productRepository;
		private readonly ITrelloQueryService _trelloQueryService;
		private readonly IShoppingListService _shoppingListService;
		private readonly IMapper _mapper;

		public ProductService(IProductRepository productRepository
			, ITrelloQueryService trelloQueryService
			, IShoppingListService shoppingListService
			, IMapper mapper)
		{
			_productRepository = productRepository;
			_trelloQueryService = trelloQueryService;
			_shoppingListService = shoppingListService;
			_mapper = mapper;
		}

		public async Task<FullProductModel> GetProductAsync(int productId)
		{
			var product = await _productRepository.GetProductAsync(productId);
			return _mapper.Map<FullProductModel>(product);
		}

		public async Task<IEnumerable<FullProductModel>> GetProductsAsync(OrderBy orderBy, string searchText = null, int? categoryId = null, int? areaId = null)
		{
			var products = await _productRepository.GetProductsAsync((int)orderBy, searchText, categoryId, areaId);
			return _mapper.Map<IEnumerable<FullProductModel>>(products);
		}

		public async Task<IEnumerable<ProductModel>> GetProductsForCategoryAsync(int categoryId)
		{
			var products = await _productRepository.GetProductsForCategoryAsync(categoryId);
			return _mapper.Map<IEnumerable<ProductModel>>(products);
		}

		public async Task<IEnumerable<ProductModel>> SearchProductsAsync(string searchText, int? categoryId = null, int? areaId = null)
		{
			var products = await _productRepository.SearchProductsAsync(searchText, categoryId, areaId);
			return _mapper.Map<IEnumerable<ProductModel>>(products);
		}

		public async Task AddProductToListAsync(int shoppingListId, int productId, int quantity = 1, bool purchased = false)
		{
			await _productRepository.AddProductToListAsync(shoppingListId, productId, quantity, purchased);
		}

		public async Task UpdateProductQuanityListAsync(int shoppingListId, int productId, int quantity)
		{
			await _productRepository.UpdateProductQuanityListAsync(shoppingListId, productId, quantity);
		}

		public async Task RemoveProductFromListAsync(int shoppingListId, int productId)
		{
			await _productRepository.RemoveProductFromListAsync(shoppingListId, productId);
		}

		public async Task<IEnumerable<ProductModel>> GetShoppingListProductsAsync(int? shoppingListId = null, bool isPurchased = false)
		{
			var shoppingListProducts = await _productRepository.GetShoppingListProductsAsync(shoppingListId);
			if (isPurchased && shoppingListId.HasValue && (shoppingListProducts?.Any() ?? false))
			{
				var shoppingList = await _shoppingListService.GetAsync(shoppingListId.Value);
				var checkListItems = await _trelloQueryService.GetChecklistItemsAsync(shoppingList.CheckListId);
				if (checkListItems?.Any() ?? false)
				{
					var completed = checkListItems.Where(c => c.State == "complete");
					var productIds = shoppingListProducts.Where(s => completed.Any(c => c.Name.Substring(0, c.Name.IndexOf(" (")) == s.Name && c.State == "complete")).Select(s => s.ProductId);

					shoppingListProducts = shoppingListProducts.Where(p => p.Purchased || productIds.Any(i => i == p.ProductId));
				}
			}
			return _mapper.Map<IEnumerable<ProductModel>>(shoppingListProducts);
		}

		public async Task DeleteShoppingListItemsAsync(int shoppingListId, IEnumerable<int> productIds)
		{
			await _productRepository.DeleteShoppingListItemsAsync(shoppingListId, productIds);
		}

		public async Task<AddUpdateResponse> AddProductAsync(AddProductRequest request)
		{
			var ret = await _productRepository.AddProductAsync(request.CategoryId, request.AreaId, request.Name, request.UomId, request.PriceUomId);
			var response = new AddUpdateResponse { ResponseCode = ret, ResponseMessage = $"Product {request.Name} Successfully Added" };

			if (ret == -1)
				response.ResponseMessage = "Name is Null or Empty";

			if (ret == -2)
				response.ResponseMessage = "Category or Area doesn't exist";

			if (ret == -3)
				response.ResponseMessage = "Product already exists";
			return response;
		}

		public async Task<AddUpdateResponse> UpdateProductAsync(UpdateProductRequest request)
		{
			var ret = await _productRepository.UpdateProductAsync(request.ProductId, request.CategoryId, request.AreaId, request.Name, request.UomId, request.PriceUomId);
			var response = new AddUpdateResponse { ResponseCode = ret, ResponseMessage = $"Product {request.Name} Successfully Updated" };
			
			if (ret == -1)
				response.ResponseMessage = "Category doesn't exist";

			if (ret == -2)
				response.ResponseMessage = "Area doesn't exist";
			return response;
		}
	}
}
