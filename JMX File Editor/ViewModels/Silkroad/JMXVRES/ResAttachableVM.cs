using JMXFileEditor.Silkroad.Data.JMXVRES;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVRES
{
    public class ResAttachableVM : JMXStructure
    {
        #region Constructor
        public ResAttachableVM(string Name, ResAttachable Resource) : base(Name, true)
        {
            // add format
            AddFormatHandler(typeof(ResAttachable.Slot), (s, e) => {
                e.Childs.Add(new Slot("[" + e.Childs.Count + "]", e.Obj is ResAttachable.Slot _obj ? _obj : new ResAttachable.Slot()));
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
            ResAttachable obj = new ResAttachable()
            {
                UnkUInt01 = (uint)((JMXAttribute)Structure.Childs[0]).Value,
                UnkUInt02 = (uint)((JMXAttribute)Structure.Childs[1]).Value,
                AttachMethod = (uint)((JMXAttribute)Structure.Childs[2]).Value,
                Slots = ((JMXStructure)Structure.Childs[3]).GetChildList<ResAttachable.Slot>(),
                nComboNum = (uint)((JMXAttribute)Structure.Childs[4]).Value,
            };
            return obj;
        }
        #endregion

        #region Internal Classes
        public class Slot : JMXStructure
        {
            #region Constructor
            public Slot(string Name, ResAttachable.Slot Slot) : base(Name, true)
            {
                Childs.Add(new JMXOption("Type", Slot.Type, JMXOption.GetValues<object>(typeof(ResAttachable.SlotType))));
                Childs.Add(new JMXAttribute("MeshSetIndex", Slot.MeshSetIndex));
            }
            #endregion

            #region Public Methods
            public override object GetClassFrom(JMXStructure Structure)
            {
                ResAttachable.Slot obj = new ResAttachable.Slot()
                {
                    Type = (ResAttachable.SlotType)((JMXOption)Structure.Childs[0]).Value,
                    MeshSetIndex = (uint)((JMXAttribute)Structure.Childs[1]).Value,
                };
                return obj;
            }
            #endregion
        }
        #endregion
    }
}