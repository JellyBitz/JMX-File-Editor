using JMXFileEditor.Silkroad.Data.Common;
using JMXFileEditor.Silkroad.IO;

using System;
using System.Collections.Generic;
using System.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVCPD
{
    /// <summary>
    /// Joymax Compound File
    /// <para>https://github.com/DummkopfOfHachtenduden/SilkroadDoc/wiki/JMXVCPD</para>
    /// </summary>
    public class JMXVCPD_0101 : IJMXFile
    {
        #region Public Properties
        /// <summary>
        /// Original header used by Joymax
        /// </summary>
        public const string LatestSignature = "JMXVCPD 0101";
        public uint Int01 { get; set; }
        public uint Int02 { get; set; }
        public uint Int03 { get; set; }
        public uint Int04 { get; set; }
        public uint Int05 { get; set; }
        public ObjectGeneralInfo ObjectInfo { get; set; }
        public string CollisionResourcePath { get; set; } = string.Empty;
        public List<string> ResourceSet { get; set; }
        #endregion Public Properties

        #region Interface Implementation
        public string Format => LatestSignature;
        public string Extension { get; } = "bsr";
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

                // File offsets (Collision, Resources)
                reader.SkipRead(8);

                // Unknown
                Int01 = reader.ReadUInt32();
                Int02 = reader.ReadUInt32();
                Int03 = reader.ReadUInt32();
                Int04 = reader.ReadUInt32();
                Int05 = reader.ReadUInt32();

                // Object info
                this.ObjectInfo = reader.Deserialize<ObjectGeneralInfo>();

                // FileOffset.Collision
                CollisionResourcePath = reader.ReadString();

                // FileOffset.Resources
                var count = reader.ReadInt32();
                ResourceSet = new List<string>(count);
                for (int i = 0; i < count; i++)
                    ResourceSet.Add(reader.ReadString());
            }
        }
        public void Save(string path)
        {
            // Override file structure
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var writer = new BSWriter(stream, System.Text.Encoding.GetEncoding(949)))
            {
                // Signature
                writer.Write(LatestSignature, 12);

                // Reserved file offsets
                writer.Write(0); // CollisionOffset
                writer.Write(0); // ResourceOffset

                // Unknown
                writer.Write(Int01);
                writer.Write(Int02);
                writer.Write(Int03);
                writer.Write(Int04);
                writer.Write(Int05);

                // Object Info
                writer.Serialize(this.ObjectInfo);

                // FileOffset.Collision
                var collisionOffset = (int)stream.Position;
                writer.Write(CollisionResourcePath);

                // FileOffset.Resources
                var resourceOffset = (int)stream.Position;
                writer.Write(ResourceSet.Count);
                for (int i = 0; i < ResourceSet.Count; i++)
                    writer.Write(ResourceSet[i]);

                // write offsets
                writer.Seek(12, SeekOrigin.Begin);
                writer.Write(collisionOffset);
                writer.Write(resourceOffset);
            }
        }
        #endregion Interface Implementation
    }
}