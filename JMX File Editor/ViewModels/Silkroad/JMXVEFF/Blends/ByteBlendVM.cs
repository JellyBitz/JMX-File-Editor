using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Blends
{
    public class ByteBlendVM : JMXStructure
    {
        #region Constructor
        public ByteBlendVM(string Name, ByteBlend data) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Time", data.Time));
            Childs.Add(new JMXAttribute("Value", data.Value));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new ByteBlend()
            {
                Time = (float)((JMXAttribute)s.Childs[i++]).Value,
                Value = (byte)((JMXAttribute)s.Childs[i++]).Value
            };
        }
        #endregion
    }
}
