using JMXFileEditor.Silkroad.IO;

using System;
using System.IO;
using System.Text;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    public class EFStoredEffect : EFStoredObject, IJMXFile
    {
        #region Public Properties
        public float Version12Value { get; set; } = 1.0f;
        public int Version13Value0 { get; set; } = 0;
        public int Version13Value1 { get; set; } = 0;
        public int Version13Value2 { get; set; } = 0;
        #endregion

        #region Interface Implementation
        public string Format => "JMXVEFF";
        public string Extension => "efp";
        public void Load(Stream stream)
        {
            // Read file structure (CP949)
            using (var reader = new BSReader(stream, Encoding.GetEncoding(949)))
            {
                var format = reader.ReadString(8);
                if (format != "JMXVEFF ")
                    throw new Exception($"Invalid file signature: {format}");

                var version = reader.ReadString(4);
                if (version != "0011" && version != "0012" && version != "0013")
                    throw new NotSupportedException($"Unsupported version: {version}");

                if (version == "0012" || version == "0013")
                {
                    Version12Value = reader.ReadFloat();
                }
                if (version == "0013")
                {
                    Version13Value0 = reader.ReadInt32();
                    Version13Value1 = reader.ReadInt32();
                    Version13Value2 = reader.ReadInt32();
                }

                base.Deserialize(reader);
            }
        }
        public void Save(string path)
        {
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var writer = new BSWriter(stream, Encoding.GetEncoding(949)))
            {
                // Auto-upgrade everything saved to JMXVEFF 0013
                writer.Write("JMXVEFF 0013".ToCharArray());

                writer.Write(Version12Value);

                writer.Write(Version13Value0);
                writer.Write(Version13Value1);
                writer.Write(Version13Value2);

                base.Serialize(writer);
            }
        }
        #endregion
    }
}