namespace Deopeia.Common.Contracts;

public partial class Uuid
{
    public Uuid(ulong high, ulong low)
    {
        High = high;
        Low = low;
    }

    public static implicit operator Guid(Uuid uuid)
    {
        var bytes = new byte[16];
        Array.Copy(BitConverter.GetBytes(uuid.High), 0, bytes, 0, 8);
        Array.Copy(BitConverter.GetBytes(uuid.Low), 0, bytes, 8, 8);

        // 還原成 little-endian，因為 Guid(byte[]) 預設用 little-endian 內部表示
        Array.Reverse(bytes, 0, 8);
        Array.Reverse(bytes, 8, 8);

        return new Guid(bytes);
    }

    public static implicit operator Uuid(Guid guid)
    {
        var bytes = guid.ToByteArray();

        // Guid.ToByteArray() 是 little-endian，為跨語言一致性，轉成 big-endian
        Array.Reverse(bytes, 0, 8);
        Array.Reverse(bytes, 8, 8);

        var high = BitConverter.ToUInt64(bytes, 0);
        var low = BitConverter.ToUInt64(bytes, 8);

        return new Uuid(high, low);
    }
}
