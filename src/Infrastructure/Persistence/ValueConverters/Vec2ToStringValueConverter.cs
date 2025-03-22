using System.Numerics;
using Domain.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.ValueConverters;

public sealed class Vec2ToStringValueConverter()
    : ValueConverter<Vector2, string>(
        to => to.ToVector2String(),
        from => from.ParseVector2());