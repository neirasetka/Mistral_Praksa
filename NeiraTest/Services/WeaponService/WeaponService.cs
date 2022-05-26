using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NeiraTest.Data;
using NeiraTest.DTOs.Character;
using NeiraTest.DTOs.Weapon;
using NeiraTest.Models;
using System.Security.Claims;

namespace NeiraTest.Services.WeaponService
{
    public class WeaponService:IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<ServiceResponse<GetCharacterDTO>> AddWeapon(AddWeaponDTO newWeapon)
        {
            var response = new ServiceResponse<GetCharacterDTO>();
            try
            {
                var character = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId &&
                     c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
            if(character == null)
            {
                    response.Success = false;
                    response.Message = "Character not found.";
                    return response;
            }
                var weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Character=character
            };
                //we add new weapon to the database
                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDTO>(character); 

            }
            catch (Exception ex)
            {

                response.Success=false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
