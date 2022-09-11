using JMXFileEditor.Silkroad.Data.JMXVEFF.Controller;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Controller
{
    public class EFCNormalTimeLoopLifeVM : JMXStructure
    {
        #region Constructor
        public EFCNormalTimeLoopLifeVM(string Name, EFCNormalTimeLoopLife data) : base(Name, true)
        {
            // empty
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i) => new EFCNormalTimeLoopLife();
        #endregion
    }
}