using JMXFileEditor.Silkroad.Data.JMXVRES;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Common;
namespace JMXFileEditor.ViewModels.Silkroad.JMXVRES
{
    public class CPrimAniGroupVM : JMXStructure
	{
		#region Constructor
		public CPrimAniGroupVM(string Name, PrimAniGroup AniGroup) : base(Name, true)
		{
			// Add new format
			m_SupportedFormats.Add(typeof(PrimAniGroup.PrimAniTypeData), (s, e) => {
				e.Childs.Add(new Entry("[" + e.Childs.Count + "]", e.Obj is PrimAniGroup.PrimAniTypeData _obj ? _obj : new PrimAniGroup.PrimAniTypeData()));
			});
			// Create viewmodel node
			Childs.Add(new JMXAttribute("Name", AniGroup.Name));
			AddChildArray("Entries", AniGroup.Entries.ToArray(), true, true);
		}
		#endregion

		#region Public Methods
		public override object GetClassFrom(JMXStructure Structure)
		{
			PrimAniGroup obj = new PrimAniGroup()
			{
				Name = (string)((JMXAttribute)Structure.Childs[0]).Value,
				Entries = ((JMXStructure)Structure.Childs[1]).GetChildList<PrimAniGroup.PrimAniTypeData>()
			};
			return obj;
		}
		#endregion

		#region Internal Classes
		public class Entry : JMXStructure
		{
			#region Constructor
			public Entry(string Name, PrimAniGroup.PrimAniTypeData Entry) : base(Name, true)
			{
				// Add new format
				m_SupportedFormats.Add(typeof(PrimAniGroup.PrimAniTypeData.Event), (s, e) => {
					e.Childs.Add(new Event("[" + e.Childs.Count + "]", e.Obj is PrimAniGroup.PrimAniTypeData.Event _obj ? _obj : new PrimAniGroup.PrimAniTypeData.Event()));
				});
				m_SupportedFormats.Add(typeof(Vector2), (s, e) => {
					e.Childs.Add(new Vector2VM("[" + e.Childs.Count + "]", e.Obj is Vector2 _obj ? _obj : new Vector2()));
				});
				// Create viewmodel node
				Childs.Add(new JMXOption("Type", Entry.Type, JMXOption.GetValues<object>(typeof(PrimAnimationType))));
				Childs.Add(new JMXAttribute("AnimationSetIndex", Entry.PrimAnimationIndex));
				AddChildArray("Events", Entry.Events.ToArray(), true, true);
				Childs.Add(new JMXAttribute("WalkLength", Entry.WalkLength));
				AddChildArray("WalkGraph", Entry.WalkGraph.ToArray(), true, true);
			}
			#endregion

			#region Public Methods
			public override object GetClassFrom(JMXStructure Structure)
			{
				PrimAniGroup.PrimAniTypeData obj = new PrimAniGroup.PrimAniTypeData()
				{
					Type = (PrimAnimationType)((JMXOption)Structure.Childs[0]).Value,
					PrimAnimationIndex = (int)((JMXAttribute)Structure.Childs[1]).Value,
					Events = ((JMXStructure)Structure.Childs[2]).GetChildList<PrimAniGroup.PrimAniTypeData.Event>(),
					WalkLength = (float)((JMXAttribute)Structure.Childs[3]).Value,
					WalkGraph = ((JMXStructure)Structure.Childs[4]).GetChildList<Vector2>()
				};
				return obj;
			}
			#endregion

			#region Internal Classes
			public class Event : JMXStructure
			{
				#region Constructor
				public Event(string Name, PrimAniGroup.PrimAniTypeData.Event Event) : base(Name, true)
				{
					Childs.Add(new JMXAttribute("Time", Event.Time));
					Childs.Add(new JMXAttribute("Type", Event.Type));
					Childs.Add(new JMXAttribute("UnkUInt01", Event.UnkUInt01));
					Childs.Add(new JMXAttribute("UnkUInt02", Event.UnkUInt02));
				}
				#endregion

				#region Public Methods
				public override object GetClassFrom(JMXStructure Structure)
				{
					PrimAniGroup.PrimAniTypeData.Event obj = new PrimAniGroup.PrimAniTypeData.Event()
					{
						Time = (uint)((JMXAttribute)Structure.Childs[0]).Value,
						Type = (uint)((JMXAttribute)Structure.Childs[1]).Value,
						UnkUInt01 = (uint)((JMXAttribute)Structure.Childs[2]).Value,
						UnkUInt02 = (uint)((JMXAttribute)Structure.Childs[3]).Value
					};
					return obj;
				}
				#endregion
			}
			#endregion
		}
		#endregion
	}
}
