using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Blends
{
    public class VectorBlendVM : JMXStructure
    {
        #region Constructor
        public VectorBlendVM(string Name, VectorBlend data) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Time", data.Time));
            Childs.Add(new Vector3VM("Value", data.Value));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new VectorBlend()
            {
                Time = (float)((JMXAttribute)s.Childs[i++]).Value,
                Value = (Vector3)((JMXStructure)s.Childs[i++]).GetClass()
            };
        }
        #endregion
    }
}
