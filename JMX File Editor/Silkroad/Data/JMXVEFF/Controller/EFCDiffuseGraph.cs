using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Controller
{
    //DiffuseGraph
    public class EFCDiffuseGraph : EFController
    {
        public override string Name => "DiffuseGraph";

        public EEBlend<byte, ByteBlend> ByteBlend { get; } = new EEBlend<byte, ByteBlend>();
        public EEBlend<Color32, DiffuseBlend> DiffuseBlend { get; } = new EEBlend<Color32, DiffuseBlend>();

        public override void Deserialize(BSReader reader)
        {
            ByteBlend.Deserialize(reader);
            DiffuseBlend.Deserialize(reader);
        }

        public override void Serialize(BSWriter writer)
        {
            writer.Serialize(ByteBlend);
            writer.Serialize(DiffuseBlend);
        }
    }
}