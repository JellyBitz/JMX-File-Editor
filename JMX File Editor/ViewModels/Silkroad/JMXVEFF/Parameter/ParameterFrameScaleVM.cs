using JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Parameter
{
    public class ParameterFrameScaleVM : JMXStructure
    {
        #region Constructor
        public ParameterFrameScaleVM(string Name, ParameterFrameScale data) : base(Name, true)
        {
            AddFormatHandler(typeof(Vector3), (s, e) =>
            {
                e.Childs.Add(new Vector3VM("[" + e.Childs.Count + "]", e.Obj is Vector3 _obj ? _obj : new Vector3()));
            });
            AddChildArray("List", data.Value.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new ParameterFrameScale()
            {
                Value = ((JMXStructure)s.Childs[i++]).GetChildList<Vector3>()
            };
        }
        #endregion
    }
}
