using System.Text.Json;
using System.Text.Json.Serialization;

namespace lab3.converter;

public class DateOnlyConverter : JsonConverter<DateOnly>
{
    private readonly string _serializationFormat;

    public DateOnlyConverter() : this(null)
    {
    }

    private DateOnlyConverter(string? serializationFormat)
    {
        this._serializationFormat = serializationFormat ?? "yyyy-MM-dd";
    }

    public override DateOnly Read(ref Utf8JsonReader reader, 
        Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        return DateOnly.Parse(value!);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, 
        JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(_serializationFormat));
}