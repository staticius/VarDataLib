using VarDataLib.codec;

namespace VarDataLib.types;

public class VarShort : IVarCodec<short>
{
    public void Serialize(BinaryWriter writer, short value)
    {
        var encoded = VarIntHelper.ZigZagEncode(value);
        var ushortCodec = new VarUShort();
        ushortCodec.Serialize(writer, encoded);
    }

    public short Deserialize(BinaryReader reader)
    {
        VarUShort ushortCodec = new VarUShort();
        ushort raw = ushortCodec.Deserialize(reader);
        return VarIntHelper.ZigZagDecode(raw);
    }
}