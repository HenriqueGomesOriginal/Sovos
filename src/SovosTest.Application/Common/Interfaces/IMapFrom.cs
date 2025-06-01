using AutoMapper;

namespace SovosTest.Application.Common.Interfaces;

// IMapFrom<T> interface is used to implement object mapping
// between different types using the AutoMapper library.
public interface IMapFrom<T>
{
    // Maps the source type (T) to the target type by creating a mapping profile.
    void Mapping(Profile profile)
    {
        profile.CreateMap(typeof(T), GetType());
    }
}
