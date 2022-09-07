using JMXFileEditor.Silkroad.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF.Controller
{
    //LinkMode
    public class EFCLinkMode : EFController
    {
        public override string Name => "LinkMode";

        public uint Int0 { get; set; }
        public uint Int1 { get; set; }
        public uint Int2 { get; set; }
        public uint Int3 { get; set; }

        public override void Deserialize(BSReader reader)
        {
            Int0 = reader.ReadUInt32();
            Int1 = reader.ReadUInt32();
            Int2 = reader.ReadUInt32();
            Int3 = reader.ReadUInt32();
        }

        public override void Serialize(BSWriter writer)
        {
            writer.Write(Int0);
            writer.Write(Int1);
            writer.Write(Int2);
            writer.Write(Int3);
        }
    }
}