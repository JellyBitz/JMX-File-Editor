using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;
using System.Collections.Generic;
namespace JMXFileEditor.Silkroad.Data.JMXVDOF
{
    public class Block : ISerializableBS
    {
        #region Public Properties
        public string Path { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public uint UnkUInt01 { get; set; }
        public Vector3 Position { get; set; }
        public float Yaw { get; set; }
        public float IsEntrance { get; set; }
        public BoundingBoxF CollisionBox01 { get; set; }
        public uint UnkUInt02 { get; set; }
        public BlockFogParam FogParameters { get; set; }
        public byte UnkByte01 { get; set; }
        public Vector3 UnkVector01 { get; set; }
        public Vector3 UnkVector02 { get; set; }
        public uint UnkUInt03 { get; set; }
        public string UnkString { get; set; } = string.Empty;
        public uint RoomIndex { get; set; }
        public uint FloorIndex { get; set; }
        public List<uint> ConnectedBlockIndices { get; set; } = new List<uint>();
        public List<uint> VisibleBlockIndices { get; set; } = new List<uint>();
        public List<BlockObject> ObjectList { get; set; } = new List<BlockObject>();
        public uint dwColObjCount { get; set; }
        public List<BlockLight> LightList { get; set; } = new List<BlockLight>();
        #endregion

        #region Interface Implementation
        public void Deserialize(BSReader reader)
        {
            Path = reader.ReadString();
            Name = reader.ReadString();

            UnkUInt01 = reader.ReadUInt32();
            
            Position = reader.ReadVector3();
            Yaw = reader.ReadFloat();
            IsEntrance = reader.ReadFloat();
            CollisionBox01 = reader.ReadBoundingBoxF();

            UnkUInt02 = reader.ReadUInt32();

            FogParameters = reader.Deserialize<BlockFogParam>();

            UnkByte01 = reader.ReadByte();
            if(UnkByte01 == 2)
            {
                UnkVector01 = reader.ReadVector3();
                UnkVector02 = reader.ReadVector3();
                UnkUInt03 = reader.ReadUInt32();
            }

            UnkString = reader.ReadString();

            RoomIndex = reader.ReadUInt32();
            FloorIndex = reader.ReadUInt32();

            ConnectedBlockIndices = new List<uint>(reader.ReadUIntArray(reader.ReadInt32()));
            VisibleBlockIndices = new List<uint>(reader.ReadUIntArray(reader.ReadInt32()));

            var count = reader.ReadInt32();
            ObjectList = new List<BlockObject>(count);
            dwColObjCount = reader.ReadUInt32();
            for (int i = 0; i < count; i++)
                ObjectList.Add(reader.Deserialize<BlockObject>());

            count = reader.ReadInt32();
            LightList = new List<BlockLight>(count);
            for (int i = 0; i < count; i++)
                LightList.Add(reader.Deserialize<BlockLight>());
        }
        public void Serialize(BSWriter writer)
        {
            writer.Write(Path);
            writer.Write(Name);

            writer.Write(UnkUInt01);

            writer.Write(Position);
            writer.Write(Yaw);
            writer.Write(IsEntrance);
            writer.Write(CollisionBox01);

            writer.Write(UnkUInt02);

            writer.Serialize(FogParameters);

            writer.Write(UnkByte01);
            if (UnkByte01 == 2)
            {
                writer.Write(UnkVector01);
                writer.Write(UnkVector02);
                writer.Write(UnkUInt03);
            }

            writer.Write(UnkString);

            writer.Write(RoomIndex);
            writer.Write(FloorIndex);

            writer.Write(ConnectedBlockIndices.Count);
            writer.Write(ConnectedBlockIndices.ToArray());
            writer.Write(VisibleBlockIndices.Count);
            writer.Write(VisibleBlockIndices.ToArray());

            writer.Write(ObjectList.Count);
            writer.Write(dwColObjCount);
            for (int i = 0; i < ObjectList.Count; i++)
                writer.Serialize(ObjectList[i]);

            writer.Write(LightList.Count);
            for (int i = 0; i < LightList.Count; i++)
                writer.Serialize(LightList[i]);
        }
        #endregion
    }
}