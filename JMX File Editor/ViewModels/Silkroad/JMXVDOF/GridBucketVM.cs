using JMXFileEditor.Silkroad.Data.JMXVDOF;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVDOF
{
    public class GridBucketVM : JMXStructure
    {
        #region Constructor
        public GridBucketVM(string Name, GridBucket GridBucket) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("ID", GridBucket.ID));
            AddChildArray("BlockIndices", GridBucket.BlockIndices.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new GridBucket()
            {
                ID = (uint)((JMXAttribute)s.Childs[i++]).Value,
                BlockIndices = ((JMXStructure)s.Childs[i++]).GetChildList<uint>(),
            };
        }
        #endregion
    }
}