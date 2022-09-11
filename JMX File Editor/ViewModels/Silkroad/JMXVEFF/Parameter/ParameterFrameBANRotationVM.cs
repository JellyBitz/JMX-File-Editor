using JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Parameter
{
    public class ParameterFrameBANRotationVM : JMXStructure
    {
        #region Constructor
        public ParameterFrameBANRotationVM(string Name, ParameterFrameBANRotation data) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Left", data.Left));
            AddFormatHandler(typeof(Matrix4x4), (s, e) =>
            {
                e.Childs.Add(new Matrix4x4VM("[" + e.Childs.Count + "]", e.Obj is Matrix4x4 _obj ? _obj : new Matrix4x4()));
            });
            AddChildArray("Right", data.Right.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new ParameterFrameBANRotation(
                (float)((JMXAttribute)s.Childs[i++]).Value,
                ((JMXStructure)s.Childs[i++]).GetChildList<Matrix4x4>()
                );
        }
        #endregion
    }
}
