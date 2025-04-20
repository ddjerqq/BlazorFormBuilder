using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Persistence.ValueConverters;

public sealed class DateTimeToUtcDateTimeValueConverter()
    : ValueConverter<DateTime, DateTime>(
        to => to.ToUniversalTime(),
        from => DateTime.SpecifyKind(from, DateTimeKind.Utc));