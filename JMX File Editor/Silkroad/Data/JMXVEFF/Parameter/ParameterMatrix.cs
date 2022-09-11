using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterMatrix : EEParameter<Matrix4x4>
    {
        public override string Name => "Matrix";

        public override void Deserialize(BSReader reader) => Value = reader.ReadMatrix4x4();

        public override void Serialize(BSWriter writer) => writer.Write(Value);
    }
}