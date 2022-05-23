using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeiraTest.Models;
using NeiraTest.Services;

namespace NeiraTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character{Name="Sam"}
        };

        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }
         
        [HttpGet]
        public async Task<ActionResult<List<Character>>> Get()
        {
            return Ok(await _characterService.GetCharacters());
        }
        public async Task<ActionResult<List<Character>>> AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return Ok(await _characterService.GetCharacters());
        }
    }
}
