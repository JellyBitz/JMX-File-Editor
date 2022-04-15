using JMXFileEditor.Silkroad.Data.JMXVRES;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVRES
{
	public class CResAttachableVM : JMXStructure
	{
		#region Constructor
		public CResAttachableVM(string Name, CResAttachable Resource) : base(Name, true)
		{
			// add format
			AddFormatHandler(typeof(CResAttachable.Slot), (s,e) => {
				e.Childs.Add(new Slot("[" + e.Childs.Count + "]", e.Obj is CResAttachable.Slot _obj ? _obj : new CResAttachable.Slot()));
			});
			// create nodes
			Childs.Add(new JMXAttribute("UnkUInt01", Resource.UnkUInt01));
			Childs.Add(new JMXAttribute("UnkUInt02", Resource.UnkUInt02));
			Childs.Add(new JMXAttribute("AttachMethod", Resource.AttachMethod));
			AddChildArray("Slots", Resource.Slots.ToArray(), true, true);
			Childs.Add(new JMXAttribute("nComboNum", Resource.nComboNum));
		}
		#endregion

		#region Public Methods
		public override object GetClassFrom(JMXStructure Structure)
		{
			CResAttachable obj = new CResAttachable()
			{
				UnkUInt01 = (uint)((JMXAttribute)Structure.Childs[0]).Value,
				UnkUInt02 = (uint)((JMXAttribute)Structure.Childs[1]).Value,
				AttachMethod = (uint)((JMXAttribute)Structure.Childs[2]).Value,
				Slots = ((JMXStructure)Structure.Childs[3]).GetChildList<CResAttachable.Slot>(),
				nComboNum = (uint)((JMXAttribute)Structure.Childs[4]).Value,
			};
			return obj;
		}
		#endregion

		#region Internal Classes
		public class Slot : JMXStructure
		{
			public Slot(string Name, CResAttachable.Slot Slot) : base(Name, true)
			{
				Childs.Add(new JMXAttribute("Index", Slot.Index));
				Childs.Add(new JMXAttribute("MeshSetIndex", Slot.MeshSetIndex));
			}
			public override object GetClassFrom(JMXStructure Structure)
			{
				CResAttachable.Slot obj = new CResAttachable.Slot()
				{
					Index = (uint)((JMXAttribute)Structure.Childs[0]).Value,
					MeshSetIndex = (uint)((JMXAttribute)Structure.Childs[1]).Value,
				};
				return obj;
			}
		}
		#endregion
	}
}
