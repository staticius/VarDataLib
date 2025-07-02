using VarDataLib.codec;

namespace VarDataLib.types;

public class VarInt : IVarCodec<int>
{
    public void Serialize(BinaryWriter writer, int value)
    {
        var encoded = VarIntHelper.ZigZagEncode(value);
        VarIntHelper.WriteVarUInt(writer, encoded);
    }

    public int Deserialize(BinaryReader reader)
    {
        var raw =  VarIntHelper.ReadVarUInt(reader);
        return VarIntHelper.ZigZagDecode(raw);
    }
    
}