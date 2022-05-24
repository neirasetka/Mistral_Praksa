using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeiraTest.DTOs.Character;
using NeiraTest.Models;
using NeiraTest.Services;

namespace NeiraTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }
         
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharactersById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            var response = await _characterService.UpdateCharacter(updatedCharacter);
            //return Ok(await _characterService.UpdateCharacter(updatedCharacter));

            if(response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id);
            if(response.Data==null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }
    }
}
