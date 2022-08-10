using JMXFileEditor.Silkroad.Data.JMXVDOF;
using JMXFileEditor.Silkroad.Mathematics;
using JMXFileEditor.ViewModels.Silkroad.Mathematics;

namespace JMXFileEditor.ViewModels.Silkroad.JMXVDOF
{
    public class BlockVM : JMXStructure
    {
        #region Constructor
        public BlockVM(string Name, Block Block) : base(Name, true)
        {
            Childs.Add(new JMXAttribute("Path", Block.Path));
            Childs.Add(new JMXAttribute("Name", Block.Name));

            Childs.Add(new JMXAttribute("UnkUInt01", Block.UnkUInt01));

            Childs.Add(new Vector3VM("Position", Block.Position));
            Childs.Add(new JMXAttribute("Yaw", Block.Yaw));
            Childs.Add(new JMXAttribute("IsEntrance", Block.IsEntrance));
            Childs.Add(new BoundingBoxVM("CollisionBox01", Block.CollisionBox01));

            Childs.Add(new JMXAttribute("UnkUInt02", Block.UnkUInt02));

            Childs.Add(new BlockFogParamVM("FogParameters", Block.FogParameters));

            Childs.Add(new JMXAttribute("UnkByte01", Block.UnkByte01));
            Childs.Add(new Vector3VM("UnkVector01", Block.UnkVector01));
            Childs.Add(new Vector3VM("UnkVector02", Block.UnkVector02));
            Childs.Add(new JMXAttribute("UnkUInt03", Block.UnkUInt03));

            Childs.Add(new JMXAttribute("UnkString", Block.UnkString));

            Childs.Add(new JMXAttribute("RoomIndex", Block.RoomIndex));
            Childs.Add(new JMXAttribute("FloorIndex", Block.FloorIndex));

            AddChildArray("ConnectedBlockIndices", Block.ConnectedBlockIndices.ToArray(), true, true);
            AddChildArray("VisibleBlockIndices", Block.VisibleBlockIndices.ToArray(), true, true);

            Childs.Add(new JMXAttribute("dwColObjCount", Block.dwColObjCount, false));
            AddFormatHandler(typeof(BlockObject), (s, e) => {
                e.Childs.Add(new BlockObjectVM("[" + e.Childs.Count + "]", e.Obj is BlockObject _obj ? _obj : new BlockObject()));
            });
            AddChildArray("ObjectList", Block.ObjectList.ToArray(), true, true);

            AddFormatHandler(typeof(BlockLight), (s, e) => {
                e.Childs.Add(new BlockLightVM("[" + e.Childs.Count + "]", e.Obj is BlockLight _obj ? _obj : new BlockLight()));
            });
            AddChildArray("LightList", Block.LightList.ToArray(), true, true);
        }
        #endregion

        #region Public Methods
        public override object GetClassFrom(JMXStructure s, int i)
        {
            return new Block
            {
                Path = (string)((JMXAttribute)s.Childs[i++]).Value,
                Name = (string)((JMXAttribute)s.Childs[i++]).Value,

                UnkUInt01 = (uint)((JMXAttribute)s.Childs[i++]).Value,

                Position = (Vector3)((Vector3VM)s.Childs[i++]).GetClass(),
                Yaw = (float)((JMXAttribute)s.Childs[i++]).Value,
                IsEntrance = (float)((JMXAttribute)s.Childs[i++]).Value,
                CollisionBox01 = (BoundingBoxF)((BoundingBoxVM)s.Childs[i++]).GetClass(),

                UnkUInt02 = (uint)((JMXAttribute)s.Childs[i++]).Value,

                FogParameters = (BlockFogParam)((BlockFogParamVM)s.Childs[i++]).GetClass(),

                UnkByte01 = (byte)((JMXAttribute)s.Childs[i++]).Value,
                UnkVector01 = (Vector3)((Vector3VM)s.Childs[i++]).GetClass(),
                UnkVector02 = (Vector3)((Vector3VM)s.Childs[i++]).GetClass(),
                UnkUInt03 = (uint)((JMXAttribute)s.Childs[i++]).Value,

                UnkString = (string)((JMXAttribute)s.Childs[i++]).Value,

                RoomIndex = (uint)((JMXAttribute)s.Childs[i++]).Value,
                FloorIndex = (uint)((JMXAttribute)s.Childs[i++]).Value,

                ConnectedBlockIndices = ((JMXStructure)s.Childs[i++]).GetChildList<uint>(),
                VisibleBlockIndices = ((JMXStructure)s.Childs[i++]).GetChildList<uint>(),

                dwColObjCount = (uint)((JMXAttribute)s.Childs[i++]).Value,
                ObjectList = ((JMXStructure)s.Childs[i++]).GetChildList<BlockObject>(),

                LightList = ((JMXStructure)s.Childs[i++]).GetChildList<BlockLight>(),
            };
        }
        #endregion
    }
}
