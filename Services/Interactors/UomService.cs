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
	public class UomService : IUomService
	{
		private readonly IUomRepository _uomRepository;
		private readonly IMapper _mapper;

		public UomService(IUomRepository uomRepository, IMapper mapper)
		{
			_uomRepository = uomRepository;
			_mapper = mapper;
		}

		public async Task<AddUpdateResponse> AddUomAsync(AddUomRequest request)
		{
			var ret = await _uomRepository.AddUomAsync(request.Name, request.AllowDecimalQuantity);
			var response = new AddUpdateResponse { ResponseCode = ret, ResponseMessage = "Uom Successfully Added" };

			if (ret == -1)
				response.ResponseMessage = "Name is Null or Empty";

			if (ret == -2)
				response.ResponseMessage = "Uom already exists";

			return response;
		}

		public async Task<UnitOfMeasureModel> GetUomAsync(int uom)
		{
			var area = await _uomRepository.GetUomAsync(uom);
			return _mapper.Map<UnitOfMeasureModel>(area);
		}

		public async Task<IEnumerable<UnitOfMeasureModel>> GetUomsAsync(string uoms = null)
		{
			IEnumerable<int> uomList = null;
			if (uoms != null)
				uomList = uoms.Split(',').Select(s => Convert.ToInt32(s));
			var list = await _uomRepository.GetUomsAsync(uomList);
			return _mapper.Map<IEnumerable<UnitOfMeasureModel>>(list);
		}

		public async Task<AddUpdateResponse> UpdateUomAsync(UpdateUomRequest request)
		{
			var ret = await _uomRepository.UpdateUomAsync(request.Id, request.Name, request.AllowDecimalQuantity);
			var response = new AddUpdateResponse { ResponseCode = ret, ResponseMessage = "Uom Successfully Updated" };

			if (ret == -1)
				response.ResponseMessage = "Name is Null or Empty";

			if (ret == -2)
				response.ResponseMessage = "Uom doesn't exist";

			return response;
		}
	}
}
