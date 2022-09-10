using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Parameter;
using JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Controller;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Parameter
{
    /// <summary>
    /// ViewModel representing a <see cref="ParameterEFStaticEmit"/>
    /// </summary>
    public class ParameterEFStaticEmitVM : JMXStructure
    {
        #region Constructor
        public ParameterEFStaticEmitVM(string Name, ParameterEFStaticEmit data) : base(Name, true)
        {
            // Mimic all nodes
            var _data = new EFStaticEmitVM(string.Empty, data.Value);
            foreach (var c in _data.Childs)
                Childs.Add(c);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s)
        {
            return new ParameterEFStaticEmit()
            {
                Value = (EFStaticEmit)new EFStaticEmitVM(string.Empty, default).GetClassFrom(this)
            };
        }
        #endregion
    }
}
