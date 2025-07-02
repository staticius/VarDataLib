using VarDataLib.codec;

namespace VarDataLib.types;

public class VarUInt : IVarCodec<uint >
{
    public void Serialize(BinaryWriter writer, uint value)
    {
        VarIntHelper.WriteVarUInt(writer, value);
    }

    public uint Deserialize(BinaryReader reader)
    {
        return VarIntHelper.ReadVarUInt(reader);
    }
}