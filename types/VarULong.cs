using VarDataLib.codec;

namespace VarDataLib.types;

public class VarULong : IVarCodec<ulong>
{
    public void Serialize(BinaryWriter writer, ulong value)
    {
        VarIntHelper.WriteVarULong(writer, value);
    }

    public ulong Deserialize(BinaryReader reader)
    {
        return VarIntHelper.ReadVarULong(reader);
    }
}