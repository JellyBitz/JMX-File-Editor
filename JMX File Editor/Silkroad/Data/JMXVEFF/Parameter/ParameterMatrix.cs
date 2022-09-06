using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterMatrix : EEParameter<Matrix4x4>
    {
        public override string Name => "Matrix";

        public override void Read(BSReader reader)
        {
            Value = reader.ReadMatrix4x4();
        }

        public override void Write(BSWriter writer)
        {
            writer.Write(Value);
        }
    }
}