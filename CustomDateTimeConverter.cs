using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Globalization;

public class CustomDateTimeConverter : ITypeConverter
{
    // Convert from string to DateTime
    public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        DateTime result;
        string[] formats = { "dd-MMM-yy", "dd-MMM-yyyy", "dd-MM-yyyy", "yyyy-MM-dd" };

        if (DateTime.TryParseExact(text, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
        {
            return result;
        }

        // Get CSV Context from IReaderRow
        var context = row.Context; 
        throw new CsvHelper.TypeConversion.TypeConverterException(
            this, 
            memberMapData, 
            text, 
            context
        );
    }

    // Convert from DateTime to string
    public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        if (value is DateTime dateTime)
        {
            return dateTime.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
        }

        // Get CSV Context from IWriterRow
        var context = row.Context; 
        throw new CsvHelper.TypeConversion.TypeConverterException(
            this, 
            memberMapData, 
            value?.ToString(), 
            context
        );
    }
}
