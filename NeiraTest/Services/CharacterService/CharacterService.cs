using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NeiraTest.Data;
using NeiraTest.DTOs.Character;
using NeiraTest.Models;
using System.Security.Claims;

namespace NeiraTest.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private static List<Character> characters = new List<Character>
        {
            new Character(),
            new Character{Name="Sam"}
        };

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }

        //we access the user via the HTTP context accessor 
        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            Character character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

            characters.Add(character);
            serviceResponse.Data = _context.Characters
                //when we add a new character, we only see the characters
                //or get the characters that are related to our currently
                //authenticated user
                .Where(c => c.User.Id == GetUserId())
                .Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            //serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            //var dbCharacters = await _context.Characters.ToListAsync();
            //var dbCharacters = await _context.Characters.Where(c=>c.User.Id==userId).ToListAsync();
            //jer smo napravili metodu GetUserId

            var dbCharacters = await _context.Characters
                //we included weapon and skills so we can see weapon and skills of any character
                .Include(c=>c.Weapon)
                .Include(c=>c.Skills)
                .Where(c => c.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Data = _context.Characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<GetCharacterDTO>>> GetCharactersById(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            var dbCharacter = await _context.Characters
                //we included weapon and skills so we can see weapon and skills of any character
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
            //serviceResponse.Data = _mapper.Map<List<GetCharacterDTO>>(characters.FirstOrDefault(c => c.Id == id));
            serviceResponse.Data = _mapper.Map<List<GetCharacterDTO>>(characters.FirstOrDefault(c => c.Id == id));
            return serviceResponse;
        }
        public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDTO>();
            try
            {
                Character character = await _context.Characters
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if (character.User.Id == GetUserId())
                {
                    character.Name = updatedCharacter.Name;
                    character.HitPoints = updatedCharacter.HitPoints;
                    character.Strength = updatedCharacter.Strength;
                    character.Defense = updatedCharacter.Defense;
                    character.Intelligence = updatedCharacter.Intelligence;
                    character.Class = character.Class;

                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found.";
                }
            }

            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;

        }

        public async Task<ServiceResponse<List<GetCharacterDTO>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
            try
            {
                Character character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
                if (character != null)
                {
                    _context.Characters.Remove(character);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _context.Characters
                        .Where(c => c.User.Id == GetUserId())
                        .Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Character not found.";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> AddCharacterSkill(AddCharacterSkillDTO newCharacterSkill)
        {
            var response = new ServiceResponse<GetCharacterDTO>();

            try
            {
                //we first access the characters from the context and then
                //filter them with the method first or default
                var character = await _context.Characters
                    .Include(c=>c.Weapon)
                    .Include(c=>c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId && c.User.Id == GetUserId());
                    
                if(character==null)
                {
                    response.Success = false;
                    response.Message = "Character not found.";
                    return response;
                }

                var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);

                if(skill==null)
                {
                    response.Success = false;
                    response.Message = "Skill nout found.";
                    return response;
                }
                character.Skills.Add(skill);
                await _context.SaveChangesAsync();
                
                //reset the response data to the mapped character
                response.Data= _mapper.Map<GetCharacterDTO>(character);
            }
            catch (Exception ex)
            {

                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
