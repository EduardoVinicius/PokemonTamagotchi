using PokemonTamagotchi;
using PokemonTamagotchi.Models;
using PokemonTamagotchi.Services;
using PokemonTamagotchi.Views;
using System.Text.Json;

GameInterface.ShowMainMenu();

while (Console.KeyAvailable)
    Console.ReadKey(true);

int userOption = GetUserOption();
var pokemonSpecies = PokemonService.GetPokemonSpecies();

switch (userOption)
{
    case 1:
        GameInterface.ShowSpeciesMenu(pokemonSpecies);
        Console.Clear();
        GameInterface.PrintPokemonSpecies(pokemonSpecies);
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
        Console.WriteLine("[1] Adopt pokémon");
        Console.WriteLine("[2] Go back");

        Console.ReadLine();
        break;
    case 2:
        break;
    case 3:
        Console.WriteLine("Thanks for playing! See you later!");
        return;
    default:
        Console.WriteLine("Invalid option");
        break;
}


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