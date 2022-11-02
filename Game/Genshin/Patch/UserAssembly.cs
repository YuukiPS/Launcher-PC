using System.Text;

namespace YuukiPS_Launcher.Game.Genshin.Patch
{
    public static class UserAssembly
    {
        public static readonly long Pas = 0x4889501048BA;
        public static readonly long Pbs = 0x908000000048BA;
        public static readonly byte[] Pbb = { 0x48, 0x89 };

        public static string Do(string original_file, string patch_file, string keynopatch, string keypatch)
        {
            //Console.WriteLine("KeyNopatch" + "\n" + keynopatch);
            //Console.WriteLine("KeyPatch" + "\n" + keypatch);

            if (!File.Exists(original_file))
            {
                return "UserAssembly file not found";
            }

            byte[] UA_Original = Encoding.ASCII.GetBytes(keynopatch);
            byte[] UA_key = Encoding.ASCII.GetBytes(keypatch);

            // Get Byte file original
            byte[] UA = File.ReadAllBytes(original_file);

            int Offset = 0;
            int DataLength;

            List<HexReplaceEntity> UA_list = new List<HexReplaceEntity>();

            while ((DataLength = UA_Original.Length - Offset) > 0)
            {
                if (DataLength > 8)
                    DataLength = 8;

                HexReplaceEntity hexReplaceEntity = new HexReplaceEntity();

                hexReplaceEntity.oldValue = new byte[8];
                Buffer.BlockCopy(UA_Original, Offset, hexReplaceEntity.oldValue, 0, DataLength);

                hexReplaceEntity.newValue = new byte[8];
                Buffer.BlockCopy(UA_key, Offset, hexReplaceEntity.newValue, 0, DataLength);

                UA_list.Add(hexReplaceEntity);
                Offset += DataLength;
            }

            byte[] UA_OS_patched = HexUtility.Replace(UA, UA_list);

            if (!HexUtility.EqualsBytes(UA, UA_OS_patched))
            {
                try
                {
                    File.WriteAllBytes(patch_file, UA_OS_patched);
                    return "";
                }
                catch (IOException e)
                {
                    return e.Message + "\n Cannot write to file.";
                }
            }
            return "Patch UserAssembly Failed";
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
