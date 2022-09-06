using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Blends
{
    public class FloatBlend : DefaultBlend<float>
    {
        public override void Read(BSReader reader)
        {
            Time = reader.ReadSingle();
            Value = reader.ReadSingle();
        }

        public override void Write(BSWriter writer)
        {
            writer.Write(Time);
            writer.Write(Value);
        }
    }
}