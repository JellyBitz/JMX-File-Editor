using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Controller
{
    //DiffuseGraph
    public class EFCDiffuseGraph : EFController
    {
        public override string Name => "DiffuseGraph";

        public EEBlend<byte, ByteBlend> ByteBlend { get; }
        public EEBlend<Color32, DiffuseBlend> DiffuseBlend { get; }

        public EFCDiffuseGraph()
        {
            ByteBlend = new EEBlend<byte, ByteBlend>();
            DiffuseBlend = new EEBlend<Color32, DiffuseBlend>();
        }

        public override void Read(BSReader reader)
        {
            ByteBlend.Read(reader);
            DiffuseBlend.Read(reader);
        }
    }
}