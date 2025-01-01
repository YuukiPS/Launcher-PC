using System.Globalization;
using System.Reflection;
using System.Text;
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

            // Normalize to remove accents and special characters
            string normalizedString = gameTypeName.Normalize(NormalizationForm.FormD);
            string cleanedString = new string(normalizedString
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray());

            // Convert to kebab case
            string kebabCase = Regex.Replace(cleanedString, "([a-z])([A-Z])", "$1-$2").ToLower();

            // Replace common problematic characters for SEO-friendly URLs
            kebabCase = kebabCase
                .Replace("ı", "i")  // Fix Turkish 'ı' to 'i'
                .Replace("ş", "s")  // Fix Turkish 'ş' to 's'
                .Replace("ç", "c")  // Fix Turkish 'ç' to 'c'
                .Replace("ü", "u")  // Fix German 'ü' to 'u'
                .Replace("ö", "o")  // Fix German 'ö' to 'o'
                .Replace("ä", "a")  // Fix German 'ä' to 'a'
                .Replace("é", "e"); // Replace French 'é' to 'e'

            return kebabCase;
        }
    }
}

