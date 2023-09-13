using PokemonTamagotchi.Models;
using PokemonTamagotchi.Services;

namespace PokemonTamagotchi.Views
{
    public static class GameInterface
    {
        public static void ShowMainMenu()
        {
            Console.WriteLine("##### ##### #  # ##### ##### ##### #####");
            Console.WriteLine("#   # #   # # #  #     #   # #       #  ");
            Console.WriteLine("##### #   # ##   ##### ##### #####   #  ");
            Console.WriteLine("#     #   # # #  #     #     #       #  ");
            Console.WriteLine("#     ##### #  # ##### #     #####   #  ");

            Console.WriteLine();
            Console.WriteLine("Welcome to PokéPet! Ready to choose your companion?");
            Console.WriteLine();
            Console.Write("First, tell me your name: ");

            var userName = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine($"Nice to meet you, {userName}! Let's get to it!");
            Console.WriteLine();
            Console.WriteLine("[1] Adopt a virtual pet");
            Console.WriteLine("[2] Check on your pets");
            Console.WriteLine("[3] Exit");
        }

        public static void ShowSpeciesMenu(PokemonSpeciesResult pokemonSpecies)
        {
            ConsoleKey pressedKey;

            do
            {
                Console.Clear();

                Console.WriteLine("Get to know the pokémon a bit, before choosing your companion.");
                Console.WriteLine();

                PrintPokemonSpecies(pokemonSpecies);

                Console.WriteLine();
                Console.WriteLine("Press the up and down arrow to go back and forth in the list.");
                Console.WriteLine("Press Enter when you're ready to choose your companion.");

                pressedKey = Console.ReadKey(true).Key;

                if (pressedKey == ConsoleKey.DownArrow && !string.IsNullOrEmpty(pokemonSpecies.Next))
                {
                    int from = pokemonSpecies.Next.IndexOf("offset=") + "offset=".Length;
                    int to = pokemonSpecies.Next.IndexOf("&limit=");
                    int offset = int.Parse(pokemonSpecies.Next.Substring(from, to - from));
                    pokemonSpecies = PokemonService.GetPokemonSpecies(pokemonSpecies.Next!);
                    pokemonSpecies.OffSet = offset;
                }
                else if (pressedKey == ConsoleKey.UpArrow && !string.IsNullOrEmpty(pokemonSpecies.Previous))
                {
                    int from = pokemonSpecies.Previous.IndexOf("offset=") + "offset=".Length;
                    int to = pokemonSpecies.Previous.IndexOf("&limit=");
                    int offset = int.Parse(pokemonSpecies.Previous.Substring(from, to - from));
                    pokemonSpecies = PokemonService.GetPokemonSpecies(pokemonSpecies.Previous!);
                    pokemonSpecies.OffSet = offset;
                }
            }
            while (pressedKey != ConsoleKey.Enter);

            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        public static void PrintPokemonSpecies(PokemonSpeciesResult pokemonSpecies)
        {
            for (int i = 0; i < pokemonSpecies.Results.Count; i++)
            {
                Console.WriteLine($"{i + 1 + pokemonSpecies.OffSet}. {pokemonSpecies.Results[i].Name}");
            }
        }

        public static void PrintPokePetInfo(PokePet pet)
        {
            Console.WriteLine($"PokéPet: {pet.Name}");
            Console.WriteLine($"Height: {pet.Height}");
            Console.WriteLine($"Weight: {pet.Weight}");
            Console.WriteLine("Abilities:");

            foreach (var ability in pet.Abilities)
            {
                Console.WriteLine($"\t* {ability.Name}");
            }
        }
    }
}
