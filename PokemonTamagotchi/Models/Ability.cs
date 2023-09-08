using System.Text.Json.Serialization;

namespace PokemonTamagotchi.Models
{
    public class Ability
    {
        [JsonPropertyName("ability.name")]
        public string? Name { get; set; }
    }
}
