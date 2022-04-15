using JMXFileEditor.Silkroad.Data.Common;
using JMXFileEditor.Utility;
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
        #region Public Properties
        /// <summary>
        /// Original header used by Joymax
        /// </summary>
        public const string FileHeader = "JMXVBMT 0102";
        public string Header { get; set; }
        public List<CPrimMtrl> Materials { get; set; }
        #endregion

        #region Interface Implementation
        public string Format { get { return FileHeader; } }
        public string Extension { get; } = "bmt";
        public void Load(FileStream FileStream)
        {
            // Read file structure
            using (var br = new BinaryReader(FileStream, System.Text.Encoding.ASCII))
            {
                Header = new string(br.ReadChars(12));
                // Entries
                Materials = new List<CPrimMtrl>();
                var mtrlCount = br.ReadInt32();
                for (int i = 0; i < mtrlCount; i++)
                {
					// Create and add it
					CPrimMtrl mtrl = new CPrimMtrl();
                    Materials.Add(mtrl);
                    // Read
                    mtrl.Name = br.ReadString32();
                    mtrl.Diffuse = new Color4()
                    {
                        Red = br.ReadSingle(),
                        Green = br.ReadSingle(),
                        Blue = br.ReadSingle(),
                        Alpha = br.ReadSingle()
                    };
                    mtrl.Ambient = new Color4()
                    {
                        Red = br.ReadSingle(),
                        Green = br.ReadSingle(),
                        Blue = br.ReadSingle(),
                        Alpha = br.ReadSingle()
                    };
                    mtrl.Specular = new Color4()
                    {
                        Red = br.ReadSingle(),
                        Green = br.ReadSingle(),
                        Blue = br.ReadSingle(),
                        Alpha = br.ReadSingle()
                    };
                    mtrl.Emissive = new Color4()
                    {
                        Red = br.ReadSingle(),
                        Green = br.ReadSingle(),
                        Blue = br.ReadSingle(),
                        Alpha = br.ReadSingle()
                    };
                    mtrl.UnkFloat01 = br.ReadSingle();
                    mtrl.Flags = br.ReadUInt32(); // MaterialEntryFlags (64 is default often used with 256 and/or 512 only a few exceptions have 1 2 4 8...)
                    mtrl.DiffuseMapPath = br.ReadString32();
                    mtrl.UnkFloat02 = br.ReadSingle();
					mtrl.UnkByte01 = br.ReadByte();
					mtrl.UnkByte02 = br.ReadByte();
                    mtrl.IsAbsolutePath = br.ReadBoolean();
                    if((mtrl.Flags & (uint)PrimMtrlFlag.Bit14) != 0)
					{
						mtrl.NormalMapPath = br.ReadString32();
						mtrl.UnkUInt01 = br.ReadUInt32();
					}
                }
            }
        }
        public void Save(string Path)
        {
            // Override file structure
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path, FileMode.Create, FileAccess.Write), System.Text.Encoding.ASCII))
            {
                bw.Write(Header.ToCharArray());
                // Entries
                bw.Write(Materials.Count);
                foreach (var entry in Materials)
				{
					bw.WriteString32(entry.Name);
					// Diffuse
					bw.Write(entry.Diffuse.Red); bw.Write(entry.Diffuse.Green); bw.Write(entry.Diffuse.Blue); bw.Write(entry.Diffuse.Alpha);
					// Ambient
					bw.Write(entry.Ambient.Red); bw.Write(entry.Ambient.Green); bw.Write(entry.Ambient.Blue); bw.Write(entry.Ambient.Alpha);
					// Specular
					bw.Write(entry.Specular.Red); bw.Write(entry.Specular.Green); bw.Write(entry.Specular.Blue); bw.Write(entry.Specular.Alpha);
					// Emissive
					bw.Write(entry.Emissive.Red); bw.Write(entry.Emissive.Green); bw.Write(entry.Emissive.Blue); bw.Write(entry.Emissive.Alpha);

					bw.Write(entry.UnkFloat01);
                    bw.Write(entry.Flags);
                    bw.WriteString32(entry.DiffuseMapPath);
                    bw.Write(entry.UnkFloat02);
					bw.Write(entry.UnkByte01);
					bw.Write(entry.UnkByte02);
                    bw.Write(entry.IsAbsolutePath);

					if ((entry.Flags & (uint)PrimMtrlFlag.Bit14) != 0)
					{
						bw.WriteString32(entry.NormalMapPath);
						bw.Write(entry.UnkUInt01);
					}
                }
            }
        }
        #endregion
    }
}
