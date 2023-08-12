using JMXFileEditor.Silkroad.Data.JMXVENVI;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVENVI
{
    public class EnvironmentFloatBlendVM : JMXStructure
    {
        public EnvironmentFloatBlendVM(string Name, EnvironmentFloatBlend data) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Value", data.Value));
            Childs.Add(new JMXAttribute("Time", data.Time));
        }
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EnvironmentFloatBlend()
            {
                Value = (float)((JMXAttribute)s.Childs[i++]).Value,
                Time = (float)((JMXAttribute)s.Childs[i++]).Value,
            };
        }
    }
}
