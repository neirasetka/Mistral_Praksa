using NeiraTest.DTOs.Character;
using NeiraTest.Models;

namespace NeiraTest.Services
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters();
        Task<ServiceResponse<List<GetCharacterDTO>>> GetCharactersById(int Id);
        Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter); 
        Task<ServiceResponse<List<GetCharacterDTO>>> UpdateCharacter(UpdateCharacterDTO updatedCharacter);
        Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id);
    }
}
