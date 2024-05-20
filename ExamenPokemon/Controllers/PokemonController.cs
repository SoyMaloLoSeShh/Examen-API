using Microsoft.AspNetCore.Mvc;
using MyDearPokemon.Services.Pokemones;
using MyDearPokemon.Services;

namespace ExamenPokemon.Controllers;

[ApiController]
[Route("[controller]")]
public class PokemonController : Controller
{
    private readonly PokemonServices _services;

    public PokemonController(PokemonServices services)
    {
        _services = services;
    }
    [HttpGet("{PokeName}")]

    public async Task<ActionResult<Pokemon>> Get(string PokemonName)
    {
        var Poke = await _services.GetPokemon(PokemonName);
        return Ok(Poke);
    }
}