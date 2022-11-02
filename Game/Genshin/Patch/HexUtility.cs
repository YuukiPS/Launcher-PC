public class HexUtility
{
    public static bool EqualsBytes(byte[] b1, params byte[] b2)
    {
        if (b1.Length != b2.Length)
            return false;
        for (int i = 0; i < b1.Length; i++)
        {
            if (b1[i] != b2[i])
                return false;
        }
        return true;
    }

    public static byte[] Replace(byte[] sourceByteArray, List<HexReplaceEntity> replaces)
    {
        byte[] newByteArray = new byte[sourceByteArray.Length];
        Buffer.BlockCopy(sourceByteArray, 0, newByteArray, 0, sourceByteArray.Length);
        int offset = 0;
        foreach (HexReplaceEntity rep in replaces)
        {
            if (EqualsBytes(rep.oldValue, rep.newValue))
            {
                continue;
            }

            for (; offset < sourceByteArray.Length; offset++)
            {
                if (sourceByteArray[offset] == rep.oldValue[0])
                {
                    if (sourceByteArray.Length - offset < rep.oldValue.Length)
                        break;

                    bool find = true;
                    for (int i = 1; i < rep.oldValue.Length - 1; i++)
                    {
                        if (sourceByteArray[offset + i] != rep.oldValue[i])
                        {
                            find = false;
                            break;
                        }
                    }
                    if (find)
                    {
                        Buffer.BlockCopy(rep.newValue, 0, newByteArray, offset, rep.newValue.Length);
                        offset += (rep.newValue.Length - 1);
                        break;
                    }
                }
            }
        }
        return newByteArray;
    }
}

public class HexReplaceEntity
{
    public byte[] oldValue { get; set; }

    public byte[] newValue { get; set; }

}