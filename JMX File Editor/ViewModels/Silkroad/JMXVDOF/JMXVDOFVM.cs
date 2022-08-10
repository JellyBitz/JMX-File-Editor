using JMXFileEditor.Silkroad.Data.Common;
using JMXFileEditor.Silkroad.Data.JMXVDOF;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Common;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVDOF
{
    /// <summary>
    /// ViewModel representing the JMXVDOF file format
    /// </summary>
    public class JMXVDOFVM : JMXStructure
    {
        #region Constructor
        public JMXVDOFVM(JMXVDOF_0101 JMXFile) : base(JMXFile.Format, true)
        {
            // Object info
            Childs.Add(new ObjectGeneralInfoVM("ObjectInfo", JMXFile.ObjectInfo));
            Childs.Add(new JMXAttribute("RegionID", JMXFile.RegionID));

            // BoundingBox
            Childs.Add(new BoundingBoxVM("CollisionBox01", JMXFile.CollisionBox01));
            Childs.Add(new BoundingBoxVM("CollisionBox02", JMXFile.CollisionBox02));

            // Blocks
            AddFormatHandler(typeof(Block), (s, e) => {
                e.Childs.Add(new BlockVM("[" + e.Childs.Count + "]", e.Obj is Block _obj ? _obj : new Block()));
            });
            AddChildArray("BlockList", JMXFile.BlockList.ToArray(), true, true);

            // Grid
            Childs.Add(new GridVM("Grid", JMXFile.Grid));

            // Links
            AddFormatHandler(typeof(Link), (s, e) => {
                e.Childs.Add(new LinkVM("[" + e.Childs.Count + "]", e.Obj is Link _obj ? _obj : new Link()));
            });
            AddChildArray("LinkList", JMXFile.LinkList.ToArray(), true, true);

            // Labels
            AddChildArray("RoomNames", JMXFile.RoomNames.ToArray(), true, true);
            AddChildArray("FloorNames", JMXFile.FloorNames.ToArray(), true, true);

            // Groups
            AddFormatHandler(typeof(Group), (s, e) => {
                e.Childs.Add(new GroupVM("[" + e.Childs.Count + "]", e.Obj is Group _obj ? _obj : new Group()));
            });
            AddChildArray("GroupList", JMXFile.GroupList.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            JMXVDOF_0101 file = new JMXVDOF_0101();

            // Object info
            file.ObjectInfo = (ObjectGeneralInfo)((ObjectGeneralInfoVM)s.Childs[i++]).GetClass();
            file.RegionID = (ushort)((JMXAttribute)s.Childs[i++]).Value;

            // BoundingBox
            file.CollisionBox01 = (BoundingBoxF)((BoundingBoxVM)s.Childs[i++]).GetClass();
            file.CollisionBox02 = (BoundingBoxF)((BoundingBoxVM)s.Childs[i++]).GetClass();

            // Blocks
            file.BlockList = ((JMXStructure)s.Childs[i++]).GetChildList<Block>();

            // Grid
            file.Grid = (Grid)((GridVM)s.Childs[i++]).GetClass();

            // Links
            file.LinkList = ((JMXStructure)s.Childs[i++]).GetChildList<Link>();

            // Labels
            file.RoomNames = ((JMXStructure)s.Childs[i++]).GetChildList<string>();
            file.FloorNames = ((JMXStructure)s.Childs[i++]).GetChildList<string>();

            // Groups
            file.GroupList = ((JMXStructure)s.Childs[i++]).GetChildList<Group>();
            
            return file;
        }
        #endregion
    }
}
