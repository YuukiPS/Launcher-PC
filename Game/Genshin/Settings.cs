using Microsoft.Win32;
using Newtonsoft.Json;
using System.Text;
using YuukiPS_Launcher.Utils;

namespace YuukiPS_Launcher.Game.Genshin
{
    public class Settings
    {
        // Source Code: https://github.com/neon-nyan/CollapseLauncher
        private static readonly string OsPathKey = @"Software\miHoYo\Genshin Impact";
        private static readonly string CnPathKey = @"Software\miHoYo\原神";

        private readonly int ch = 1;

        public Settings(int ch)
        {
            this.ch = ch;
        }

        public string GetReg()
        {
            return ch == 1 ? OsPathKey : CnPathKey;
        }

        private byte[]? GetDataGeneral()
        {
            RegistryKey? keys = Registry.CurrentUser.OpenSubKey(GetReg());
            return (byte[]?)keys?.GetValue("GENERAL_DATA_h2389025596", "{}", RegistryValueOptions.None);
        }

        public string? GetGameLanguage()
        {
            ReadOnlySpan<char> value;
            RegistryKey? keys = Registry.CurrentUser.OpenSubKey(GetReg());

            byte[]? result = (byte[]?)keys?.GetValue("MIHOYOSDK_CURRENT_LANGUAGE_h2559149783");

            if (keys is null || result is null || result.Length is 0)
            {
                Logger.Info("Settings", $"Fallback value will be used.");
                return "en";
            }

            value = Encoding.UTF8.GetString(result).AsSpan().Trim('\0');
            return new string(value);
        }

        public int GetVoiceLanguageID()
        {
            byte[]? value = GetDataGeneral();
            if (value is null || value.Length is 0)
            {
                Logger.Info("Settings", $"Fallback value will be used (2 / ja-jp).");
                return 2;
            }
            ReadOnlySpan<char> regValue = Encoding.UTF8.GetString(value).AsSpan().Trim('\0');
            return JsonConvert.DeserializeObject<GeneralDataProp>(new string(regValue))?.DeviceVoiceLanguageType ?? 2;
        }

        public int GetRegServerNameID()
        {
            byte[]? value = GetDataGeneral();
            if (value is null || value.Length is 0)
            {
                Logger.Info("Settings", $"Fallback value will be used (0 / USA).");
                return 0;
            }
            string regValue = new string(Encoding.UTF8.GetString(value).AsSpan().Trim('\0'));
            return (int)(JsonConvert.DeserializeObject<GeneralDataProp>(regValue)?.SelectedServerName ?? ServerRegionID.os_usa);
        }

        public enum ServerRegionID
        {
            os_usa = 0,
            // os_euro = 1,
            // os_asia = 2,
            // os_cht = 3
        }

        public class GeneralDataProp
        {
            [JsonProperty("deviceVoiceLanguageType")]
            public int DeviceVoiceLanguageType { get; set; } = 2;
            [JsonConverter(typeof(ServerRegionIDConverter)), JsonProperty("selectedServerName")]
            public ServerRegionID? SelectedServerName { get; set; }
        }

    }
}
