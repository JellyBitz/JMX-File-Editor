using JMXFileEditor.Silkroad.IO;

using System;
using System.IO;
using System.Text;

namespace JMXFileEditor.Silkroad.Data.JMXVEFF
{
    /// <summary>
    /// Joymax Visual Effects File
    /// <para>https://github.com/DummkopfOfHachtenduden/SilkroadDoc/wiki/JMXVEFF</para>
    /// </summary>
    public class EFStoredEffect : EFStoredObject, IJMXFile
    {
        #region Public Properties
        public float Scale { get; set; } = 1f;
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

                // Check file version
                var version = reader.ReadString(4);
                var ver = int.Parse(version);

                // Supported formats
                if (ver < 10 || ver > 13)
                    throw new NotSupportedException($"Unsupported version: {version}");
               
                // Read stuff based on file version
                if(ver >= 12 && ver <= 13)
                    Scale = reader.ReadFloat();
                if (ver == 13)
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

                writer.Write(Scale);

                writer.Write(Version13Value0);
                writer.Write(Version13Value1);
                writer.Write(Version13Value2);

                base.Serialize(writer);
            }
        }
        #endregion
    }
}