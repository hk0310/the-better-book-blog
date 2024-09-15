using BookCatalog.API.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BookCatalog.API.Serialization
{
    public class IsbnConverter : JsonConverter<Isbn>
    {
        public override Isbn Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            new Isbn(reader.GetString());

        public override void Write(Utf8JsonWriter writer, Isbn value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.Number);
    }
}
