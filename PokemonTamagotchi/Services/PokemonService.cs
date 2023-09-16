using PokemonTamagotchi.Models;
using RestSharp;
using System.Text.Json;

namespace PokemonTamagotchi.Services
{
    public static class PokemonService
    {
        private static string GetPokemonSpeciesInfo(int speciesId)
        {
            var client = new RestClient();
            var request = new RestRequest($"https://pokeapi.co/api/v2/pokemon/{speciesId}", Method.Get);
            var response = client.Execute(request);

            return response.Content!;
        }

        public static PokemonSpeciesResult GetPokemonSpecies(string uri = "https://pokeapi.co/api/v2/pokemon-species/")
        {
            var client = new RestClient();
            var request = new RestRequest(uri, Method.Get);
            var response = client.Execute(request);

            var result = JsonSerializer.Deserialize<PokemonSpeciesResult>(response.Content!, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return result ?? new PokemonSpeciesResult();
        }

        public static PokePet CreatePokePet(int speciesId)
        {
            var pokemonSpeciesInfo = GetPokemonSpeciesInfo(speciesId);

            var options = new JsonSerializerOptions();
            options.Converters.Add(new PokemonJsonConverter());

            PokePet? petPokemon = JsonSerializer.Deserialize<PokePet>(pokemonSpeciesInfo, options);
            return petPokemon ?? new PokePet();
        }
    }
}
