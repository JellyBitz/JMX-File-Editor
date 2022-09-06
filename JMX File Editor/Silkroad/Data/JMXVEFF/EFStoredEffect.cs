using JMXFileEditor.Silkroad.IO;

using System;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public class EFStoredEffect : EFStoredObject
    {
        public int Version12Value { get; private set; }
        public int Version13Value0 { get; private set; }
        public int Version13Value1 { get; private set; }
        public int Version13Value2 { get; private set; }

        public override void Read(BSReader reader)
        {
            var format = reader.ReadString(8);
            if (format != "JMXVEFF ")
                throw new Exception($"Invalid file signature: {format}");

            var version = reader.ReadString(4);
            if (version != "0011" && version != "0012" && version != "0013")
                throw new Exception($"Unsupported version: {version}");

            if (version == "0012" || version == "0013")
            {
                this.Version12Value = reader.ReadInt32();
            }
            if (version == "0013")
            {
                this.Version13Value0 = reader.ReadInt32();
                this.Version13Value1 = reader.ReadInt32();
                this.Version13Value2 = reader.ReadInt32();
            }
            base.Read(reader);
        }
    }
}