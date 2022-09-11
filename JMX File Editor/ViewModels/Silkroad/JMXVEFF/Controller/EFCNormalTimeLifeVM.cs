using JMXFileEditor.Silkroad.Data.JMXVEFF.Controller;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF.Controller
{
    public class EFCNormalTimeLifeVM : JMXStructure
    {
        #region Constructor
        public EFCNormalTimeLifeVM(string Name, EFCNormalTimeLife data) : base(Name, true)
        {
            // empty
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i) => new EFCNormalTimeLife();
        #endregion
    }
}