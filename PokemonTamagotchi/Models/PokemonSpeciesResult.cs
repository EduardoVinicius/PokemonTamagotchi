namespace PokemonTamagotchi.Models
{
    public class PokemonSpeciesResult
    {
        public int Count { get; set; }
        public string? Next { get; set; }
        public string? Previous { get; set; }
        public List<PokemonSpecies>? Results { get; set; }
    }
}
