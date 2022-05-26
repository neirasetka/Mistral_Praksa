using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeiraTest.DTOs.Character;
using NeiraTest.DTOs.Weapon;
using NeiraTest.Models;
using NeiraTest.Services.WeaponService;

namespace NeiraTest.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController:ControllerBase
    {
        private readonly IWeaponService _weaponService;
        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> AddWeapon (AddWeaponDTO newWeapon)
        {
            return Ok(await _weaponService.AddWeapon(newWeapon));
        }
    }
}
