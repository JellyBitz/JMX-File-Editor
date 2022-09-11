using JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Parameter
{
    public class ParameterFrameDiffuseVM : JMXStructure
    {
        #region Constructor
        public ParameterFrameDiffuseVM(string Name, ParameterFrameDiffuse data) : base(Name, true)
        {
            AddFormatHandler(typeof(Color32), (s, e) =>
            {
                e.Childs.Add(new Color32VM("[" + e.Childs.Count + "]", e.Obj is Color32 _obj ? _obj : new Color32()));
            });
            AddChildArray("List", data.Value.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new ParameterFrameDiffuse()
            {
                Value = ((JMXStructure)s.Childs[i++]).GetChildList<Color32>()
            };
        }
        #endregion
    }
}
