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
            Childs.Add(new GradientColorPickerVM(data.Name, 0, 1, data.Value, false));
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new ParameterFrameDiffuse()
            {
                Value = ((GradientColorPickerVM)s.Childs[i++]).GetColor32(),
            };
        }
        #endregion
    }
}
