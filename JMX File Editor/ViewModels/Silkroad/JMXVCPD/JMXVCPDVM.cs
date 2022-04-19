using JMXFileEditor.Silkroad.Data.JMXVCPD;

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

            // Signature
            file.Header = (string)((JMXAttribute)Structure.Childs[0]).Value;

            // Unknown flags
            file.Int01 = (uint)((JMXAttribute)Structure.Childs[3]).Value;
            file.Int02 = (uint)((JMXAttribute)Structure.Childs[4]).Value;
            file.Int03 = (uint)((JMXAttribute)Structure.Childs[5]).Value;
            file.Int04 = (uint)((JMXAttribute)Structure.Childs[6]).Value;
            file.Int05 = (uint)((JMXAttribute)Structure.Childs[7]).Value;

            // Object info
            // TODO: Create VM class?!
            // file.Type = (uint)((JMXAttribute)Structure.Childs[8]).Value;
            // file.Name = (string)((JMXAttribute)Structure.Childs[9]).Value;
            // file.Int06 = (uint)((JMXAttribute)Structure.Childs[10]).Value;
            // file.Int07 = (uint)((JMXAttribute)Structure.Childs[11]).Value;

            // FileOffset.Collision
            file.CollisionResourcePath = (string)((JMXAttribute)Structure.Childs[12]).Value;

            // FileOffset.Resource
            file.ResourceSet = ((JMXStructure)Structure.Childs[13]).GetChildList<string>();

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
            //Childs.Add(new JMXAttribute("ObjInfo.Type", JMXFile.Type));
            //Childs.Add(new JMXAttribute("ObjInfo.Name", JMXFile.Name));
            //Childs.Add(new JMXAttribute("ObjInfo.UnkUInt06", JMXFile.Int06));
            //Childs.Add(new JMXAttribute("ObjInfo.UnkUInt07", JMXFile.Int07));

            // FileOffset.Collision
            Childs.Add(new JMXAttribute("CollisionResourcePath", JMXFile.CollisionResourcePath));

            // FileOffset.Resource
            AddChildArray("ResourceSet", JMXFile.ResourceSet.ToArray(), true, true);
        }

        #endregion Private Helpers
    }
}