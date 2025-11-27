using Newtonsoft.Json.Converters;
using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

public class CustomDateTimeConverter : JsonConverter<DateTime?>
{
    private const string Format = "yyyy-MM-dd HH:mm:ss";

    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var str = reader.GetString();
            if (DateTime.TryParseExact(str, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                return dt;
        }
        return null;
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value.HasValue)
            writer.WriteStringValue(value.Value.ToString(Format));
        else
            writer.WriteNullValue();
    }
}
public class CustomDateTimeConverter2 : IsoDateTimeConverter
{
    public CustomDateTimeConverter2()
    {
        DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
    }
}