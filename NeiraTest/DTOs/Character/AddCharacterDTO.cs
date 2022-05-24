using NeiraTest.Enums;

namespace NeiraTest.DTOs.Character
{
    public class AddCharacterDTO
    {
        public string Name { get; set; } = "Neira";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; }
        public int Defense { get; set; }
        public int Intelligence { get; set; }
        public RpgClass Class { get; set; } = RpgClass.Knight;
    }
}
