using System.Linq;

using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Blends;
using JMXFileEditor.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Blends
{
    public class EEDiffuseBlendVM : JMXStructure
    {
        #region Constructor
        public EEDiffuseBlendVM(string Name, EEBlend<Color32, DiffuseBlend> data) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Begin", data.Begin));
            Childs.Add(new JMXAttribute("End", data.End));

            AddFormatHandler(typeof(DiffuseBlend), (s, e) => {
                e.Childs.Add(new DiffuseBlendVM("[" + e.Childs.Count + "]", e.Obj is DiffuseBlend _obj ? _obj : new DiffuseBlend()));
            });
            AddChildArray("Points", data.Cast<DiffuseBlend>().ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EEBlend<Color32, DiffuseBlend>(((JMXStructure)s.Childs[i+2]).GetChildList<DiffuseBlend>())
            {
                Begin = (float)((JMXAttribute)s.Childs[i++]).Value,
                End = (float)((JMXAttribute)s.Childs[i++]).Value
            };
        }
        #endregion
    }
}
