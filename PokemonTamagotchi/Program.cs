using PokemonTamagotchi;
using PokemonTamagotchi.Models;
using PokemonTamagotchi.Services;
using PokemonTamagotchi.Views;
using System.Text.Json;

GameInterface.ShowMainMenu();

while (Console.KeyAvailable)
    Console.ReadKey(true);

List<PokePet> pets = new List<PokePet>();

int userOption = GetUserOption();
var pokemonSpecies = PokemonService.GetPokemonSpecies();

do
{
    switch (userOption)
    {
        case 1:
            Console.Clear();
            GameInterface.ShowSpeciesMenu(pokemonSpecies);
            Console.Clear();
            Console.WriteLine();
            Console.Write("Type in the number of the pokémon to be your companion: ");
            userOption = GetUserInput();
            while (userOption > pokemonSpecies.Count)
            {
                Console.Clear();
                GameInterface.PrintPokemonSpecies(pokemonSpecies);
                Console.WriteLine();
                Console.Write("Type in the number of the pokémon to be your companion: ");

                userOption = GetUserInput();
            }
            var petPokemon = CreatePokePet(userOption);

            Console.Clear();
            GameInterface.PrintPokePetInfo(petPokemon);
            Console.WriteLine();
            Console.WriteLine($"[1] Adopt {petPokemon.Name}");
            Console.WriteLine("[2] Go back");

            userOption = GetUserOption();

            switch (userOption)
            {
                case 1:
                    pets.Add(petPokemon);
                    Console.WriteLine($"Congratulations! Now {petPokemon.Name} is your companion!");
                    Console.WriteLine();
                    Console.WriteLine("[1] Go back to main menu");
                    userOption = GetUserOption();
                    break;
                case 2:
                    userOption = 1;
                    break;
                default:
                    break;
            }
            break;
        case 2:
            Console.WriteLine();
            Console.WriteLine("Enjoy the time with your companions!");
            Console.WriteLine();
            for (int i = 0; i < pets.Count; i++)
            {
                Console.WriteLine($"{pets[i].Name}");
            }
            Console.WriteLine();
            Console.WriteLine("[1] Go back to main menu");
            userOption = GetUserOption();
            break;
        case 3:
            Console.WriteLine();
            Console.WriteLine("Thanks for playing! See you later!");
            return;
        default:
            Console.WriteLine("Invalid option");
            break;
    }
} while (userOption != 0);

int GetUserOption()
{
    int userOption;
    var userInput = Console.ReadKey(true).KeyChar.ToString();

    while (!int.TryParse(userInput, out userOption) || userOption <= 0)
    {
        userInput = Console.ReadKey(true).KeyChar.ToString();
    }

    return userOption;
}

int GetUserInput()
{
    int userOption;
    var userInput = Console.ReadLine();

    while (!int.TryParse(userInput, out userOption) || userOption <= 0)
    {
        userInput = Console.ReadLine();
    }

    return userOption;
}

PokePet CreatePokePet(int speciesId)
{
    var pokemonSpeciesInfo = PokemonService.GetPokemonSpeciesInfo(speciesId);

    var options = new JsonSerializerOptions();
    options.Converters.Add(new PokemonJsonConverter());

    PokePet? petPokemon = JsonSerializer.Deserialize<PokePet>(pokemonSpeciesInfo, options);
    return petPokemon ?? new PokePet();
}