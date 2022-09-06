using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Blends
{
    public class DiffuseBlend : DefaultBlend<Color32>
    {
        public override void Read(BSReader reader)
        {
            Time = reader.ReadSingle();
            Value = reader.ReadColor32();
        }

        public override void Write(BSWriter writer)
        {
            writer.Write(Time);
            writer.Write(Value);
        }
    }
}