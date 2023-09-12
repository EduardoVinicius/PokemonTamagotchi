using PokemonTamagotchi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PokemonTamagotchi
{
    public class PokemonJsonConverter : JsonConverter<PokePet>
    {
        public override PokePet? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDocument = JsonDocument.ParseValue(ref reader);
            var root = jsonDocument.RootElement;

            var abilities = root.GetProperty("abilities").EnumerateArray()
                .Select(a => new PokeAbility
                {
                    Name = a.GetProperty("ability").GetProperty("name").GetString(),
                    Url = a.GetProperty("ability").GetProperty("url").GetString()
                }).ToList();

            return new PokePet
            {
                Id = root.GetProperty("order").GetInt32(),
                Name = root.GetProperty("name").GetString(),
                Weight = root.GetProperty("weight").GetInt32(),
                Height = root.GetProperty("height").GetInt32(),
                Abilities = abilities!
            };
        }

        public override void Write(Utf8JsonWriter writer, PokePet value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
