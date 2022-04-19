using JMXFileEditor.Silkroad.IO;

using System;
using System.Collections.Generic;
using System.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVBMT
{
    /// <summary>
    /// Joymax Binary Material File
    /// <para>https://github.com/DummkopfOfHachtenduden/SilkroadDoc/wiki/JMXVBMT </para>
    /// </summary>
    public class JMXVBMT_0102 : IJMXFile
    {
        /// <summary>
        /// Original header used by Joymax
        /// </summary>
        public const string LatestSignature = "JMXVBMT 0102";

        public List<PrimMtrl> Materials { get; set; }

        public string Format => LatestSignature;

        public string Extension { get; } = "bmt";

        public void Load(Stream stream)
        {
            // Read file structure
            using (var reader = new BSReader(stream))
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
                {
                    // Create and add it
                    PrimMtrl mtrl = new PrimMtrl();
                    Materials.Add(mtrl);
                    // Read
                    mtrl.Name = reader.ReadString();
                    mtrl.Diffuse = reader.ReadColor4();
                    mtrl.Ambient = reader.ReadColor4();
                    mtrl.Specular = reader.ReadColor4();
                    mtrl.Emissive = reader.ReadColor4();
                    mtrl.UnkFloat01 = reader.ReadSingle();
                    mtrl.Flags = reader.ReadUInt32(); // MaterialEntryFlags (64 is default often used with 256 and/or 512 only a few exceptions have 1 2 4 8...)

                    mtrl.DiffuseMapPath = reader.ReadString();
                    mtrl.UnkFloat02 = reader.ReadSingle();
                    mtrl.UnkByte01 = reader.ReadByte();
                    mtrl.UnkByte02 = reader.ReadByte();
                    mtrl.IsAbsolutePath = reader.ReadBoolean();
                    if ((mtrl.Flags & (uint)PrimMtrlFlag.Bit14) != 0)
                    {
                        mtrl.NormalMapPath = reader.ReadString();
                        mtrl.UnkUInt01 = reader.ReadUInt32();
                    }
                }
            }
        }

        public void Save(string path)
        {
            // Override file structure
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var writer = new BSWriter(stream))
            {
                writer.Write(LatestSignature, 12);
                writer.Write(Materials.Count);
                foreach (var mtrl in Materials)
                {
                    writer.Write(mtrl.Name);
                    writer.Write(mtrl.Diffuse);
                    writer.Write(mtrl.Ambient);
                    writer.Write(mtrl.Specular);
                    writer.Write(mtrl.Emissive);
                    writer.Write(mtrl.UnkFloat01);
                    writer.Write(mtrl.Flags);
                    writer.Write(mtrl.DiffuseMapPath);
                    writer.Write(mtrl.UnkFloat02);
                    writer.Write(mtrl.UnkByte01);
                    writer.Write(mtrl.UnkByte02);
                    writer.Write(mtrl.IsAbsolutePath);

                    if ((mtrl.Flags & (uint)PrimMtrlFlag.Bit14) != 0)
                    {
                        writer.Write(mtrl.NormalMapPath);
                        writer.Write(mtrl.UnkUInt01);
                    }
                }
            }
        }
    }
}