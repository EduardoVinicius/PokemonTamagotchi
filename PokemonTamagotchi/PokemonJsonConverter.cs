using PokemonTamagotchi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PokemonTamagotchi
{
    public class PokemonJsonConverter : JsonConverter<Pokemon>
    {
        public override Pokemon? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDocument = JsonDocument.ParseValue(ref reader);
            var root = jsonDocument.RootElement;

            var abilities = root.GetProperty("abilities").EnumerateArray()
                .Select(a => a.GetProperty("ability").GetProperty("name").GetString()).ToList();

            return new Pokemon
            {
                Id = root.GetProperty("order").GetInt32(),
                Name = root.GetProperty("name").GetString(),
                Weight = root.GetProperty("weight").GetInt32(),
                Height = root.GetProperty("height").GetInt32(),
                Abilities = abilities!
            };
        }

        public override void Write(Utf8JsonWriter writer, Pokemon value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
