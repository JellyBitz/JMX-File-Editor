using JMXFileEditor.Silkroad.Data.Common;
using JMXFileEditor.Silkroad.Data.JMXVRES.ModData;
using JMXFileEditor.Silkroad.IO;
using JMXFileEditor.Silkroad.Mathematics;

using System;
using System.Collections.Generic;
using System.IO;

namespace JMXFileEditor.Silkroad.Data.JMXVRES
{
    /// <summary>
    /// Joymax Resource File
    /// <para>https://github.com/DummkopfOfHachtenduden/SilkroadDoc/wiki/JMXVRES </para>
    /// </summary>
    public class JMXVRES_0109 : IJMXFile
    {
        #region Public Properties

        /// <summary>
        /// Original header used by Joymax
        /// </summary>
        public const string LatestSignature = "JMXVRES 0109";

        public uint FlagUInt01 { get; set; }
        public uint FlagUInt02 { get; set; }
        public uint FlagUInt03 { get; set; }
        public uint FlagUInt04 { get; set; }
        public uint FlagUInt05 { get; set; }
        public ObjectGeneralInfo ObjectInfo { get; set; }
        public byte[] UnkBuffer { get; set; } = new byte[40];
        public string CollisionMesh { get; set; } = string.Empty;
        public BoundingBoxF CollisionBox01 { get; set; }
        public BoundingBoxF CollisionBox02 { get; set; }
        public bool UseCollisionMatrix { get; set; }
        public Matrix4x4 CollisionMatrix { get; set; } = new Matrix4x4();
        public List<PrimMtrlSet> MaterialSet { get; set; }
        public List<PrimMesh> MeshSet { get; set; }
        public uint AnimationTypeVersion { get; set; }
        public uint AnimationTypeUserDefine { get; set; }
        public List<PrimAnimation> AnimationSet { get; set; }
        public bool HasSkeleton { get; set; }
        public string SkeletonPath { get; set; } = string.Empty;
        public string AttachmentBone { get; set; } = string.Empty;
        public List<PrimMeshGroup> MeshGroups { get; set; }
        public List<PrimAniGroup> AnimationGroups { get; set; }
        public List<ModDataSet> SystemModSets { get; set; } = new List<ModDataSet>();
        public List<ModDataSet> AniModSets { get; set; } = new List<ModDataSet>();
        public ResAttachable ResourceAttachable { get; set; } = new ResAttachable();
        public byte[] NonDecodedBytes { get; set; }

        #endregion Public Properties

        #region Interface Implementation

        public string Format
        { get { return LatestSignature; } }

        public string Extension { get; } = "bsr";

