using JMXFileEditor.Silkroad.Data.JMXVRES;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVRES
{
	public class CPrimAniVM : JMXStructure
	{
		#region Constructor
		public CPrimAniVM(string Name, CPrimAni Animation) : base(Name, true)
		{
			Childs.Add(new JMXAttribute("Path", Animation.Path));
		}
		#endregion

		#region Public Methods
		public override object GetClassFrom(JMXStructure Structure)
		{
			CPrimAni ani = new CPrimAni()
			{
				Path = (string)((JMXAttribute)Structure.Childs[0]).Value,
			};
			return ani;
		}
		#endregion
	}
}
