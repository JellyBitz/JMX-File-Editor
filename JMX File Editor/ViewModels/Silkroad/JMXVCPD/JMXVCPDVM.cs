using JMXFileEditor.Silkroad.Data.Common;
using JMXFileEditor.Silkroad.Data.JMXVCPD;
using JMXFileEditor.ViewModels.Silkroad.Common;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVCPD
{
    /// <summary>
    /// ViewModel representing the JMXVRES file format
    /// </summary>
    public class JMXVCPDVM : JMXStructure
    {
        #region Constructor
        public JMXVCPDVM(JMXVCPD_0101 JMXFile) : base(JMXFile.Format, true)
        {
            CreateNodes(JMXFile);
        }
        #endregion Constructor

        #region Public Methods
        public override object GetClassFrom(JMXStructure Structure)
        {
            JMXVCPD_0101 file = new JMXVCPD_0101();

            // Unknown flags
            file.Int01 = (uint)((JMXAttribute)Structure.Childs[0]).Value;
            file.Int02 = (uint)((JMXAttribute)Structure.Childs[1]).Value;
            file.Int03 = (uint)((JMXAttribute)Structure.Childs[2]).Value;
            file.Int04 = (uint)((JMXAttribute)Structure.Childs[3]).Value;
            file.Int05 = (uint)((JMXAttribute)Structure.Childs[4]).Value;

            // Object info
            file.ObjectInfo = (ObjectGeneralInfo)((ObjectGeneralInfoVM)Structure.Childs[5]).GetClass();

            // FileOffset.Collision
            file.CollisionResourcePath = (string)((JMXAttribute)Structure.Childs[6]).Value;

            // FileOffset.Resource
            file.ResourceSet = ((JMXStructure)Structure.Childs[7]).GetChildList<string>();

            return file;
        }

        #endregion Public Methods

        #region Private Helpers
        /// <summary>
        /// Create UI format
        /// </summary>
        private void CreateNodes(JMXVCPD_0101 JMXFile)
        {
            // Unknown
            Childs.Add(new JMXAttribute("UnkUInt01", JMXFile.Int01));
            Childs.Add(new JMXAttribute("UnkUInt02", JMXFile.Int02));
            Childs.Add(new JMXAttribute("UnkUInt03", JMXFile.Int03));
            Childs.Add(new JMXAttribute("UnkUInt04", JMXFile.Int04));
            Childs.Add(new JMXAttribute("UnkUInt05", JMXFile.Int05));

            // Object info
            Childs.Add(new ObjectGeneralInfoVM("ObjectInfo", JMXFile.ObjectInfo));

            // FileOffset.Collision
            Childs.Add(new JMXAttribute("CollisionResourcePath", JMXFile.CollisionResourcePath));

            // FileOffset.Resource
            AddChildArray("ResourceSet", JMXFile.ResourceSet.ToArray(), true, true);
        }
        #endregion Private Helpers
    }
}