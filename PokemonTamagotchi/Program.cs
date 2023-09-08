using PokemonTamagotchi;
using PokemonTamagotchi.Models;
using RestSharp;
using System.Text.Json;

PrintStartScreen();
var userOption = GetUserOption();

while (userOption < 1 || userOption > 3)
{
    userOption = GetUserOption();
}

var pokemonInfo = GetPokemonInfo(userOption);
var pokemon = CreatePokemon(pokemonInfo);

Console.Clear();
PrintPokemonInfo(pokemon);
Console.ReadLine();


void PrintStartScreen()
{
    Console.WriteLine("**************** POKÉMON TAMAGOTCHI ****************");
    Console.WriteLine("*                                                  *");
    Console.WriteLine("*      It's time to choose your companion!         *");
    Console.WriteLine("*                                                  *");
    Console.WriteLine("*      Who do you choose to be your friend?        *");
    Console.WriteLine("*                                                  *");
    Console.WriteLine("*      1. Bulbasaur                                *");
    Console.WriteLine("*      2. Charmander                               *");
    Console.WriteLine("*      3. Squirtle                                 *");
    Console.WriteLine("*                                                  *");
    Console.WriteLine("****************************************************");
}

void PrintPokemonInfo(Pokemon pokemon)
{
    Console.WriteLine($"{pokemon.Name?.ToUpper()}");
    Console.WriteLine();
    Console.WriteLine($"PokéId: {pokemon.Id}");
    Console.WriteLine($"Height: {pokemon.Height}");
    Console.WriteLine($"Weight: {pokemon.Weight}");
    Console.WriteLine("Abilities:");

    foreach (var ability in pokemon.Abilities!)
        Console.WriteLine($"    - {ability}");

    Console.WriteLine();
    Console.WriteLine($"1. Choose {pokemon.Name?.ToUpper()}");
    Console.WriteLine("2. Go back");
}

int GetUserOption()
{
    var userInput = Console.ReadKey(true).KeyChar;

    while (!char.IsDigit(userInput))
    {
        userInput = Console.ReadKey(true).KeyChar;
    }

    return int.Parse(userInput.ToString());
}

string GetPokemonInfo(int pokemonId)
{
    var client = new RestClient($"https://pokeapi.co/api/v2/pokemon/{pokemonId}");
    RestRequest request = new RestRequest("", Method.Get);
    var response = client.Execute(request);

    if (!response.IsSuccessful)
        Console.WriteLine(response.ErrorMessage);

    return response.Content!;
}

Pokemon CreatePokemon(string pokemonInfo)
{
    var options = new JsonSerializerOptions();
    options.Converters.Add(new PokemonJsonConverter());

    Pokemon? pokemon = JsonSerializer.Deserialize<Pokemon>(pokemonInfo, options);
    return pokemon ?? new Pokemon();
}