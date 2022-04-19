using JMXFileEditor.Silkroad.Data.JMXVRES;
namespace JMXFileEditor.ViewModels.Silkroad.JMXVRES
{
	public class CModDataSetVM : JMXStructure
	{
		#region Constructor
		public CModDataSetVM(string Name, CModDataSet DataSet) : base(Name, true)
		{
			// Add new format
			m_SupportedFormats.Add(typeof(CModData), (s, e) => {
				// set default case
				CModData obj = e.Obj == null ? new CModDataMtrl() : (CModData)e.Obj;
				e.Childs.Add(new CModDataVM("[" + e.Childs.Count + "]",
					JMXAbstract.GetTypes(
						typeof(CModDataMtrl),
						typeof(CModDataTexAni),
						typeof(CModDataMultiTex),
						typeof(CModDataMultiTexRev),
						typeof(CModDataParticle),
						typeof(CModDataEnvMap),
						typeof(CModDataBumpEnv),
						typeof(CModDataSound),
						typeof(CModDataDyVertex),
						typeof(CModDataDyJoint),
						typeof(CModDataDyLattice),
						typeof(CModDataProgEquipPow)),
					obj.GetType(),obj));
			});
			// Create nodes
			Childs.Add(new JMXAttribute("Type", DataSet.Type));
			Childs.Add(new JMXOption("AnimationType", DataSet.AnimationType, JMXOption.GetValues<object>(typeof(CPrimAnimationType))));
			Childs.Add(new JMXAttribute("Name", DataSet.Name));
			AddChildArray("ModsData", DataSet.ModsData.ToArray(), true, true);
		}
		#endregion

		#region Public Methods
		public override object GetClassFrom(JMXStructure Structure)
		{
			CModDataSet obj = new CModDataSet()
			{
				Type = (uint)((JMXAttribute)Structure.Childs[0]).Value,
				AnimationType = (CPrimAnimationType)((JMXOption)Childs[1]).Value,
				Name = (string)((JMXAttribute)Structure.Childs[2]).Value,
				ModsData = ((JMXStructure)Structure.Childs[3]).GetChildList<CModData>()
			};
			return obj;
		}
		#endregion
	}
}
