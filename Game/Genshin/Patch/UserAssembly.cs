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

            byte[] UAOriginal = Encoding.ASCII.GetBytes(keynopatch);
            byte[] UAKey = Encoding.ASCII.GetBytes(keypatch);

            // Get Byte file original
            byte[] UA = File.ReadAllBytes(original_file);

            int Offset = 0;
            int DataLength;

            List<HexReplaceEntity> UA_list = new();

            while ((DataLength = UAOriginal.Length - Offset) > 0)
            {
                if (DataLength > 8)
                    DataLength = 8;

                HexReplaceEntity hexReplaceEntity = new();
                Buffer.BlockCopy(UAOriginal, Offset, hexReplaceEntity.OldValue, 0, DataLength);

                hexReplaceEntity.NewValue = new byte[8];
                Buffer.BlockCopy(UAKey, Offset, hexReplaceEntity.NewValue, 0, DataLength);

                UA_list.Add(hexReplaceEntity);
                Offset += DataLength;
            }

            byte[] UAOSPatched = HexUtility.Replace(UA, UA_list);

            if (!HexUtility.EqualsBytes(UA, UAOSPatched))
            {
                try
                {
                    File.WriteAllBytes(patch_file, UAOSPatched);
                    return "";
                }
                catch (IOException e)
                {
                    return e.Message + "\n Cannot write to file.";
                }
            }
            return "Patch UserAssembly Failed, Send console logs don't just screenshot this message.";
        }

        public static byte[] ToUABytes(string key)
        {
            int count = key.Length + 2;
            List<byte> uaBytes = Encoding.UTF8.GetBytes(key).ToList();
            for (int i = 1; i < 44; i++)
            {
                if (i <= 29)
                {
                    byte[] k = GetBytesPb(29 - i);
                    for (int j = 0; j < k.Length; j++)
                    {
                        uaBytes.Insert(count - 8 * i, k[k.Length - 1 - j]);
                    }
                }
                else
                {
                    byte[] k = GetBytesPa(14 - (i - 29));
                    for (int j = 0; j < k.Length; j++)
                    {
                        uaBytes.Insert(count - 8 * i, k[k.Length - 1 - j]);
                    }
                }
            }
            return uaBytes.ToArray();
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
