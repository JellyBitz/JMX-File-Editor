using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using JMXFileEditor.Silkroad.Mathematics;
using System.Linq;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Blends
{
    public class EEVectorBlendVM : JMXStructure
    {
        #region Constructor
        public EEVectorBlendVM(string Name, EEBlend<Vector3, VectorBlend> data) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Begin", data.Begin));
            Childs.Add(new JMXAttribute("End", data.End));

            AddFormatHandler(typeof(VectorBlend), (s, e) => {
                e.Childs.Add(new VectorBlendVM("[" + e.Childs.Count + "]", e.Obj is VectorBlend _obj ? _obj : new VectorBlend()));
            });
            AddChildArray("Points", data.Cast<VectorBlend>().ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EEBlend<Vector3, VectorBlend>(((JMXStructure)s.Childs[i + 2]).GetChildList<VectorBlend>())
            {
                Begin = (float)((JMXAttribute)s.Childs[i++]).Value,
                End = (float)((JMXAttribute)s.Childs[i++]).Value
            };
        }
        #endregion
    }
}
