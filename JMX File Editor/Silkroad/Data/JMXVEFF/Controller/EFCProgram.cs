using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Controller
{
    //Program
    public class EFCProgram : EFController
    {
        public override string Name => "Program";

        public EEStaticProgram StaticProgram { get; set; } = new EEStaticProgram();

        public override void Deserialize(BSReader reader) => StaticProgram.Deserialize(reader);

        public override void Serialize(BSWriter writer) => writer.Serialize(StaticProgram);
    }
}