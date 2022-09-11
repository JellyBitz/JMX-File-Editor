using JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Parameter
{
    public class ParameterRotVectorVM : JMXStructure
    {
        #region Constructor
        public ParameterRotVectorVM(string Name, ParameterRotVector data) : base(Name, true)
        {
            Childs.Add(new Vector3VM("Left", data.Left));
            Childs.Add(new Matrix4x4VM("Right", data.Right));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new ParameterRotVector(
                (Vector3)((Vector3VM)s.Childs[i++]).GetClass(),
                (Matrix4x4)((Matrix4x4VM)s.Childs[i++]).GetClass()
                );
        }
        #endregion
    }
}
