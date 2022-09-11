using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Blends
{
    public class FloatBlendVM : JMXStructure
    {
        #region Constructor
        public FloatBlendVM(string Name, FloatBlend data) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Time", data.Time));
            Childs.Add(new JMXAttribute("Value", data.Value));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new FloatBlend()
            {
                Time = (float)((JMXAttribute)s.Childs[i++]).Value,
                Value = (float)((JMXAttribute)s.Childs[i++]).Value
            };
        }
        #endregion
    }
}
