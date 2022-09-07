using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Blends
{
    public class VectorBlend : DefaultBlend<Vector3>
    {
        public override void Read(BSReader reader)
        {
            base.Read(reader);
            Value = reader.ReadVector3();
        }

        public override void Write(BSWriter writer)
        {
            base.Write(writer);
            writer.Write(Value);
        }
    }
}