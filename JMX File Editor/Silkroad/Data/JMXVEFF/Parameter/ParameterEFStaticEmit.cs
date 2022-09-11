using JMXFileEditor.Silkroad.IO;


namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter
{
    public class ParameterEFStaticEmit : EEParameter<EFStaticEmit>
    {
        public override string Name => "EFStaticEmit";

        public override void Deserialize(BSReader reader) => Value = reader.Deserialize<EFStaticEmit>();

        public override void Serialize(BSWriter writer) => writer.Serialize(Value);
    }
}