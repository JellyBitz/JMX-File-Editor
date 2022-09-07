using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterBlendScaleGraphPointer : EEParameter<float>
    {
        public override string Name => "BlendScaleGraphPointer";

        public override void Deserialize(BSReader reader) => Value = reader.ReadFloat();

        public override void Serialize(BSWriter writer) => writer.Write(Value);
    }
}