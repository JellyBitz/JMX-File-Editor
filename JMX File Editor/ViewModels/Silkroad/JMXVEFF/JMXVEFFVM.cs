using JMXFileEditor.Silkroad.Data.JMXVEFF;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVEFF
{
    /// <summary>
    /// ViewModel representing the <see cref="EFStoredEffect"/> file format
    /// </summary>
    public class JMXVEFFVM : JMXStructure
    {
        #region Constructor
        public JMXVEFFVM(EFStoredEffect JMXFile) : base(JMXFile.Format, true)
        {
            // Store base class as node
            Childs.Add(new EFStoredObjectVM("RootObject", JMXFile));
        }
        #endregion

        #region Public Properties
        public override object GetClassFrom(JMXStructure s, int i)
        {
            var b = (EFStoredObject)((EFStoredObjectVM)s.Childs[i++]).GetClass();
            // Copy base class node
            return new EFStoredEffect()
            {
                Name = b.Name,
                Controllers = b.Controllers,
            };
        }
        #endregion
    }
}