        public void Load(Stream stream)
        {
            // Read file structure
            using (var reader = new BSReader(stream))
            {
                // Signature
                var signature = reader.ReadString(12);
                if (signature != LatestSignature)
                {
                    // TODO: Migrate old version to current if possible.
                    throw new NotSupportedException($"Migration from '{signature}' not supported.");
                }

                reader.ReadInt32(); // MaterialOffset
                reader.ReadInt32(); // MeshOffset
                reader.ReadInt32(); // SkeletonOffset
                reader.ReadInt32(); // AnimationOffset
                reader.ReadInt32(); // PrimMeshGroupOffset
                reader.ReadInt32(); // PrimAniGroupOffset
                reader.ReadInt32(); // ModPaletteOffset
                reader.ReadInt32(); // CollisionOffset

                // Unknown flags
                FlagUInt01 = reader.ReadUInt32();
                FlagUInt02 = reader.ReadUInt32();
                FlagUInt03 = reader.ReadUInt32();
                FlagUInt04 = reader.ReadUInt32();
                FlagUInt05 = reader.ReadUInt32();

                // Object info
                this.ObjectInfo = reader.Deserialize<ObjectGeneralInfo>();

                // Reserved
                UnkBuffer = reader.ReadBytes(40);

                // FileOffset.Collision
                CollisionMesh = reader.ReadString();
                CollisionBox01 = reader.ReadBoundingBoxF();
                CollisionBox02 = reader.ReadBoundingBoxF();
                UseCollisionMatrix = reader.ReadUInt32() != 0;
                if (UseCollisionMatrix)
                {
                    CollisionMatrix = reader.ReadMatrix4x4();
                }

                // FileOffset.Material
                var count = reader.ReadInt32();
                MaterialSet = new List<PrimMtrlSet>();
                for (int i = 0; i < count; i++)
                {
                    // create
                    MaterialSet.Add(new PrimMtrlSet()
                    {
                        Index = reader.ReadUInt32(),
                        Path = reader.ReadString()
                    });
                }

                // FileOffset.Mesh
                count = reader.ReadInt32();
                MeshSet = new List<PrimMesh>();
                for (int i = 0; i < count; i++)
                {
                    // create
                    MeshSet.Add(new PrimMesh()
                    {
                        Path = reader.ReadString(),
                        UnkUInt01 = FlagUInt01 == 1 ? reader.ReadUInt32() : 0
                    });
                }

                // FileOffset.Animation
                AnimationTypeVersion = reader.ReadUInt32();
                AnimationTypeUserDefine = reader.ReadUInt32();
                count = reader.ReadInt32();
                AnimationSet = new List<PrimAnimation>(count);
                for (int i = 0; i < count; i++)
                    AnimationSet.Add(reader.Deserialize<PrimAnimation>());

                // FileOffset.Skeleton
                HasSkeleton = reader.ReadUInt32() != 0;
                if (HasSkeleton)
                {
                    SkeletonPath = reader.ReadString();
                    AttachmentBone = reader.ReadString();
                }

                // FileOffset.MeshGroup
                count = reader.ReadInt32();
                MeshGroups = new List<PrimMeshGroup>();
                for (int i = 0; i < count; i++)
                    this.MeshGroups.Add(reader.Deserialize<PrimMeshGroup>());

                // FileOffset.AnimationGroup
                count = reader.ReadInt32();
                AnimationGroups = new List<PrimAniGroup>();
                for (int i = 0; i < count; i++)
                {
                    // create
                    var animationGroup = new PrimAniGroup();
                    AnimationGroups.Add(animationGroup);
                    // read
                    animationGroup.Name = reader.ReadString();
                    var animationEntryCount = reader.ReadInt32();
                    animationGroup.Entries = new List<PrimAniGroup.PrimAniTypeData>(animationEntryCount);
                    for (int j = 0; j < animationEntryCount; j++)
                    {
                        // create
                        var entry = new PrimAniGroup.PrimAniTypeData();
                        animationGroup.Entries.Add(entry);
                        // read
                        entry.Type = (PrimAnimationType)reader.ReadInt32();
                        entry.PrimAnimationIndex = reader.ReadInt32();
                        var eventCount = reader.ReadInt32();
                        entry.Events = new List<PrimAniGroup.PrimAniTypeData.Event>(eventCount);
                        for (int k = 0; k < eventCount; k++)
                        {
                            // create
                            entry.Events.Add(new PrimAniGroup.PrimAniTypeData.Event()
                            {
                                Time = reader.ReadUInt32(),
                                Type = reader.ReadUInt32(),
                                UnkUInt01 = reader.ReadUInt32(),
                                UnkUInt02 = reader.ReadUInt32()
                            });
                        }
                        var WalkGraphPointCount = reader.ReadInt32();
                        entry.WalkLength = reader.ReadSingle();
                        entry.WalkGraph = new List<Vector2>(WalkGraphPointCount);
                        for (int k = 0; k < WalkGraphPointCount; k++)
                        {
                            // create
                            entry.WalkGraph.Add(reader.ReadVector2());
                        }
                    }
                }

                // FileOffset.ModPalette
                for (int x = 0; x < 2; x++)
                {
                    // Quick variable to resume code
                    var modSet = x == 0 ? SystemModSets : AniModSets;

                    count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        var set = reader.Deserialize<ModDataSet>();

                        modSet.Add(set);
                    }
                }
                if (ObjectInfo.Type == ObjectGeneralType.Character || ObjectInfo.Type == ObjectGeneralType.Item)
                {
                    ResourceAttachable.UnkUInt01 = reader.ReadUInt32();
                    ResourceAttachable.UnkUInt02 = reader.ReadUInt32();
                    ResourceAttachable.AttachMethod = reader.ReadUInt32();
                    count = reader.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        ResourceAttachable.Slots.Add(new ResAttachable.Slot()
                        {
                            Index = reader.ReadUInt32(),
                            MeshSetIndex = reader.ReadUInt32(),
                        });
                    }
                    if (ObjectInfo.Type == ObjectGeneralType.Character)
                    {
                        ResourceAttachable.nComboNum = reader.ReadUInt32();
                    }
                }
            }
        }

        public void Save(string Path)
        {
            // Override file structure
            using (var stream = new FileStream(Path, FileMode.Create, FileAccess.Write))
            using (var writer = new BSWriter(stream))
            {
                // Signature
                writer.Write(LatestSignature, 12);

                // Dummy offsets
                writer.Write(0); // MaterialOffset
                writer.Write(0); // MeshOffset
                writer.Write(0); // SkeletonOffset
                writer.Write(0); // AnimationOffset
                writer.Write(0); // MeshGroupOffset
                writer.Write(0); // AnimationGroupOffset
                writer.Write(0); // ModPaletteOffset
                writer.Write(0); // CollisionOffset

                // Unknown Flags
                writer.Write(FlagUInt01);
                writer.Write(FlagUInt02);
                writer.Write(FlagUInt03);
                writer.Write(FlagUInt04);
                writer.Write(FlagUInt05);

                // Object Info
                writer.Serialize(this.ObjectInfo);

                writer.Write(UnkBuffer);

                // FileOffset.Collision
                var collisionOffset = (int)stream.Position;
                writer.Write(CollisionMesh);
                writer.Write(this.CollisionBox01);
                writer.Write(this.CollisionBox02);
                writer.Write(UseCollisionMatrix ? 1u : 0u);
                if (UseCollisionMatrix)
                    writer.Write(this.CollisionMatrix);

                var materialOffset = (int)stream.Position;
                writer.Write(MaterialSet.Count);
                for (int i = 0; i < MaterialSet.Count; i++)
                {
                    writer.Write(MaterialSet[i].Index);
                    writer.Write(MaterialSet[i].Path);
                }

                // FileOffset.Mesh
                var meshOffset = (int)stream.Position;
                writer.Write(MeshSet.Count);
                for (int i = 0; i < MeshSet.Count; i++)
                {
                    writer.Write(MeshSet[i].Path);
                    if (FlagUInt01 != 0)
                        writer.Write(MeshSet[i].UnkUInt01);
                }

                // FileOffset.Animation
                var animationOffset = (int)stream.Position;
                writer.Write(AnimationTypeVersion);
                writer.Write(AnimationTypeUserDefine);
                writer.Write(AnimationSet.Count);
                for (int i = 0; i < AnimationSet.Count; i++)
                {
                    writer.Write(AnimationSet[i].Path);
                }

                // FileOffset.Skeleton
                var skeletonOffset = (int)stream.Position;
                writer.Write(HasSkeleton ? 1u : 0u);
                if (HasSkeleton)
                {
                    writer.Write(SkeletonPath);
                    writer.Write(AttachmentBone);
                }

                // FileOffset.MeshGroup
                var meshGroupOffset = (int)stream.Position;
                writer.Write(MeshGroups.Count);
                foreach (var group in MeshGroups)
                {
                    writer.Write(group.Name);
                    writer.Write(group.MeshIndices.Count);
                    foreach (var item in group.MeshIndices)
                        writer.Write(item);
                }

                // FileOffset.AnimationGroup
                var aniGroupOffset = (int)stream.Position;
                writer.Write(AnimationGroups.Count);
                for (int i = 0; i < AnimationGroups.Count; i++)
                {
                    writer.Write(AnimationGroups[i].Name);
                    writer.Write(AnimationGroups[i].Entries.Count);
                    for (int j = 0; j < AnimationGroups[i].Entries.Count; j++)
                    {
                        writer.Write((uint)AnimationGroups[i].Entries[j].Type);
                        writer.Write(AnimationGroups[i].Entries[j].PrimAnimationIndex);
                        writer.Write(AnimationGroups[i].Entries[j].Events.Count);
                        for (int k = 0; k < AnimationGroups[i].Entries[j].Events.Count; k++)
                        {
                            writer.Write(AnimationGroups[i].Entries[j].Events[k].Time);
                            writer.Write(AnimationGroups[i].Entries[j].Events[k].Type);
                            writer.Write(AnimationGroups[i].Entries[j].Events[k].UnkUInt01);
                            writer.Write(AnimationGroups[i].Entries[j].Events[k].UnkUInt02);
                        }
                        writer.Write(AnimationGroups[i].Entries[j].WalkGraph.Count);
                        writer.Write(AnimationGroups[i].Entries[j].WalkLength);
                        for (int k = 0; k < AnimationGroups[i].Entries[j].WalkGraph.Count; k++)
                        {
                            writer.Write(AnimationGroups[i].Entries[j].WalkGraph[k].X);
                            writer.Write(AnimationGroups[i].Entries[j].WalkGraph[k].Y);
                        }
                    }
                }

                var modPaletteOffset = (int)stream.Position;
                // FileOffset.ModPalette
                for (int x = 0; x < 2; x++)
                {
                    // Quick variable to resume code
                    var modSet = x == 0 ? SystemModSets : AniModSets;

                    writer.Write(modSet.Count);
                    foreach (var set in modSet)
                        writer.Serialize(set);
                }

                // Extra
                if (ObjectInfo.Type == ObjectGeneralType.Character || ObjectInfo.Type == ObjectGeneralType.Item)
                {
                    writer.Write(ResourceAttachable.UnkUInt01);
                    writer.Write(ResourceAttachable.UnkUInt02);
                    writer.Write(ResourceAttachable.AttachMethod);
                    writer.Write(ResourceAttachable.Slots.Count);
                    for (int i = 0; i < ResourceAttachable.Slots.Count; i++)
                    {
                        writer.Write(ResourceAttachable.Slots[i].Index);
                        writer.Write(ResourceAttachable.Slots[i].MeshSetIndex);
                    }
                    if (ObjectInfo.Type == ObjectGeneralType.Character)
                        writer.Write(ResourceAttachable.nComboNum);
                }
                
                // Overwrite offsets now that we know them
                writer.Seek(12, SeekOrigin.Begin);
                writer.Write(materialOffset);
                writer.Write(meshOffset);
                writer.Write(skeletonOffset);
                writer.Write(animationOffset);
                writer.Write(meshGroupOffset);
                writer.Write(aniGroupOffset);
                writer.Write(modPaletteOffset);
                writer.Write(collisionOffset);
            }
        }

        #endregion Interface Implementation
    }
}