using JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Parameter
{
    /// <summary>
    /// ViewModel representing a Rotation Vector
    /// </summary>
    public class ParameterAxisVector4VM : JMXStructure
    {
        #region Constructor
        public ParameterAxisVector4VM(string Name, ParameterAxisVector4 data) : base(Name, true)
        {
            Childs.Add(new Vector4VM("Left", data.Left));
            Childs.Add(new Matrix4x4VM("Right", data.Right));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new ParameterAxisVector4(
                (Vector4)((Vector4VM)s.Childs[i++]).GetClass(),
                (Matrix4x4)((Matrix4x4VM)s.Childs[i++]).GetClass()
                );
        }
        #endregion
    }
}
