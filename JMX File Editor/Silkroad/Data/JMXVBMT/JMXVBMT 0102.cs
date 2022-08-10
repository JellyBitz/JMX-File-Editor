using JMXFileEditor.Silkroad.IO;

using System;
using System.Collections.Generic;
using System.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVBMT
{
    /// <summary>
    /// Joymax Binary Material File
    /// <para>https://github.com/DummkopfOfHachtenduden/SilkroadDoc/wiki/JMXVBMT</para>
    /// </summary>
    public class JMXVBMT_0102 : IJMXFile
    {
        #region Public Properties
        /// <summary>
        /// Original header used by Joymax
        /// </summary>
        public const string LatestSignature = "JMXVBMT 0102";
        public List<PrimMtrl> Materials { get; set; } = new List<PrimMtrl>();
        public string Format => LatestSignature;
        public string Extension { get; } = "bmt";
        #endregion

        #region Interface Implementations
        public void Load(Stream stream)
        {
            // Read file structure (CP949)
            using (var reader = new BSReader(stream, System.Text.Encoding.GetEncoding(949)))
            {
                var signature = reader.ReadString(12);
                if (signature != LatestSignature)
                {
                    // TODO: Migrate old version to current if possible.
                    throw new NotSupportedException($"Migration from '{signature}' not supported.");
                }

                // Materials
                var mtrlCount = reader.ReadInt32();
                Materials = new List<PrimMtrl>(mtrlCount);
                for (int i = 0; i < mtrlCount; i++)
                    this.Materials.Add(reader.Deserialize<PrimMtrl>());
            }
        }
        public void Save(string path)
        {
            // Override file structure
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var writer = new BSWriter(stream, System.Text.Encoding.GetEncoding(949)))
            {
                writer.Write(LatestSignature.ToCharArray());
                writer.Write(Materials.Count);
                foreach (var mtrl in Materials)
                    writer.Serialize(mtrl);
            }
        }
        #endregion
    }
}