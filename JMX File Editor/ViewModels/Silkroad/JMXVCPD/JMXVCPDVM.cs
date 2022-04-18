using JMXFileEditor.Silkroad.Data.JMXVCPD;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVCPD
{
	/// <summary>
	/// ViewModel representing the JMXVRES file format
	/// </summary>
	public class JMXVCPDVM : JMXStructure
	{
		#region Constructor
		public JMXVCPDVM(JMXVCPD_0101 JMXFile) : base(JMXFile.Format, true)
		{
			CreateNodes(JMXFile);
		}
		#endregion

		#region Public Methods
		public override object GetClassFrom(JMXStructure Structure)
		{
			JMXVCPD_0101 file = new JMXVCPD_0101();

			// Signature
			file.Header = (string)((JMXAttribute)Structure.Childs[0]).Value;

			// Unknown flags
			file.UnkUInt01 = (uint)((JMXAttribute)Structure.Childs[3]).Value;
			file.UnkUInt02 = (uint)((JMXAttribute)Structure.Childs[4]).Value;
			file.UnkUInt03 = (uint)((JMXAttribute)Structure.Childs[5]).Value;
			file.UnkUInt04 = (uint)((JMXAttribute)Structure.Childs[6]).Value;
			file.UnkUInt05 = (uint)((JMXAttribute)Structure.Childs[7]).Value;

			// Object info
			file.Type = (uint)((JMXAttribute)Structure.Childs[8]).Value;
			file.Name = (string)((JMXAttribute)Structure.Childs[9]).Value;
			file.UnkUInt06 = (uint)((JMXAttribute)Structure.Childs[10]).Value;
			file.UnkUInt07 = (uint)((JMXAttribute)Structure.Childs[11]).Value;

			// FileOffset.Collision
			file.CollisionResourcePath = (string)((JMXAttribute)Structure.Childs[12]).Value;

			// FileOffset.Resource
			file.ResourceSet = ((JMXStructure)Structure.Childs[13]).GetChildList<string>();

			return file;
		}
		#endregion

		#region Private Helpers
		/// <summary>
		/// Create UI format
		/// </summary>
		private void CreateNodes(JMXVCPD_0101 JMXFile)
		{
			// Signature
			Childs.Add(new JMXAttribute("Header", JMXFile.Header, false));

			// FileOffsets
			Childs.Add(new JMXAttribute("FileOffset.Collision", JMXFile.CollisionFileOffset, false));
			Childs.Add(new JMXAttribute("FileOffset.Resource", JMXFile.ResourceFileOffset, false));
			// Unknown
			Childs.Add(new JMXAttribute("UnkUInt01", JMXFile.UnkUInt01));
			Childs.Add(new JMXAttribute("UnkUInt02", JMXFile.UnkUInt02));
			Childs.Add(new JMXAttribute("UnkUInt03", JMXFile.UnkUInt03));
			Childs.Add(new JMXAttribute("UnkUInt04", JMXFile.UnkUInt04));
			Childs.Add(new JMXAttribute("UnkUInt05", JMXFile.UnkUInt05));

			// Object info
			Childs.Add(new JMXAttribute("ObjInfo.Type", JMXFile.Type));
			Childs.Add(new JMXAttribute("ObjInfo.Name", JMXFile.Name));
			Childs.Add(new JMXAttribute("ObjInfo.UnkUInt06", JMXFile.UnkUInt06));
			Childs.Add(new JMXAttribute("ObjInfo.UnkUInt07", JMXFile.UnkUInt07));

			// FileOffset.Collision
			Childs.Add(new JMXAttribute("CollisionResourcePath", JMXFile.CollisionResourcePath));

			// FileOffset.Resource
			AddChildArray("ResourceSet", JMXFile.ResourceSet.ToArray(), true, true);
		}
		#endregion
	}
}
