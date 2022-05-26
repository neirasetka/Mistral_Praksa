using NeiraTest.DTOs.Character;
using NeiraTest.DTOs.Weapon;
using NeiraTest.Models;

namespace NeiraTest.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponse<GetCharacterDTO>> AddWeapon(AddWeaponDTO newWeapon);
    }
}
