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
    /// <para>https://github.com/DummkopfOfHachtenduden/SilkroadDoc/wiki/JMXVRES</para>
    /// </summary>
    public class JMXVRES_0109 : IJMXFile
    {
        #region Public Properties
        /// <summary>
        /// Original header used by Joymax
        /// </summary>
        public const string LatestSignature = "JMXVRES 0109";
        public int Flag01 { get; set; }
        public int Flag02 { get; set; }
        public int Flag03 { get; set; }
        public int Flag04 { get; set; }
        public int Flag05 { get; set; }
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
        public List<ModDataSet> SystemModSets { get; set; }
        public List<ModDataSet> AniModSets { get; set; }
        public ResAttachable ResourceAttachable { get; set; }
        #endregion Public Properties

        #region Interface Implementation
        public string Format => LatestSignature;
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

                // File offsets (Material, Mesh, Animation, Skeleton, MeshGroup, AnimationGroup, ModPalette, Collision)
                reader.SkipRead(32);

                // Unknown Flags
                Flag01 = reader.ReadInt32();
                Flag02 = reader.ReadInt32();
                Flag03 = reader.ReadInt32();
                Flag04 = reader.ReadInt32();
                Flag05 = reader.ReadInt32();

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
                    CollisionMatrix = reader.ReadMatrix4x4();

                // FileOffset.Material
                var count = reader.ReadInt32();
                MaterialSet = new List<PrimMtrlSet>(count);
                for (int i = 0; i < count; i++)
                    MaterialSet.Add(reader.Deserialize<PrimMtrlSet>());

                // FileOffset.Mesh
                count = reader.ReadInt32();
                MeshSet = new List<PrimMesh>(count);
                for (int i = 0; i < count; i++)
                    MeshSet.Add(reader.DeserializeParameterized<PrimMesh>(this.Flag01));

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
                MeshGroups = new List<PrimMeshGroup>(count);
                for (int i = 0; i < count; i++)
                    this.MeshGroups.Add(reader.Deserialize<PrimMeshGroup>());

                // FileOffset.AnimationGroup
                count = reader.ReadInt32();
                AnimationGroups = new List<PrimAniGroup>(count);
                for (int i = 0; i < count; i++)
                    this.AnimationGroups.Add(reader.Deserialize<PrimAniGroup>());

                // FileOffset.ModPalette
                count = reader.ReadInt32();
                SystemModSets = new List<ModDataSet>();
                for (int i = 0; i < count; i++)
                    SystemModSets.Add(reader.Deserialize<ModDataSet>());
                count = reader.ReadInt32();
                AniModSets = new List<ModDataSet>();
                for (int i = 0; i < count; i++)
                    AniModSets.Add(reader.Deserialize<ModDataSet>());

                // ResAttachable
                ResourceAttachable = reader.DeserializeParameterized<ResAttachable>(ObjectInfo.Type);
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

                // Reserved offsets
                writer.Write(0); // MaterialOffset
                writer.Write(0); // MeshOffset
                writer.Write(0); // SkeletonOffset
                writer.Write(0); // AnimationOffset
                writer.Write(0); // MeshGroupOffset
                writer.Write(0); // AnimationGroupOffset
                writer.Write(0); // ModPaletteOffset
                writer.Write(0); // CollisionOffset

                // Unknown Flags
                writer.Write(Flag01);
                writer.Write(Flag02);
                writer.Write(Flag03);
                writer.Write(Flag04);
                writer.Write(Flag05);

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
                foreach (var item in MaterialSet)
                    writer.Serialize(item);

                // FileOffset.Mesh
                var meshOffset = (int)stream.Position;
                writer.Write(MeshSet.Count);
                foreach (var item in MeshSet)
                    writer.SerializeParameterized(item, this.Flag01);

                // FileOffset.Animation
                var animationOffset = (int)stream.Position;
                writer.Write(AnimationTypeVersion);
                writer.Write(AnimationTypeUserDefine);
                writer.Write(AnimationSet.Count);
                foreach (var item in AnimationSet)
                    writer.Serialize(item);

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
                    writer.Serialize(group);
                }

                // FileOffset.AnimationGroup
                var aniGroupOffset = (int)stream.Position;
                writer.Write(AnimationGroups.Count);
                foreach (var item in AnimationGroups)
                    writer.Serialize(item);

                // FileOffset.ModPalette
                var modPaletteOffset = (int)stream.Position;
                writer.Write(SystemModSets.Count);
                foreach (var set in SystemModSets)
                    writer.Serialize(set);
                writer.Write(AniModSets.Count);
                foreach (var set in AniModSets)
                    writer.Serialize(set);

                // ResAttachable
                writer.SerializeParameterized(ResourceAttachable, ObjectInfo.Type);

                // Overwrite offsets now that we know them all
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