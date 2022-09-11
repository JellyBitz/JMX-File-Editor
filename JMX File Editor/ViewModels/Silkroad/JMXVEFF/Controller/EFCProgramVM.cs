using JMXFileEditor.Silkroad.Data.JMXVEFF;
using JMXFileEditor.Silkroad.Data.JMXVEFF.Controller;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Controller
{
    public class EFCProgramVM : JMXStructure
    {
        #region Constructor
        public EFCProgramVM(string Name, EFCProgram data) : base(Name, true)
        {
            // Mimic all nodes
            var _data = new EEStaticProgramVM(string.Empty, data.StaticProgram);
            foreach (var c in _data.Childs)
                Childs.Add(c);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new EFCProgram()
            {
                StaticProgram = (EEStaticProgram)new EEStaticProgramVM(string.Empty, new EEStaticProgram()).GetClassFrom(s, i)
            };
        }
        #endregion
    }
}