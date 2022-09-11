using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Blends
{
    public class DiffuseBlend : DefaultBlend<Color32>
    {

        public DiffuseBlend()
        {
            Value = new Color32();
        }

        public override void Read(BSReader reader)
        {
            base.Read(reader);
            Value = reader.ReadColor32();
        }

        public override void Write(BSWriter writer)
        {
            base.Write(writer);
            writer.Write(Value);
        }
    }
}