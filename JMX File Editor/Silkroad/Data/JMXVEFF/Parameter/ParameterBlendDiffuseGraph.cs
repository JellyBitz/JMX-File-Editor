using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterBlendDiffuseGraph : EEParameter<EEBlend<Color32, DiffuseBlend>>
    {
        public override string Name => "BlendDiffuseGraph";


        public override void Deserialize(BSReader reader) => Value.Deserialize(reader);

        public override void Serialize(BSWriter writer) => Value.Serialize(writer);
    }
}