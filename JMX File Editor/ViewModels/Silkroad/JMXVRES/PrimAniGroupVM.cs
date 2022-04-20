using JMXFileEditor.Silkroad.Data.JMXVRES;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Common;
namespace JMXFileEditor.ViewModels.Silkroad.JMXVRES
{
    public class PrimAniGroupVM : JMXStructure
    {
        #region Constructor
        public PrimAniGroupVM(string Name, PrimAniGroup AniGroup) : base(Name, true)
        {
            // Add new format
            m_SupportedFormats.Add(typeof(PrimAniTypeData), (s, e) => {
                e.Childs.Add(new Entry("[" + e.Childs.Count + "]", e.Obj is PrimAniTypeData _obj ? _obj : new PrimAniTypeData()));
            });
            // Create viewmodel node
            Childs.Add(new JMXAttribute("Name", AniGroup.Name));
            AddChildArray("Entries", AniGroup.AniTypeDataList.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure Structure)
        {
            PrimAniGroup obj = new PrimAniGroup()
            {
                Name = (string)((JMXAttribute)Structure.Childs[0]).Value,
                AniTypeDataList = ((JMXStructure)Structure.Childs[1]).GetChildList<PrimAniTypeData>()
            };
            return obj;
        }
        #endregion

        #region Internal Classes
        public class Entry : JMXStructure
        {
            #region Constructor
            public Entry(string Name, PrimAniTypeData Entry) : base(Name, true)
            {
                // Add new format
                m_SupportedFormats.Add(typeof(PrimAnimationEvent), (s, e) => {
                    e.Childs.Add(new Event("[" + e.Childs.Count + "]", e.Obj is PrimAnimationEvent _obj ? _obj : new PrimAnimationEvent()));
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
                PrimAniTypeData obj = new PrimAniTypeData()
                {
                    Type = (PrimAnimationType)((JMXOption)Structure.Childs[0]).Value,
                    PrimAnimationIndex = (int)((JMXAttribute)Structure.Childs[1]).Value,
                    Events = ((JMXStructure)Structure.Childs[2]).GetChildList<PrimAnimationEvent>(),
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
                public Event(string Name, PrimAnimationEvent Event) : base(Name, true)
                {
                    Childs.Add(new JMXAttribute("Time", Event.Time));
                    Childs.Add(new JMXAttribute("Type", Event.Type));
                    Childs.Add(new JMXAttribute("UnkUInt01", Event.Param0));
                    Childs.Add(new JMXAttribute("UnkUInt02", Event.Param1));
                }
                #endregion

                #region Public Methods
                public override object GetClassFrom(JMXStructure Structure)
                {
                    PrimAnimationEvent obj = new PrimAnimationEvent()
                    {
                        Time = (int)((JMXAttribute)Structure.Childs[0]).Value,
                        Type = (int)((JMXAttribute)Structure.Childs[1]).Value,
                        Param0 = (int)((JMXAttribute)Structure.Childs[2]).Value,
                        Param1 = (int)((JMXAttribute)Structure.Childs[3]).Value
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
