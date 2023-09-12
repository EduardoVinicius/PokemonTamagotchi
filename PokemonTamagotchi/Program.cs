using PokemonTamagotchi;
using PokemonTamagotchi.Models;
using System.Text.Json;

var pokemonSpecies = PokemonService.GetPokemonSpecies();
ConsoleKey pressedKey;

do
{
    Console.Clear();
    Console.WriteLine("Choose your companion:");
    PrintPokemonSpecies(pokemonSpecies.Results!);

    Console.WriteLine();
    Console.Write("Press the down arrow to see more options, Esc to exit or Enter: ");

    pressedKey = Console.ReadKey(true).Key;

    if (pressedKey == ConsoleKey.DownArrow && !string.IsNullOrEmpty(pokemonSpecies.Next))
    {
        pokemonSpecies = PokemonService.GetPokemonSpecies(pokemonSpecies.Next!);
    }
    else if (pressedKey == ConsoleKey.UpArrow && !string.IsNullOrEmpty(pokemonSpecies.Previous))
    {
        pokemonSpecies = PokemonService.GetPokemonSpecies(pokemonSpecies.Previous!);
    }
}
while (pressedKey != ConsoleKey.Enter);

while (Console.KeyAvailable) { Console.ReadKey(true); }

Console.Clear();
PrintPokemonSpecies(pokemonSpecies.Results!);
Console.WriteLine();
Console.Write("Type in the number of the pokémon to be your companion: ");

string userInput = Console.ReadLine();
int userOption;

while (!int.TryParse(userInput, out userOption) || userOption < 1 || userOption > pokemonSpecies.Count)
{
    Console.Clear();
    PrintPokemonSpecies(pokemonSpecies.Results!);
    Console.WriteLine();
    Console.Write("Type in the number of the pokémon to be your companion: ");

    userInput = Console.ReadLine();
}

var petPokemon = CreatePokePet(userOption);

Console.Clear();
PrintPokePetInfo(petPokemon);
Console.WriteLine();
Console.Write("Type [1] to confirm your choice or [2] to go back: ");

Console.ReadLine();


void PrintPokemonSpecies(List<PokemonSpecies> pokemonSpecies)
{
    for (int i = 0; i < pokemonSpecies.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {pokemonSpecies[i].Name}");
    }
}

void PrintPokePetInfo(PokePet pet)
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


PokePet CreatePokePet(int speciesId)
{
    var pokemonSpeciesInfo = PokemonService.GetPokemonSpeciesInfo(speciesId);

    var options = new JsonSerializerOptions();
    options.Converters.Add(new PokemonJsonConverter());

    PokePet? petPokemon = JsonSerializer.Deserialize<PokePet>(pokemonSpeciesInfo, options);
    return petPokemon ?? new PokePet();
}