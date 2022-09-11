using JMXFileEditor.Silkroad.IO;


namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterFloat : EEParameter<float>
    {
        public override string Name => "float";

        public override void Deserialize(BSReader reader) => Value = reader.ReadSingle();

        public override void Serialize(BSWriter writer) => writer.Write(Value);
    }
}