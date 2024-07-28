using System.Diagnostics;

namespace YuukiPS_Launcher.Game.Genshin.Patch
{
    public static class Methods
    {
        public static byte[] ReplaceBytes(byte[] src, byte[] search, byte[] repl, ref int i)
        {
            byte[] dst = src;
            int index = FindBytes(src, search);
            if (index == -1)
            {
                return src;
            }
            if (index >= 0)
            {
                dst = new byte[src.Length - search.Length + repl.Length];

                Buffer.BlockCopy(src, 0, dst, 0, index);

                Buffer.BlockCopy(repl, 0, dst, index, repl.Length);

                Buffer.BlockCopy(
                    src,
                    index + search.Length,
                    dst,
                    index + repl.Length,
                    src.Length - (index + search.Length));
            }
            i++;
            return dst;
        }

        public static int FindBytes(byte[] src, byte[] find)
        {
            int index = -1;
            int matchIndex = 0;

            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] == find[matchIndex])
                {
                    if (matchIndex == (find.Length - 1))
                    {
                        index = i - matchIndex;
                        break;
                    }
                    matchIndex++;
                }
                else
                {
                    matchIndex = 0;
                }

            }

            Debug.Print("FindIndex:" + index);
            return index;
        }
    }
}
