using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Blends
{
    public class ByteBlend : DefaultBlend<byte>
    {
        public override void Read(BSReader reader)
        {
            base.Read(reader);
            Value = reader.ReadByte();
        }

        public override void Write(BSWriter writer)
        {
            base.Write(writer);
            writer.Write(Value);
        }
    }
}