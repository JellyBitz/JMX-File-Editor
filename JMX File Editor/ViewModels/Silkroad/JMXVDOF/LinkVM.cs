using JMXFileEditor.Silkroad.Data.JMXVDOF;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVDOF
{
    public class LinkVM : JMXStructure
    {
        #region Constructor
        public LinkVM(string Name, Link Link) : base(Name, true)
        {
            AddChildArray("BlockIndices", Link.BlockIndices.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new Link()
            {
                BlockIndices = ((JMXStructure)s.Childs[i++]).GetChildList<uint>(),
            };
        }
        #endregion
    }
}