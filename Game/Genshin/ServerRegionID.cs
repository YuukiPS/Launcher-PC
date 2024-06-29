using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using static YuukiPS_Launcher.Game.Genshin.Settings;

public class ServerRegionIDConverter : StringEnumConverter
{
    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.String)
        {
            string enumText = reader.Value.ToString();
            foreach (ServerRegionID enumValue in Enum.GetValues(typeof(ServerRegionID)))
            {
                if (enumValue.ToString().Equals(enumText, StringComparison.OrdinalIgnoreCase))
                {
                    return enumValue;
                }
            }
            // Handle unknown value here
            return ServerRegionID.os_usa; // Default value
        }
        return base.ReadJson(reader, objectType, existingValue, serializer);
    }
}
