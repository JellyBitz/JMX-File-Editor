using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Controller
{
    //StaticEmit
    public class EFCStaticEmit : EFController
    {
        public override string Name => "StaticEmit";

        public EFStaticEmit StaticEmit { get; set; }

        public override void Deserialize(BSReader reader)
        {
            StaticEmit = reader.Deserialize<EFStaticEmit>();
        }

        public override void Serialize(BSWriter writer)
        {
            writer.Serialize(StaticEmit);
        }
    }
}