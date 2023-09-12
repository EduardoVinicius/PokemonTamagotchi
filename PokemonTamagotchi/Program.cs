using PokemonTamagotchi;
using PokemonTamagotchi.Models;
using System.Text.Json;

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

Console.ReadKey();

var pokemonSpecies = PokemonService.GetPokemonSpecies();
GameInterface.ShowSpeciesMenu(pokemonSpecies);

Console.Clear();
GameInterface.PrintPokemonSpecies(pokemonSpecies);
Console.WriteLine();
Console.Write("Type in the number of the pokémon to be your companion: ");

int userOption = GetUserInput();

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
Console.Write("Type [1] to confirm your choice or [2] to go back: ");

Console.ReadLine();

int GetUserOption()
{
    int userOption;
    var userInput = Console.ReadKey(true).ToString();

    while (!int.TryParse(userInput, out userOption) || userOption <= 0)
    {
        userInput = Console.ReadKey(true).ToString();
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