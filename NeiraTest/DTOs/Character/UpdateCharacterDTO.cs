﻿using NeiraTest.Enums;

namespace NeiraTest.DTOs.Character
{
    public class UpdateCharacterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Neira";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        public RpgClass Class { get; set; } = RpgClass.Knight;

    }
}
