using JMXFileEditor.Silkroad.IO;


namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterFloat : EEParameter<float>
    {
        public override string Name => "float";

        public override void Read(BSReader reader)
        {
            Value = reader.ReadSingle();
        }

        public override void Write(BSWriter writer)
        {
            writer.Write(Value);
        }
    }
}