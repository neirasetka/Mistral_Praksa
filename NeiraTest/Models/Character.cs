using NeiraTest.Enums;

namespace NeiraTest.Models
{
    public class Character
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Neira";
        public int HitPoints { get; set; } = 100;
        public RpgClass Class { get; set; } = RpgClass.Knight;
    }
}
