using System.Reflection;
using System.Text.RegularExpressions;

namespace YuukiPS_Launcher.Json
{
    public enum GameType
    {
        [StringValue("Genshin Impact")]
        GenshinImpact = 1,

        [StringValue("Star Rail")]
        StarRail = 2,

        [StringValue("Zenless Zone Zero")]
        ZenlessZoneZero = 3,

        [StringValue("Wuthering Waves")]
        WutheringWaves = 4
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class StringValueAttribute : Attribute
    {
        public string Value { get; }

        public StringValueAttribute(string value)
        {
            Value = value;
        }
    }

    public static class EnumExtensions
    {
        public static string GetStringValue(this Enum value)
        {
            Type type = value.GetType();
            string? name = Enum.GetName(type, value);

            if (name == null)
                return value.ToString();

            FieldInfo? field = type.GetField(name);
            if (field == null)
                return value.ToString();

            StringValueAttribute? attribute = field.GetCustomAttribute<StringValueAttribute>();
            return attribute?.Value ?? value.ToString();
        }

        public static string SEOUrl(this GameType gameType)
        {
            string gameTypeName = gameType.ToString();
            string kebabCase = Regex.Replace(gameTypeName, "([a-z])([A-Z])", "$1-$2").ToLower();
            return kebabCase;
        }
    }
}

