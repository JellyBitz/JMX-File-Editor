using JMXFileEditor.Silkroad.Data.JMXVEFF.Controller;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Controller
{
    public class EFCLinkModeVM : JMXStructure
    {
        #region Constructor
        public EFCLinkModeVM(string Name, EFCLinkMode data) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("UnkUInt01", data.Int0));
            Childs.Add(new JMXAttribute("UnkUInt02", data.Int1));
            Childs.Add(new JMXAttribute("UnkUInt03", data.Int2));
            Childs.Add(new JMXAttribute("UnkUInt04", data.Int3));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EFCLinkMode()
            {
                Int0 = (uint)((JMXAttribute)s.Childs[i++]).Value,
                Int1 = (uint)((JMXAttribute)s.Childs[i++]).Value,
                Int2 = (uint)((JMXAttribute)s.Childs[i++]).Value,
                Int3 = (uint)((JMXAttribute)s.Childs[i++]).Value,
            };
        }
        #endregion
    }
}