using MyDearPokemon.Services.Pokemones;
using System.Net.Http.Json;
using System.Text.Json;

namespace MyDearPokemon.Services;
public class PokemonServices
{
    private readonly HttpClient _httpClient;

    public PokemonServices(HttpClient httpClient)
    { _httpClient = httpClient; }

    public async Task<Pokemon> GetPokemon(string PokemonName)
    {
        var response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{PokemonName}");
        response.EnsureSuccessStatusCode();

        var PokemonData = await response.Content.ReadFromJsonAsync<JsonElement>();

        var Poke = new Pokemon
        {
            Name = PokemonName,
            Type = PokemonData.GetProperty("types").EnumerateArray().First().GetProperty("type").GetProperty("name").GetString(),
            URL = PokemonData.GetProperty("sprites").GetProperty("front_default").GetString(),
            Moves = PokemonData.GetProperty("moves").EnumerateArray().Select(m => m.GetProperty("move").GetProperty("name").GetString()).ToList()
        };
        return Poke;
    }
}