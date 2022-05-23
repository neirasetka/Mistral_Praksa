using NeiraTest.Models;

namespace NeiraTest.Services
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character{Name="Sam"}
        };

        public async Task<List<Character>> AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return characters;
        }

        public async Task<List<Character>> GetCharacters()
        {
            return characters;
        }
    }
}
