using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Controller
{
    //Program
    public class EFCProgram : EFController
    {
        public override string Name => "Program";

        public EEStaticProgram StaticProgram { get; } = new EEStaticProgram();

        public override void Read(BSReader reader) => this.StaticProgram.Read(reader);
    }
}