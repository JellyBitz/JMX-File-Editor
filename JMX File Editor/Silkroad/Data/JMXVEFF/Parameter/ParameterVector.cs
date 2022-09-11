using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterVector3 : EEParameter<Vector3>
    {
        public override string Name => "Vector3";

        public override void Deserialize(BSReader reader) => Value = reader.ReadVector3();

        public override void Serialize(BSWriter writer) => writer.Write(Value);
    }
}