using JMXFileEditor.Silkroad.Data.Common;
using JMXFileEditor.Utility;
using System.Collections.Generic;
using System.IO;
namespace JMXFileEditor.Silkroad.Data.JMXVCPD
{
	/// <summary>
	/// Joymax Compound File
	/// <para>https://github.com/DummkopfOfHachtenduden/SilkroadDoc/wiki/JMXVCPD </para>
	/// </summary>
	public class JMXVCPD_0101 : IJMXFile
	{
		#region Public Properties
		/// <summary>
		/// Original header used by Joymax
		/// </summary>
		public const string FileHeader = "JMXVCPD 0101";
		public string Header { get; set; }
		public uint CollisionFileOffset { get; private set; }
		public uint ResourceFileOffset { get; private set; }
		public uint UnkUInt01 { get; set; }
		public uint UnkUInt02 { get; set; }
		public uint UnkUInt03 { get; set; }
		public uint UnkUInt04 { get; set; }
		public uint UnkUInt05 { get; set; }
		public uint Type { get; set; }
		public string Name { get; set; } = string.Empty;
		public uint UnkUInt06 { get; set; }
		public uint UnkUInt07 { get; set; }
		public string CollisionResourcePath { get; set; } = string.Empty;
		public List<string> ResourceSet { get; set; } = new List<string>();
		#endregion

		#region Public Methods
		/// <summary>
		/// Calculate and update the values from file offsets
		/// </summary>
		public void UpdateFileOffsets()
		{
			CollisionFileOffset = (uint)(12 + (4 * 2) + (5 * 4) + (4 + (4 + Name.Length) + 4 * 2));
			ResourceFileOffset = (uint)(CollisionFileOffset + (4 + CollisionResourcePath.Length));
		}
		#endregion

		#region Interface Implementation
		public string Format { get { return FileHeader; } }
		public string Extension { get; } = "bsr";

		public void Load(FileStream FileStream)
		{
			// Read file structure
			using (var br = new BinaryReader(FileStream, System.Text.Encoding.ASCII))
			{
				// Signature
				Header = new string(br.ReadChars(12));

				// File Offsets
				CollisionFileOffset = br.ReadUInt32();
				ResourceFileOffset = br.ReadUInt32();
				//Flags? 
				UnkUInt01 = br.ReadUInt32();
				UnkUInt02 = br.ReadUInt32();
				UnkUInt03 = br.ReadUInt32();
				UnkUInt04 = br.ReadUInt32();
				UnkUInt05 = br.ReadUInt32();

				// Object info
				Type = br.ReadUInt32();
				Name = br.ReadString32();
				UnkUInt01 = br.ReadUInt32();
				UnkUInt02 = br.ReadUInt32();

				// FileOffset.Collision
				CollisionResourcePath = br.ReadString32();

				// Offset.Resource
				int count = br.ReadInt32();
				ResourceSet = new List<string>();
				for (int i = 0; i < count; i++)
				{
					ResourceSet.Add(br.ReadString32());
				}
			}
		}
		public void Save(string Path)
		{
			// Override file structure
			using (BinaryWriter bw = new BinaryWriter(new FileStream(Path, FileMode.Create, FileAccess.Write), System.Text.Encoding.ASCII))
			{
				// Signature
				bw.Write(Header.ToCharArray());

				// Recalculate file offsets for safety
				UpdateFileOffsets();

				// File Offsets
				bw.Write(CollisionFileOffset);
				bw.Write(ResourceFileOffset);
				// Unknown
				bw.Write(UnkUInt01);
				bw.Write(UnkUInt02);
				bw.Write(UnkUInt03);
				bw.Write(UnkUInt04);
				bw.Write(UnkUInt05);

				// Object Info
				bw.Write(Type);
				bw.WriteString32(Name);
				bw.Write(UnkUInt01);
				bw.Write(UnkUInt02);

				// FileOffset.Collision
				bw.WriteString32(CollisionResourcePath);

				// FileOffset.Material
				bw.Write(ResourceSet.Count);
				for (int i = 0; i < ResourceSet.Count; i++)
				{
					bw.WriteString32(ResourceSet[i]);
				}
			}
		}
		#endregion
	}
}