using JMXFileEditor.Silkroad.Data.JMXVRES;
namespace JMXFileEditor.ViewModels.Silkroad.JMXVRES
{
	public class CPrimMtrlSetVM : JMXStructure
	{
		#region Constructor
		public CPrimMtrlSetVM(string Name, PrimMtrlSet MtrlSet) : base(Name, true)
		{
			Childs.Add(new JMXAttribute("Index", MtrlSet.Index));
			Childs.Add(new JMXAttribute("Path", MtrlSet.Path));
		}
		#endregion

		#region Public Methods
		public override object GetClassFrom(JMXStructure Structure)
		{
			PrimMtrlSet mtrlSet = new PrimMtrlSet()
			{
				Index = (uint)((JMXAttribute)Structure.Childs[0]).Value,
				Path = (string)((JMXAttribute)Structure.Childs[1]).Value
			};
			return mtrlSet;
		}
		#endregion
	}
}
