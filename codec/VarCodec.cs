namespace VarDataLib.codec;

    public interface IVarCodec<T>
    {
        void Serialize(BinaryWriter writer, T value);
        T Deserialize(BinaryReader reader);
    }

    internal static class VarIntHelper
    {
        public static void WriteVarUInt(BinaryWriter writer, uint value)
        {
            while (value >= 0x80)
            {
                writer.Write((byte)((value & 0x7F) | 0x80));
                value >>= 7;
            }
            writer.Write((byte)(value & 0x7F));
        }

        public static uint ReadVarUInt(BinaryReader reader)
        {
            uint result = 0;
            int shift = 0;
            byte b;

            do
            {
                b = reader.ReadByte();
                result |= (uint)(b & 0x7F) << shift;
                shift += 7;
                if (shift > 35) throw new InvalidDataException("VarUInt too big");
            } while ((b & 0x80) != 0);

            return result;
        }

        public static ulong ReadVarULong(BinaryReader reader)
        {
            ulong result = 0;
            int shift = 0;
            byte b;

            do
            {
                b = reader.ReadByte();
                result |= (ulong)(b & 0x7F) << shift;
                shift += 7;
                if (shift > 70) throw new InvalidDataException("VarULong too big");
            } while ((b & 0x80) != 0);

            return result;
        }

        public static void WriteVarULong(BinaryWriter writer, ulong value)
        {
            while (value >= 0x80)
            {
                writer.Write((byte)((value & 0x7F) | 0x80));
                value >>= 7;
            }
            writer.Write((byte)(value & 0x7F));
        }
        
        public static uint ZigZagEncode(int value)
            => (uint)((value << 1) ^ (value >> 31));

        public static int ZigZagDecode(uint value)
            => (int)((value >> 1) ^ (~(value & 1) + 1));

        public static ulong ZigZagEncode(long value)
            => (ulong)((value << 1) ^ (value >> 63));

        public static long ZigZagDecode(ulong value)
            => (long)((value >> 1) ^ (~(value & 1) + 1));

        public static ushort ZigZagEncode(short value)
        {
            int v = value;
            return (ushort)((v << 1) ^ (v >> 15));
        }

        public static short ZigZagDecode(ushort value)
        {
            int v = value;
            return (short)((v >> 1) ^ (~(v & 1) + 1));
        }
    }