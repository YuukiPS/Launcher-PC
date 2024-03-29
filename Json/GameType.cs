﻿using System.Reflection;
using System.Text.RegularExpressions;

public enum GameType
{
    [StringValue("Genshin Impact")]
    GenshinImpact = 1,

    [StringValue("Star Rail")]
    StarRail = 2
}

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
        string name = Enum.GetName(type, value);

        MemberInfo member = type.GetField(name);
        StringValueAttribute attribute = member.GetCustomAttribute<StringValueAttribute>();

        return attribute != null ? attribute.Value : value.ToString();
    }
    public static string SEOUrl(this GameType gameType)
    {
        string gameTypeName = gameType.ToString();
        string kebabCase = Regex.Replace(gameTypeName, "([a-z])([A-Z])", "$1-$2").ToLower();
        return kebabCase;
    }
}