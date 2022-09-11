using JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Parameter
{
    public class ParameterAngleVector1VM : JMXStructure
    {

        #region Constructor
        public ParameterAngleVector1VM(string Name, ParameterAngleVector1 data) : base(Name,true)
        {
            Childs.Add(new Vector3VM("Left", data.Left));
            Childs.Add(new Vector3VM("Right", data.Right));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new ParameterAngleVector1(
                (Vector3)((Vector3VM)s.Childs[i++]).GetClass(),
                (Vector3)((Vector3VM)s.Childs[i++]).GetClass()
                );
        }
        #endregion
    }
}
