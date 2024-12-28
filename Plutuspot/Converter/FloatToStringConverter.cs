using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Plutuspot.Converter
{
    public class FloatToStringConverter : JsonConverter<string>
    {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Number)
            {
                decimal decValue = reader.GetDecimal();
                return decValue.ToString();
            }
            throw new JsonException("Expected a number but got " + reader.TokenType);
        }

        public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(double.Parse(value));
        }
    }
}