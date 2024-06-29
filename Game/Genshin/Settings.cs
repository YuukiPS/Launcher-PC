using Microsoft.Win32;
using Newtonsoft.Json;
using System.Text;
using YuukiPS_Launcher.Utils;

namespace YuukiPS_Launcher.Game.Genshin
{
    public class Settings
    {
        // Source Code: https://github.com/neon-nyan/CollapseLauncher
        private static string OsPathKey = @"Software\miHoYo\Genshin Impact";
        private static string CnPathKey = @"Software\miHoYo\原神";

        private int ch = 1;

        public Settings(int ch)
        {
            this.ch = ch;
        }

        public int GetChannel()
        {
            return ch;
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

        public string GetDataGeneralString()
        {
            RegistryKey? keys = Registry.CurrentUser.OpenSubKey(GetReg());
            var data = (byte[]?)keys?.GetValue("GENERAL_DATA_h2389025596", "{}", RegistryValueOptions.None);
            if (data != null)
            {
                return Encoding.UTF8.GetString(data);
            }
            else
            {
                return "";
            }
        }

        public GeneralDataProp? GetDataGeneralJson()
        {
            RegistryKey? keys = Registry.CurrentUser.OpenSubKey(GetReg());
            var data = (byte[]?)keys?.GetValue("GENERAL_DATA_h2389025596", "{}", RegistryValueOptions.None);
            if (data != null)
            {
                var tesss = Encoding.UTF8.GetString(data);
                if (tesss != null)
                {
                    return JsonConvert.DeserializeObject<GeneralDataProp>(tesss);
                }
            }
            return null;

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
            return JsonConvert.DeserializeObject<GeneralDataProp>(new string(regValue))?.deviceVoiceLanguageType ?? 2;
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
            return (int)(JsonConvert.DeserializeObject<GeneralDataProp>(regValue)?.selectedServerName ?? ServerRegionID.os_usa);
        }

        public enum ServerRegionID
        {
            os_usa = 0,
            os_euro = 1,
            os_asia = 2,
            os_cht = 3
        }

        public class GeneralDataProp
        {
            public string deviceUUID { get; set; } = "";
            public string userLocalDataVersionId { get; set; } = "0.0.1";
            public int deviceLanguageType { get; set; } = 1;
            public int deviceVoiceLanguageType { get; set; } = 2;
            [JsonConverter(typeof(ServerRegionIDConverter))]
            public ServerRegionID? selectedServerName { get; set; }
            public int localLevelIndex { get; set; } = 0;
            public string deviceID { get; set; } = "";
            public string targetUID { get; set; } = "";
            public string curAccountName { get; set; } = "";
            public string uiSaveData { get; set; } = "";
            public string inputData { get; set; } = "";
            // Initialize 60 fps limit if it's blank
            public string graphicsData { get; set; } = "{\"customVolatileGrades\":[{\"key\":1,\"value\":2}]";
            public string globalPerfData { get; set; } = "";
            public int miniMapConfig { get; set; } = 1;
            public bool enableCameraSlope { get; set; } = true;
            public bool enableCameraCombatLock { get; set; } = true;
            public bool completionPkg { get; set; } = false;
            public bool completionPlayGoPkg { get; set; } = false;
            public bool onlyPlayWithPSPlayer { get; set; } = false;
            public bool needPlayGoFullPkgPatch { get; set; } = false;
            public bool resinNotification { get; set; } = true;
            public bool exploreNotification { get; set; } = true;
            public int volumeGlobal { get; set; } = 10;
            public int volumeSFX { get; set; } = 10;
            public int volumeMusic { get; set; } = 10;
            public int volumeVoice { get; set; } = 10;
            public int audioAPI { get; set; } = -1;
            public int audioDynamicRange { get; set; } = 0;
            // Use Surround by default if it's blank
            public int audioOutput { get; set; } = 1;
            public bool _audioSuccessInit { get; set; } = true;
            public bool enableAudioChangeAndroidMinimumBufferCapacity { get; set; } = true;
            public int audioAndroidMiniumBufferCapacity { get; set; } = 2 << 10;
            public bool motionBlur { get; set; } = true;
            public bool gyroAiming { get; set; } = false;
            public bool firstHDRSetting { get; set; } = true;
            public double maxLuminosity { get; set; } = 0.0f;
            public double uiPaperWhite { get; set; } = 0.0f;
            public double scenePaperWhite { get; set; } = 0.0f;
            public double gammaValue { get; set; } = 2.200000047683716f;
            public IEnumerable<object> _overrideControllerMapKeyList { get; set; } = new List<object>();
            public IEnumerable<object> _overrideControllerMapValueList { get; set; } = new List<object>();
            public int lastSeenPreDownloadTime { get; set; } = 0;
            public bool mtrCached { get; set; } = false;
            public bool mtrIsOpen { get; set; } = false;
            public int mtrMaxTTL { get; set; } = 0x20;
            public int mtrTimeOut { get; set; } = 0x1388;
            public int mtrTraceCount { get; set; } = 5;
            public int mtrAbortTimeOutCount { get; set; } = 3;
            public int mtrAutoTraceInterval { get; set; } = 0;
            public int mtrTraceCDEachReason { get; set; } = 0x258;
            public IEnumerable<object> _customDataKeyList { get; set; } = new List<object>();
            public IEnumerable<object> _customDataValueList { get; set; } = new List<object>();
        }

    }
}
