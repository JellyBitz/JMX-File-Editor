

using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;


using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterBlendScaleGraph : EEParameter<EEBlend<Vector3, VectorBlend>>
    {
        public override string Name => "BlendScaleGraph";

        public ParameterBlendScaleGraph()
        {
            this.Value = new EEBlend<Vector3, VectorBlend>();
        }

        public override void Read(BSReader reader)
        {
            this.Value.Read(reader);
        }

        public override void Write(BSWriter writer)
        {
            //this.Value.Write(writer);
        }
    }
}