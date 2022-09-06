using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Blends
{
    public class ByteBlend : DefaultBlend<byte>
    {
        public override void Read(BSReader reader)
        {
            Time = reader.ReadSingle();
            Value = reader.ReadByte();
        }

        public override void Write(BSWriter writer)
        {
            writer.Write(Time);
            writer.Write(Value);
        }
    }
}