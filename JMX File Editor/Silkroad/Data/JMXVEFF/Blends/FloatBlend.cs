using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Blends
{
    public class FloatBlend : DefaultBlend<float>
    {
        public override void Read(BSReader reader)
        {
            base.Read(reader);
            Value = reader.ReadFloat();
        }

        public override void Write(BSWriter writer)
        {
            base.Write(writer);
            writer.Write(Value);
        }
    }
}