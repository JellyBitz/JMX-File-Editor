using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using System.Linq;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Blends
{
    public class EEFloatBlendVM : JMXStructure
    {
        #region Constructor
        public EEFloatBlendVM(string Name, EEBlend<float,FloatBlend> data) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Begin", data.Begin));
            Childs.Add(new JMXAttribute("End", data.End));

            AddFormatHandler(typeof(FloatBlend), (s, e) => {
                e.Childs.Add(new FloatBlendVM("[" + e.Childs.Count + "]", e.Obj is FloatBlend _obj ? _obj : new FloatBlend()));
            });
            AddChildArray("Points", data.Cast<FloatBlend>().ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EEBlend<float, FloatBlend>(((JMXStructure)s.Childs[i+2]).GetChildList<FloatBlend>())
            {
                Begin = (float)((JMXAttribute)s.Childs[i++]).Value,
                End = (float)((JMXAttribute)s.Childs[i++]).Value
            };
        }
        #endregion
    }
}
