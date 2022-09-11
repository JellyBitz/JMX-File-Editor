using JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Parameter
{
    public class ParameterFrameTextureSlideVM : JMXStructure
    {
        #region Constructor
        public ParameterFrameTextureSlideVM(string Name, ParameterFrameTextureSlide data) : base(Name, true)
        {
            Childs.Add(new Vector3VM("Left", data.Left));
            AddFormatHandler(typeof(Vector4), (s, e) =>
            {
                e.Childs.Add(new Vector4VM("[" + e.Childs.Count + "]", e.Obj is Vector4 _obj ? _obj : new Vector4()));
            });
            AddChildArray("Right", data.Right.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new ParameterFrameTextureSlide(
                (Vector3)((Vector3VM)s.Childs[i++]).GetClass(),
                ((JMXStructure)s.Childs[i++]).GetChildList<Vector4>()
                );
        }
        #endregion
    }
}
