using System.Text;
using YuukiPS_Launcher.patch;

namespace YuukiPS_Launcher.Game.Genshin.Patch
{
    public static class Metadata
    {
        public static byte[] bytes = { 0x0D, 0x0A, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20 };

        public static string Do(string original_file, string patch_file, string key1nopatch, string key1patch, string key2nopatch, string key2patch)
        {
            if (!File.Exists(original_file))
            {
                return "Metadata file not found";
            }
            byte[] filebytes = File.ReadAllBytes(original_file);
            byte[] data = decrypt(filebytes);

            Array.Resize<byte>(ref data, data.Length - 16384);

            int count = 0;
            string str;

            data = Methods.ReplaceBytes(data, ToFixBytesP1(key1nopatch), ToFixBytesP1(key1patch), ref count);
            if (count == 0)
            {
                return "Key1 can't be patched";
            }

            data = Methods.ReplaceBytes(data, Encoding.UTF8.GetBytes(key2nopatch), Encoding.UTF8.GetBytes(key2patch), ref count);

            if (count != 0)
            {
                Array.Resize<byte>(ref data, data.Length + 16384);
                filebytes = encrypt(data);

                str = "";
                FileStream stream = File.Create(patch_file);
                stream.Write(data, 0, data.Length);
                stream.Close();
            }
            else
            {
                str = "Key2 can't be patched";
            }

            return str;
        }

        public static byte[] ToFixBytesP1(string key)
        {
            byte[] bp1 = Encoding.UTF8.GetBytes(key.Substring(0, 48));
            byte[] bp2 = Encoding.UTF8.GetBytes(key.Substring(48, 57));
            byte[] bp3 = Encoding.UTF8.GetBytes(key.Substring(105, 57));
            byte[] bp4 = Encoding.UTF8.GetBytes(key.Substring(162));
            byte[] ret = bp1.Concat(bytes).Concat(bp2).Concat(bytes).Concat(bp3).Concat(bytes).Concat(bp4).ToArray();
            return ret;

        }

        unsafe static public byte[] decrypt(byte[] bytes)
        {
            fixed (byte* d1 = bytes)
            {
                Patch_Meta patch_Meta = new Patch_Meta();
                patch_Meta.decrypt_global_metadata(d1, Convert.ToUInt64(bytes.Length));
                return bytes;
            }
        }
        unsafe static public byte[] encrypt(byte[] bytes)
        {
            fixed (byte* d1 = bytes)
            {
                Patch_Meta patch_Meta = new Patch_Meta();
                patch_Meta.encrypt_global_metadata(d1, Convert.ToUInt64(bytes.Length));
                return bytes;
            }
        }

    }
}
