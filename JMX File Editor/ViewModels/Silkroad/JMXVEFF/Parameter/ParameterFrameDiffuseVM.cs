using JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Parameter
{
    public class ParameterFrameDiffuseVM : JMXStructure
    {
        #region Constructor
        public ParameterFrameDiffuseVM(string Name, ParameterFrameDiffuse data) : base(Name, true)
        {
            AddFormatHandler(typeof(Color32), (s, e) =>
            {
                e.Childs.Add(new ColorVM("[" + e.Childs.Count + "]", e.Obj is Color32 _obj ? ColorVM.GetColor(_obj) : new Color() { A = 255 }));
            });
            AddChildArray("List", data.Value.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            var colorList = ((JMXStructure)s.Childs[i++]).GetChildList<Color>();
            return new ParameterFrameDiffuse()
            {
                Value = new List<Color32>(colorList.Select(x => new Color32(x.R, x.G, x.B, x.A)))
            };
        }
        #endregion
    }
}
