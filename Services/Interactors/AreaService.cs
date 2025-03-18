using AutoMapper;
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
	public class AreaService : IAreaService
	{
		private readonly IAreaRepository _areaRepository;
		private readonly IMapper _mapper;
		
		public AreaService(IAreaRepository areaRepository, IMapper mapper)
		{
			_areaRepository = areaRepository;;
			_mapper = mapper;
		}

		public async Task<AddUpdateResponse> AddAreaAsync(string name)
		{
			var ret = await _areaRepository.AddAreaAsync(name);
			var response = new AddUpdateResponse { ResponseCode = ret, ResponseMessage = "Storage Area Successfully Added" };

			if (ret == -1)
				response.ResponseMessage = "Name is Null or Empty";

			if (ret == -2)
				response.ResponseMessage = "Storage Area already exists";

			return response;
		}

		public async Task<AreaModel> GetAreaAsync(int areaId)
		{
			var area = await _areaRepository.GetAreaAsync(areaId);
			return _mapper.Map<AreaModel>(area);
		}

		public async Task<IEnumerable<AreaModel>> GetAreasAsync(string areaIds = null, bool incldueProducts = false)
		{
			IEnumerable<int> ids = null;
			if (areaIds != null)
				ids = areaIds.Split(',').Select(s => Convert.ToInt32(s));
			var categories = await _areaRepository.GetAreasAsync(ids, incldueProducts);
			return _mapper.Map<IEnumerable<AreaModel>>(categories);
		}

		public async Task<AddUpdateResponse> UpdateAreaAsync(UpdateRequest request)
		{
			var ret = await _areaRepository.UpdateAreaAsync(request.Id, request.Name);
			var response = new AddUpdateResponse { ResponseCode = ret, ResponseMessage = "Area Successfully Updated" };

			if (ret == -1)
				response.ResponseMessage = "Area doesn't exist";

			if (ret == -2)
				response.ResponseMessage = "Cannot Update Shipped Area";

			return response;
		}
	}
}
