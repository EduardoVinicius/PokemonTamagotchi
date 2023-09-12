namespace PokemonTamagotchi.Models
{
    public class PokePet
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }

        public List<PokeAbility>? Abilities { get; set; }


    }
}
