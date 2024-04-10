using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace lab2
{
    public class JsonAnimalConverter : JsonConverter<Animals>
    {
        public override Animals Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                var root = doc.RootElement;
                if (root.TryGetProperty("type", out var typeProp))
                {
                    switch (typeProp.GetString())
                    {
                        case "Cat":
                            var catData = root.GetProperty("data").GetRawText();
                            return JsonSerializer.Deserialize<Cat>(catData, options);
                        case "Dog":
                            var dogData = root.GetProperty("data").GetRawText();
                            return JsonSerializer.Deserialize<Dog>(dogData, options);
                        case "Parrot":
                            var parrotData = root.GetProperty("data").GetRawText();
                            return JsonSerializer.Deserialize<Parrot>(parrotData, options);
                        default:
                            throw new JsonException("Unknown animal type");
                    }
                }
                else
                {
                    throw new JsonException("Missing 'type' property");
                }
            }
        }


        public override void Write(Utf8JsonWriter writer, Animals value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            if (value is Cat cat)
            {
                writer.WriteString("type", "Cat");
                writer.WritePropertyName("data");
                JsonSerializer.Serialize(writer, cat, cat.GetType(), options);
            }
            else if (value is Dog dog)
            {
                writer.WriteString("type", "Dog");
                writer.WritePropertyName("data");
                JsonSerializer.Serialize(writer, dog, dog.GetType(), options);
            }
            else if (value is Parrot parrot)
            {
                writer.WriteString("type", "Parrot");
                writer.WritePropertyName("data");
                JsonSerializer.Serialize(writer, parrot, parrot.GetType(), options);
            }
            else
            {
                throw new JsonException("Unknown animal type");
            }
            writer.WriteEndObject();
        }
    }
}
