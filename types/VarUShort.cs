using VarDataLib.codec;

namespace VarDataLib.types;

public class VarUShort : IVarCodec<ushort>
{
    public void Serialize(BinaryWriter writer, ushort value)
    {
        uint val = value;
        while (val >= 0x80)
        {
            writer.Write((byte)((val & 0x7F) | 0x80));
            val >>= 7;
        }
        writer.Write((byte)(val & 0x7F));
    }

    public ushort Deserialize(BinaryReader reader)
    {
        uint result = 0;
        int shift = 0;
        byte b;

        do
        {
            b = reader.ReadByte();
            result |= (uint)(b & 0x7F) << shift;
            shift += 7;
            if (shift > 21)
                throw new InvalidDataException("VarUShort too big");
        } while ((b & 0x80) != 0);

        return (ushort)result;
    }
}