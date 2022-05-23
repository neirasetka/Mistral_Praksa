using NeiraTest.Models;

namespace NeiraTest.Services
{
    public interface ICharacterService
    {
        Task<List<Character>> GetCharacters();
        Task<List<Character>> AddCharacter(Character newCharacter); 
    }
}
