using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System.Globalization;

namespace HRDataProcessing;

public class CustomDateTimeConverter : ITypeConverter
{
    public object? ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData)
    {
        string[] formats = ["dd-MMM-yy", "dd-MMM-yyyy", "dd-MM-yyyy", "yyyy-MM-dd"];

        if (DateTime.TryParseExact(text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
        {
            return result;
        }

        throw new CsvHelper.TypeConversion.TypeConverterException(
            this,
            memberMapData,
            text,
            row.Context
        );
    }

    public string? ConvertToString(object? value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is DateTime dateTime)
        {
            return dateTime.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
        }

        throw new CsvHelper.TypeConversion.TypeConverterException(
            this,
            memberMapData,
            value?.ToString(),
            row.Context
        );
    }
}
