namespace PokemonTamagotchi.Models
{
    public class PokePet
    {
        private readonly Random random;
        private int hunger;
        private int humor;
        private int energy;
        private int fatigue;

        public PokePet()
        {
            random = new Random();
            hunger = random.Next(11);
            humor = 10 - hunger;
            energy = random.Next(11);
            fatigue = 10 - energy;
        }

        public int Id { get; set; }

        public string? Name { get; set; }

        public int Height { get; set; }

        public int Weight { get; set; }

        public List<PokeAbility>? Abilities { get; set; }

        public int Hunger { get => hunger; }

        public int Humor { get => humor; }

        public int Fatigue { get => fatigue; }

        public int Energy { get => energy; }


        public void Feed()
        {
            if (Hunger > 0)
                hunger--;
            if (Humor < 10)
                humor++;
        }

        public void Sleep()
        {
            if (Fatigue > 0)
                fatigue--;
            if (Energy > 10)
                energy++;
        }
    }
}
