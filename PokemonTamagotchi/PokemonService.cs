using PokemonTamagotchi.Models;
using RestSharp;
using System.Text.Json;

namespace PokemonTamagotchi
{
    public static class PokemonService
    {
        public static string GetPokemonSpeciesInfo(int speciesId)
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
    }
}
