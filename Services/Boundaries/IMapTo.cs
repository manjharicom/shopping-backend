using AutoMapper;

namespace Services.Boundaries
{
	public interface IMapTo<T>
	{
		void Mapping(Profile profile) => profile.CreateMap(GetType(), typeof(T));
	}
}
