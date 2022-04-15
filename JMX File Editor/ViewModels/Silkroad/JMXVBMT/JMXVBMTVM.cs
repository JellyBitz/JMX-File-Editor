using JMXFileEditor.Silkroad.Data.JMXVBMT;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVBMT
{
	/// <summary>
	/// ViewModel representing the JMXVRES file format
	/// </summary>
	public class JMXVBMTVM : JMXStructure
	{
		#region Constructor
		public JMXVBMTVM(JMXVBMT_0102 JMXFile) : base(JMXFile.Format, true)
		{
			AddChildFormats();
			CreateNodes(JMXFile);
		}
		#endregion

		#region Public Methods
		public override object GetClassFrom(JMXStructure Structure)
		{
			JMXVBMT_0102 file = new JMXVBMT_0102();

			// Signature
			file.Header = (string)((JMXAttribute)Structure.Childs[0]).Value;
			// MaterialSet
			file.Materials = ((JMXStructure)Structure.Childs[1]).GetChildList<CPrimMtrl>();

			return file;
		}
		#endregion

		#region Private Helpers
		/// <summary>
		/// Add new node formats
		/// </summary>
		private void AddChildFormats()
		{
			AddFormatHandler(typeof(CPrimMtrl), (s, e) => {
				e.Childs.Add(new CPrimMtrlVM("[" + e.Childs.Count + "]", e.Obj is CPrimMtrl _obj ? _obj : new CPrimMtrl()));
			});
		}
		/// <summary>
		/// Create UI format
		/// </summary>
		private void CreateNodes(JMXVBMT_0102 JMXFile)
		{
			// Signature
			Childs.Add(new JMXAttribute("Header", JMXFile.Header, false));
			// MaterialSet
			AddChildArray("Materials", JMXFile.Materials.ToArray(), true, true);
		}
		#endregion
	}
}
