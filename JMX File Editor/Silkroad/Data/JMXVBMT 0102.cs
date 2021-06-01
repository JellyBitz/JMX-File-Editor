using JMXFileEditor.Utility;
using System;
using System.Collections.Generic;
using System.IO;

namespace JMXFileEditor.Silkroad.Data
{
    class JMXVBMT_0102 : IJMXFile
    {
        #region Public Properties
        /// <summary>
        /// Original header used by Joymax
        /// </summary>
        public const string FileHeader = "JMXVBMT 0102";
        public string Header { get; set; }
        public List<Entry> Entries { get; set; }
        #endregion

        #region Interface Implementation
        public string Format { get { return FileHeader; } }
        public string Extension { get; } = "bmt";
        public void Load(FileStream FileStream)
        {
            // Read file structure
            using (var br = new BinaryReader(FileStream))
            {
                Header = new string(br.ReadChars(12));
                // Entries
                Entries = new List<Entry>();
                var entryCount = br.ReadUInt32();
                for (int i = 0; i < entryCount; i++)
                {
                    // Create and add it
                    Entry entry = new Entry();
                    Entries.Add(entry);
                    // Read
                    entry.Name = br.ReadString32();
                    entry.Diffuse = new Color4()
                    {
                        Red = br.ReadSingle(),
                        Green = br.ReadSingle(),
                        Blue = br.ReadSingle(),
                        Alpha = br.ReadSingle()
                    };
                    entry.Ambient = new Color4()
                    {
                        Red = br.ReadSingle(),
                        Green = br.ReadSingle(),
                        Blue = br.ReadSingle(),
                        Alpha = br.ReadSingle()
                    };
                    entry.Specular = new Color4()
                    {
                        Red = br.ReadSingle(),
                        Green = br.ReadSingle(),
                        Blue = br.ReadSingle(),
                        Alpha = br.ReadSingle()
                    };
                    entry.Emissive = new Color4()
                    {
                        Red = br.ReadSingle(),
                        Green = br.ReadSingle(),
                        Blue = br.ReadSingle(),
                        Alpha = br.ReadSingle()
                    };
                    entry.UnkFloat01 = br.ReadSingle(); // Specular Power?   (ranging from ~0.8 to ~189)
                    entry.UnkUInt01 = br.ReadUInt32(); // MaterialEntryFlags (64 is default often used with 256 and/or 512 only a few exceptions have 1 2 4 8...)
                    entry.DiffuseMap = br.ReadString32();
                    entry.UnkFloat02 = br.ReadSingle(); // 1.0
                    entry.UnkUShort01 = br.ReadUInt16(); // 0, 24, 2080
                    entry.IsAtOtherDirectory = br.ReadBoolean();
                    if((entry.UnkUInt01 & 0x2000) != 0)
                    {
                        entry.NormalMap = br.ReadString32();
                    }
                }
            }
        }
        public void Save(string Path)
        {

        }
        #endregion

        #region Internal classes
        /// <summary>
        /// Represents an entry into the bmt file
        /// </summary>
        public class Entry
        {
            public string Name { get; set; } = string.Empty;
            public Color4 Diffuse { get; set; }
            public Color4 Ambient { get; set; }
            public Color4 Specular { get; set; }
            public Color4 Emissive { get; set; }
            public float UnkFloat01 { get; set; }
            public uint UnkUInt01 { get; set; }
            public string DiffuseMap { get; set; } = string.Empty;
            public float UnkFloat02 { get; set; }
            public ushort UnkUShort01 { get; set; }
            // Indicates whether or not the DiffuseMap is not at the same directory as the material set
            public bool IsAtOtherDirectory { get; set; }
            public string NormalMap { get; set; } = string.Empty;
        }
        /// <summary>
        /// Color representation
        /// </summary>
        public class Color4
        {
            public float Red { get; set; }
            public float Green { get; set; }
            public float Blue { get; set; }
            public float Alpha { get; set; }
        }
        #endregion
    }
}
