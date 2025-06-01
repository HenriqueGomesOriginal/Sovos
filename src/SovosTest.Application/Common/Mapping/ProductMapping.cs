using AutoMapper;
using SovosTest.Application.Common.Interfaces;
using System.Reflection;

namespace SovosTest.Application.Common.Mapping;

// MappingProfile class extends AutoMapper Profile to define
// custom mapping configurations using IMapFrom<T> interface.
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Apply mapping configurations from the executing assembly.
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    // ApplyMappingsFromAssembly scans the assembly for types that implement
    // IMapFrom<T> interface and creates corresponding mappings.
    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces()
                .Where(i => i.IsGenericType)
                .Any(i => i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod("Mapping")
                ?? type.GetInterface("IMapFrom`1")!.GetMethod("Mapping");

            // Invoke the Mapping method on the instance of the type.
            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}