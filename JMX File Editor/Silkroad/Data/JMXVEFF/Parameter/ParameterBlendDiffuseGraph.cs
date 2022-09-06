using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterBlendDiffuseGraph : EEParameter<EEBlend<Color32, DiffuseBlend>>
    {
        public override string Name => "BlendDiffuseGraph";

        public ParameterBlendDiffuseGraph()
        {
            this.Value = new EEBlend<Color32, DiffuseBlend>();
        }

        public override void Read(BSReader reader)
        {
            this.Value.Read(reader);
        }

        public override void Write(BSWriter writer)
        {
            this.Value.Write(writer);
        }
    }
}