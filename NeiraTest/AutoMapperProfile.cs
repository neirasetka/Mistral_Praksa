﻿using AutoMapper;
using NeiraTest.DTOs.Character;
using NeiraTest.Models;

namespace NeiraTest
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDTO>();
            CreateMap<AddCharacterDTO, Character>();
        }
    }
}
