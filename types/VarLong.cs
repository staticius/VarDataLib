using VarDataLib.codec;

namespace VarDataLib.types;

public class VarLong : IVarCodec<long>
{
    public void Serialize(BinaryWriter writer, long value)
    {
        var encoded = VarIntHelper.ZigZagEncode(value);
        VarIntHelper.WriteVarULong(writer, encoded);
    }

    public long Deserialize(BinaryReader reader)
    {
        var raw = VarIntHelper.ReadVarULong(reader);
        return VarIntHelper.ZigZagDecode(raw);
    }
}