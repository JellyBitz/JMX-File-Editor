using JMXFileEditor.Silkroad.Data.Common;
using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JMXFileEditor.Silkroad.Data.JMXVDOF
{
    /// <summary>
    /// Joymax Dungeon File
    /// <para>https://github.com/DummkopfOfHachtenduden/SilkroadDoc/wiki/JMXVDOF</para>
    /// </summary>
    public class JMXVDOF_0101 : IJMXFile
    {
        #region Public Properties 
        /// <summary>
        /// Original header used by Joymax
        /// </summary>
        public const string LatestSignature = "JMXVDOF 0101";
        public ObjectGeneralInfo ObjectInfo { get; set; }
        public ushort RegionID { get; set; }
        public BoundingBoxF CollisionBox01 { get; set; }
        public BoundingBoxF CollisionBox02 { get; set; }
        public List<Block> BlockList { get; set; } = new List<Block>();
        public Grid Grid { get; set; }
        public List<Link> LinkList { get; set; } = new List<Link>();
        public List<string> RoomNames { get; set; } = new List<string>();
        public List<string> FloorNames { get; set; } = new List<string>();
        public List<Group> GroupList { get; set; } = new List<Group>();
        #endregion

        #region Interface Implementation
        public string Format => LatestSignature;
        public string Extension => "dof";
        public void Load(Stream stream)
        {
            // Read file structure (CP949)
            using (var reader = new BSReader(stream, Encoding.GetEncoding(949)))
            {
                var signature = reader.ReadString(12);
                if (signature != LatestSignature)
                {
                    // TODO: Migrate old version to current if possible.
                    throw new NotSupportedException($"Migration from '{signature}' not supported.");
                }

                // File offsets (Block, Link, Grid, Group, Label, Offset01, Offset02, BoundingBox)
                reader.SkipRead(16);
                var fileOffsetLabels = reader.ReadUInt32();
                reader.SkipRead(12);

                // ObjectInfo
                ObjectInfo = reader.Deserialize<ObjectGeneralInfo>();
                RegionID = reader.ReadUInt16();

                // FileOffset.BoundingBox
                CollisionBox01 = reader.ReadBoundingBoxF();
                CollisionBox02 = reader.ReadBoundingBoxF();

                // FileOffset.Block
                var count = reader.ReadInt32();
                BlockList = new List<Block>(count);
                for (int i = 0; i < count; i++)
                    BlockList.Add(reader.Deserialize<Block>());

                // FileOffset.Grid
                Grid = reader.Deserialize<Grid>();

                // FileOffset.Links
                count = reader.ReadInt32();
                LinkList = new List<Link>(count);
                for (int i = 0; i < count; i++)
                    LinkList.Add(reader.Deserialize<Link>());

                // FileOffsets.Labels
                if (fileOffsetLabels != 0)
                {
                    RoomNames = new List<string>(reader.ReadStringArray(reader.ReadInt32()));
                    FloorNames = new List<string>(reader.ReadStringArray(reader.ReadInt32()));
                }

                // FileOffset.Groups
                count = reader.ReadInt32();
                GroupList = new List<Group>(count);
                for (int i = 0; i < count; i++)
                    GroupList.Add(reader.Deserialize<Group>());
            }
        }
        public void Save(string path) 
        {
            // Override file structure
            using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var writer = new BSWriter(stream, Encoding.GetEncoding(949)))
            {
                // Signature
                writer.Write(LatestSignature.ToCharArray());

                // Reserved file offsets (Block, Link, Grid, Group, Label, Offset01, Offset02, BoundingBox)
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);
                writer.Write(0);

                // Object Info
                writer.Serialize(ObjectInfo);
                writer.Write(RegionID);

                // FileOffset.BoundingBox
                var offsetBoundingBox = (int)stream.Position;
                writer.Write(CollisionBox01);
                writer.Write(CollisionBox02);

                // FileOffset.Blocks
                var offsetBlocks = (int)stream.Position;
                writer.Write(BlockList.Count);
                for (int i = 0; i < BlockList.Count; i++)
                    writer.Serialize(BlockList[i]);

                // FileOffset.Grid
                var offsetGrid = (int)stream.Position;
                writer.Serialize(Grid);

                // FileOffset.Links
                var offsetLinks = (int)stream.Position;
                writer.Write(LinkList.Count);
                for (int i = 0; i < LinkList.Count; i++)
                    writer.Serialize(LinkList[i]);

                // FileOffsets.Labels
                var offsetLabels = 0;
                if (RoomNames.Count > 0 && FloorNames.Count > 0)
                {
                    offsetLabels = (int)stream.Position;
                    writer.Write(RoomNames.Count);
                    writer.Write(RoomNames.ToArray());
                    writer.Write(FloorNames.Count);
                    writer.Write(FloorNames.ToArray());
                }

                // FileOffset.Groups
                var offsetGroups = (int)stream.Position;
                writer.Write(GroupList.Count);
                for (int i = 0; i < GroupList.Count; i++)
                    writer.Serialize(GroupList[i]);

                // Overwrite offsets now that we know them all
                writer.Seek(12, SeekOrigin.Begin);
                writer.Write(offsetBlocks);
                writer.Write(offsetLinks);
                writer.Write(offsetGrid);
                writer.Write(offsetGroups);
                writer.Write(offsetLabels);
                writer.Write(0);
                writer.Write(0);
                writer.Write(offsetBoundingBox);
            }
        }
        #endregion
    }
}
