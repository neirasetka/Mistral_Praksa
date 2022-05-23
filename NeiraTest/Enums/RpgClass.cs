using System.Text.Json.Serialization;

namespace NeiraTest.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Knight,
        Mage,
        Cleric
    }
}
