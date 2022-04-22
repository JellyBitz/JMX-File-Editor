using JMXFileEditor.Silkroad.Data.JMXVBMT;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVBMT
{
    /// <summary>
    /// ViewModel representing the JMXVRES file format
    /// </summary>
    public class JMXVBMTVM : JMXStructure
    {
        #region Constructor
        public JMXVBMTVM(JMXVBMT_0102 JMXFile) : base(JMXFile.Format, true)
        {
            AddChildFormats();
            CreateNodes(JMXFile);
        }
        #endregion Constructor

        #region Public Methods
        public override object GetClassFrom(JMXStructure Structure)
        {
            JMXVBMT_0102 file = new JMXVBMT_0102();

            // MaterialSet
            file.Materials = ((JMXStructure)Structure.Childs[1]).GetChildList<PrimMtrl>();

            return file;
        }
        #endregion Public Methods

        #region Private Helpers
        /// <summary>
        /// Add new node formats
        /// </summary>
        private void AddChildFormats()
        {
            AddFormatHandler(typeof(PrimMtrl), (s, e) =>
            {
                e.Childs.Add(new CPrimMtrlVM("[" + e.Childs.Count + "]", e.Obj is PrimMtrl _obj ? _obj : new PrimMtrl()));
            });
        }
        /// <summary>
        /// Create UI format
        /// </summary>
        private void CreateNodes(JMXVBMT_0102 JMXFile)
        {
            // MaterialSet
            AddChildArray("Materials", JMXFile.Materials.ToArray(), true, true);
        }
        #endregion Private Helpers
    }
}