using PokemonTamagotchi.Models;

namespace PokemonTamagotchi.Views
{
    public class GameInterface
    {
        public string UserName { get; set; }
        public int UserOption { get; set; }


        public void ShowWelcomingMessage()
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
            UserName = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine($"Nice to meet you, {UserName}! Let's get to it!");
            Console.WriteLine();
        }

        public void ShowMainMenu()
        {
            Console.WriteLine($"{UserName}, what would you like to do?");
            Console.WriteLine();
            Console.WriteLine("[1] Adopt a virtual pet");
            Console.WriteLine("[2] Check on your pets");
            Console.WriteLine("[3] Exit");
        }

        public void ShowSpeciesMenu(PokemonSpeciesResult pokemonSpecies)
        {
            ConsoleKey pressedKey;

            Console.Clear();

            Console.WriteLine("Get to know the pokémon a bit, before choosing your companion.");
            Console.WriteLine();

            PrintPokemonSpecies(pokemonSpecies);

            Console.WriteLine();
            Console.WriteLine("[↑] Go back on the list");
            Console.WriteLine("[↓] Go forth on the list");
            Console.WriteLine();
            Console.WriteLine("[1] Choose your companion");
            Console.WriteLine("[2] Go back");

            while (Console.KeyAvailable)
                Console.ReadKey(true);
        }

        public int GetOffset(string url)
        {
            int from = url.IndexOf("offset=") + "offset=".Length;
            int to = url.IndexOf("&limit=");
            return int.Parse(url.Substring(from, to - from));
        }

        public void PrintPokemonSpecies(PokemonSpeciesResult pokemonSpecies)
        {
            for (int i = 0; i < pokemonSpecies.Results!.Count; i++)
            {
                Console.WriteLine($"{i + 1 + pokemonSpecies.OffSet}. {pokemonSpecies.Results[i].Name}");
            }
        }

        public void PrintPokePetInfo(PokePet pet)
        {
            Console.WriteLine($"PokéPet: {pet.Name}");
            Console.WriteLine($"Height: {pet.Height}");
            Console.WriteLine($"Weight: {pet.Weight}");
            Console.WriteLine("Abilities:");

            foreach (var ability in pet.Abilities!)
            {
                Console.WriteLine($"\t* {ability.Name}");
            }
        }

        public void PrintCompanions(List<PokePet> pets)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Enjoy the time with your companions!");
            Console.WriteLine();
            for (int i = 0; i < pets.Count; i++)
            {
                Console.WriteLine($"{pets[i].Name}");
            }
        }

        public void GetUserOption()
        {
            int userOption;
            var userInput = Console.ReadKey(true).KeyChar.ToString();

            while (!int.TryParse(userInput, out userOption) || userOption <= 0)
            {
                userInput = Console.ReadKey(true).KeyChar.ToString();
            }

            UserOption = userOption;
        }

        public void GetUserInput()
        {
            int userOption;
            var userInput = Console.ReadLine();

            while (!int.TryParse(userInput, out userOption) || userOption <= 0)
            {
                userInput = Console.ReadLine();
            }

            UserOption = userOption;
        }
    }
}
