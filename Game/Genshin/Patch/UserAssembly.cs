using System.Text;
using YuukiPS_Launcher.patch;

namespace YuukiPS_Launcher.Game.Genshin.Patch
{
    public static class UserAssembly
    {
        public static readonly long Pas = 0x4889501048BA;
        public static readonly long Pbs = 0x908000000048BA;
        public static readonly byte[] Pbb = { 0x48, 0x89 };

        public static string Do(string original_file, string patch_file, string keynopatch, string keypatch)
        {
            if (!File.Exists(original_file))
            {
                return "UserAssembly file not found";
            }
            byte[] data = File.ReadAllBytes(original_file);

            int count = 0;
            string str;

            data = Methods.ReplaceBytes(data, ToUABytes(keynopatch), ToUABytes(keypatch), ref count);

            if (count != 0)
            {
                str = "";
                FileStream stream = File.Create(patch_file);
                stream.Write(data, 0, data.Length);
                stream.Close();
            }
            else
            {
                str = "Patch UserAssembly Failed";
            }

            return str;
        }

        public static byte[] ToUABytes(string key)
        {
            int count = key.Length + 2;
            List<byte> uabytes = Encoding.UTF8.GetBytes(key).ToList();
            for (int i = 1; i < 44; i++)
            {
                if (i <= 29)
                {
                    byte[] k = GetBytesPb(29 - i);
                    for (int j = 0; j < k.Length; j++)
                    {
                        uabytes.Insert(count - 8 * i, k[k.Length - 1 - j]);
                    }
                }
                else
                {
                    byte[] k = GetBytesPa(14 - (i - 29));
                    for (int j = 0; j < k.Length; j++)
                    {
                        uabytes.Insert(count - 8 * i, k[k.Length - 1 - j]);
                    }
                }
            }
            return uabytes.ToArray();
        }

        public static byte[] GetBytesPa(int count)//0-13
        {
            byte[] pa = BitConverter.GetBytes(Pas + (0x80000 * count));
            Array.Resize(ref pa, 6);
            pa = pa.Reverse().ToArray();
            return pa;
        }
        public static byte[] GetBytesPb(int count)//0-28
        {
            byte[] pb = BitConverter.GetBytes(Pbs + (0x80000000000 * count));
            Array.Resize(ref pb, 7);
            byte[] pb2 = Pbb.Concat(pb.Reverse()).ToArray();
            if (count >= 16)
            {
                pb2[2] = 0x90;
                pb2[4] = 0x01;
            }
            return pb2;
        }
    }
}
