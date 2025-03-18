using AutoMapper;
using Dapper;
using Data.Boundaries;
using Data.Entities;
using Data.Repositories;
using Services.Boundaries;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Interactors
{
	public class SuperMarketService : ISuperMarketService
	{
		private readonly ISuperMarketRepository _superMarketRepository;
		private readonly IMapper _mapper;

		public SuperMarketService(ISuperMarketRepository superMarketRepository
			, IMapper mapper)
		{
			_superMarketRepository = superMarketRepository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<SuperMarketModel>> GetSuperMarketsAsync(string superMarketIds = null)
		{
			IEnumerable<int> catIds = null;
			if (superMarketIds != null)
				catIds = superMarketIds.Split(',').Select(s => Convert.ToInt32(s));
			var products = await _superMarketRepository.GetSuperMarketsAsync(catIds);
			return _mapper.Map<IEnumerable<SuperMarketModel>>(products);
		}

		public async Task<SuperMarketModel> GetSuperMarketAsync(int superMarketId)
		{
			var superMarket = await _superMarketRepository.GetSuperMarketAsync(superMarketId);
			return _mapper.Map<SuperMarketModel>(superMarket);
		}

		public async Task<IEnumerable<CategorySuperMarketModel>> GetCategorySuperMarketsAsync(int superMarketId)
		{
			var catSuperMarkets = await _superMarketRepository.GetCategorySuperMarketsAsync(superMarketId);
			return _mapper.Map<IEnumerable<CategorySuperMarketModel>>(catSuperMarkets);
		}

		public async Task<AddUpdateResponse> AddAsync(string name)
		{
			var ret = await _superMarketRepository.AddAsync(name);
			var response = new AddUpdateResponse { ResponseCode = ret, ResponseMessage = "SuperMarket Successfully Added" };

			if (ret == -1)
				response.ResponseMessage = "Name is Null or Empty";

			if (ret == -2)
				response.ResponseMessage = "SuperMarket already exists";

			return response;
		}

		public async Task<AddUpdateResponse> UpdateAsync(UpdateRequest request)
		{
			var ret = await _superMarketRepository.UpdateAsync(request.Id, request.Name);
			var response = new AddUpdateResponse { ResponseCode = ret, ResponseMessage = "SuperMarket Successfully Updated" };

			if (ret == -1)
				response.ResponseMessage = "SuperMarket doesn't exist";

			return response;
		}

		public async Task DeleteAsync(int superMarketId)
		{
			await _superMarketRepository.DeleteAsync(superMarketId);
		}

		public async Task<AddUpdateResponse> MergeCategorySuperMarketAsync(int superMarketId, IEnumerable<CategorySuperMarketRequest> categorySuperMarkets)
		{
			categorySuperMarkets = categorySuperMarkets.Where(c => c.Include);
			var catSuperMarkets = _mapper.Map<IEnumerable<UdtCategorySuperMarket>>(categorySuperMarkets);
			var superMarket = await GetSuperMarketAsync(superMarketId);
			await _superMarketRepository.MergeCategorySuperMarketAsync(superMarketId, catSuperMarkets);
			return new AddUpdateResponse { ResponseCode = 0, ResponseMessage = $"Shop {superMarket.Name} Successfully Merged" };
		}

		public async Task SetSuperMarketAsync(int superMarketId)
		{
			await _superMarketRepository.SetSuperMarketAsync(superMarketId);
		}

	}
}
