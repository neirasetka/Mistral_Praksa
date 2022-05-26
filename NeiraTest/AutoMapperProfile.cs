using AutoMapper;
using NeiraTest.DTOs.Character;
using NeiraTest.DTOs.Skill;
using NeiraTest.DTOs.Weapon;
using NeiraTest.Models;

namespace NeiraTest
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDTO>();
            CreateMap<AddCharacterDTO, Character>();
            CreateMap<Weapon, GetWeaponDTO>();
            CreateMap<Skill, GetSkillDTO>();
        }
    }
}
