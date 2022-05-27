using Microsoft.AspNetCore.Mvc;
using NeiraTest.DTOs.Fight;
using NeiraTest.Models;
using NeiraTest.Services.FightService;

namespace NeiraTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FightController:ControllerBase
    {
        private readonly IFightService _fightService;
        public FightController(IFightService fightService)
        {
            _fightService = fightService;
        }

        [HttpPost("Weapon")]
        public async Task<ActionResult<ServiceResponse<AttackResultDTO>>> WeaponAttack(WeaponAttackDTO request)
        {
            return Ok(await _fightService.WeaponAttack(request));
        }
        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<AttackResultDTO>>> SkillAttack(SkillAttackDTO request)
        {
            return Ok(_fightService.SkillAttack(request));
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<FightResultDTO>>> Fight(FightRequestDTO request)
        {
            return Ok(_fightService.Fight(request));
        }
        [HttpGet]
        public async Task<ActionResult<ServiceResponse<HighScoreDTO>>> GetHighScore()
        {
            return Ok(await _fightService.GetHighScore());
        }
    }
}