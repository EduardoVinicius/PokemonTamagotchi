using System.Text.Json.Serialization;

namespace PokemonTamagotchi.Models
{
    public class Pokemon
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }

        public List<string>? Abilities { get; set; }


    }
}
