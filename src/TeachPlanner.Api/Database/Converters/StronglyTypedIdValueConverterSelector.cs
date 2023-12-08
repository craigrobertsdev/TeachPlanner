using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TeachPlanner.Api.Database.Converters;

// Taken from https://andrewlock.net/strongly-typed-ids-in-ef-core-using-strongly-typed-entity-ids-to-avoid-primitive-obsession-part-4/#creating-a-custom-valueconverterselector-for-strongly-typed-ids
public class StronglyTypedIdValueConverterSelector : ValueConverterSelector {
    private readonly ConcurrentDictionary<(Type ModelClrType, Type ProviderClrType), ValueConverterInfo> _converters =
        new();

    public StronglyTypedIdValueConverterSelector(ValueConverterSelectorDependencies dependencies) : base(dependencies) {
    }

    public override IEnumerable<ValueConverterInfo> Select(Type modelClrType, Type? providerClrType = null) {
        var baseConverters = base.Select(modelClrType, providerClrType);
        foreach (var converter in baseConverters) yield return converter;

        var underlyingModelType = UnwrapNullableType(modelClrType);
        var underlyingProviderType = UnwrapNullableType(providerClrType);

        if (underlyingProviderType is null || underlyingProviderType == typeof(Guid)) {
            var converterType = underlyingModelType!.GetNestedType("StronglyTypedIdEfValueConverter");

            if (converterType is not null)
                yield return _converters.GetOrAdd(
                    (underlyingModelType, typeof(Guid)),
                    k => {
                        Func<ValueConverterInfo, ValueConverter> factory =
                            info => (ValueConverter)Activator.CreateInstance(converterType, info.MappingHints)!;

                        return new ValueConverterInfo(modelClrType, typeof(Guid), factory);
                    }
                );
        }
    }

    private static Type? UnwrapNullableType(Type? type) {
        if (type is null) return null;

        return Nullable.GetUnderlyingType(type) ?? type;
    }
}