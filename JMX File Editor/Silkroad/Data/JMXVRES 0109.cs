using JMXFileEditor.Utility;
using System;
using System.Collections.Generic;
using System.IO;
namespace JMXFileEditor.Silkroad.Data
{
    public class JMXVRES_0109 : IJMXFile
    {
        #region Public Properties
        /// <summary>
        /// Original header used by Joymax
        /// </summary>
        public static string FileHeader { get; } = "JMXVRES 0109";
        public string Header { get; set; }
        public uint PointerMaterial { get; private set; }
        public uint PointerMesh { get; private set; }
        public uint PointerSkeleton { get; private set; }
        public uint PointerAnimation { get; private set; }
        public uint PointerMeshGroup { get; private set; }
        public uint PointerAnimationGroup { get; private set; }
        public uint PointerSoundEffect { get; private set; }
        public uint PointerBoundingBox { get; private set; }
        public uint FlagUInt01 { get; set; }
        public uint FlagUInt02 { get; set; }
        public uint FlagUInt03 { get; set; }
        public uint FlagUInt04 { get; set; }
        public uint FlagUInt05 { get; set; }
        public ResourceType ResourceType { get; set; }
        public string Name { get; set; }
        public byte[] UnkByteArray01 { get; set; }
        public string RootMesh { get; set; }
        public float[] BoundingBox01 { get; set; }
        public float[] BoundingBox02 { get; set; }
        public byte[] ExtraBoundingData { get; set; }
        public List<Material> Materials { get; set; }
        public List<Mesh> Meshes { get; set; }
        public uint UnkUInt01 { get; set; }
        public uint UnkUInt02 { get; set; }
        public List<Animation> Animations { get; set; }
        public List<Skeleton> Skeletons { get; set; }
        public List<MeshGroup> MeshGroups { get; set; }
        public List<AnimationGroup> AnimationGroups { get; set; }
        public byte[] SoundEffectUndecodedBytes { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Calculate and update the values from file pointers
        /// </summary>
        public void UpdatePointers()
        {
            //PointerBoundingBox = (uint)(file.m_Header.Length + (8 * 4) + (5 * 4) + (4) + (file.m_Name.Length + 4) + file.m_UnkByteArray01.Length);
            //PointerMaterial = file.PointerBoundingBox + (uint)((file.m_RootMesh.Length + 4) + (file.m_BoundingBox01.Length * 4) + (file.m_BoundingBox02.Length * 4) + (file.m_ExtraBoundingData.Length + 4));
        }
        #endregion

        #region Interface Implementation
        public string Format { get { return FileHeader; } }
        public string Extension { get; } = "bsr";

        public void Load(FileStream FileStream)
        {
            // Read file structure
            using (var br = new BinaryReader(FileStream))
            {
                Header = new string(br.ReadChars(12));
                // Pointers
                PointerMaterial = br.ReadUInt32();
                PointerMesh = br.ReadUInt32();
                PointerSkeleton = br.ReadUInt32();
                PointerAnimation = br.ReadUInt32();
                PointerMeshGroup = br.ReadUInt32();
                PointerAnimationGroup = br.ReadUInt32();
                PointerSoundEffect = br.ReadUInt32();
                PointerBoundingBox = br.ReadUInt32();
                // Flags
                FlagUInt01 = br.ReadUInt32();
                FlagUInt02 = br.ReadUInt32();
                FlagUInt03 = br.ReadUInt32();
                FlagUInt04 = br.ReadUInt32();
                FlagUInt05 = br.ReadUInt32();
                // Details
                ResourceType = (ResourceType)br.ReadUInt32();
                Name = new string(br.ReadChars(br.ReadInt32()));
                UnkByteArray01 = br.ReadBytes(48);

                // Pointer.BoundingBox
                RootMesh = new string(br.ReadChars(br.ReadInt32()));
                BoundingBox01 = br.ReadSingleArray(6);
                BoundingBox02 = br.ReadSingleArray(6);
                var hasExtraBoundingData = br.ReadUInt32() != 0;
                if (hasExtraBoundingData)
                    ExtraBoundingData = br.ReadBytes(64);
                else
                    ExtraBoundingData = new byte[0];

                // Pointer.Material
                Materials = new List<Material>(br.ReadInt32());
                for (int i = 0; i < Materials.Capacity; i++)
                {
                    // create
                    var material = new Material();
                    Materials.Add(material);
                    // read
                    material.Index = br.ReadUInt32();
                    material.Path = new string(br.ReadChars(br.ReadInt32()));
                }

                // Pointer.Mesh
                Meshes = new List<Mesh>(br.ReadInt32());
                for (int i = 0; i < Meshes.Capacity; i++)
                {
                    // create
                    var mesh = new Mesh();
                    Meshes.Add(mesh);
                    // read
                    mesh.Path = new string(br.ReadChars(br.ReadInt32()));
                    if(FlagUInt01 == 1)
                    {
                        mesh.UnkUInt01 = br.ReadUInt32();
                    }
                }

                // Pointer.Animation
                UnkUInt01 = br.ReadUInt32();
                UnkUInt02 = br.ReadUInt32();
                Animations = new List<Animation>(br.ReadInt32());
                for (int i = 0; i < Animations.Capacity; i++)
                {
                    // create
                    var animation = new Animation();
                    Animations.Add(animation);
                    // read
                    animation.Path = new string(br.ReadChars(br.ReadInt32()));
                }
                
                // Pointer.Skeleton
                Skeletons = new List<Skeleton>(br.ReadInt32());
                for (int i = 0; i < Skeletons.Capacity; i++)
                {
                    // create
                    var skeleton = new Skeleton();
                    Skeletons.Add(skeleton);
                    // read
                    skeleton.Path = new string(br.ReadChars(br.ReadInt32()));
                    skeleton.ExtraData = br.ReadBytes(br.ReadInt32());
                }

                // Pointer.MeshGroup
                MeshGroups = new List<MeshGroup>(br.ReadInt32());
                for (int i = 0; i < MeshGroups.Capacity; i++)
                {
                    // create
                    var meshGroup = new MeshGroup();
                    MeshGroups.Add(meshGroup);
                    // read
                    meshGroup.Name = new string(br.ReadChars(br.ReadInt32()));
                    meshGroup.FileIndexes = br.ReadUInt32Array(br.ReadInt32());
                }
                
                // Pointer.AnimationGroup
                AnimationGroups = new List<AnimationGroup>(br.ReadInt32());
                for (int i = 0; i < AnimationGroups.Capacity; i++)
                {
                    // create
                    var animationGroup = new AnimationGroup();
                    AnimationGroups.Add(animationGroup);
                    // read
                    animationGroup.Name = new string(br.ReadChars(br.ReadInt32()));
                    var animationEntryCount = br.ReadUInt32();
                    animationGroup.Entries = new AnimationGroup.Entry[animationEntryCount];
                    for (int j = 0; j < animationEntryCount; j++)
                    {
                        // create
                        var entry = new AnimationGroup.Entry();
                        animationGroup.Entries[j] = entry;
                        // read
                        entry.Type = (ResourceAnimationType)br.ReadUInt32();
                        entry.FileIndex = br.ReadUInt32();
                        var eventCount = br.ReadUInt32();
                        entry.Events = new AnimationGroup.Entry.Event[eventCount];
                        for (int k = 0; k < eventCount; k++)
                        {
                            // create
                            var eevent = new AnimationGroup.Entry.Event();
                            entry.Events[k] = eevent;
                            // read
                            eevent.KeyTime = br.ReadUInt32();
                            eevent.Type = br.ReadUInt32();
                            eevent.UnkUInt01 = br.ReadUInt32();
                            eevent.UnkUInt02 = br.ReadUInt32();
                        }
                        var WalkGraphPointCount = br.ReadUInt32();
                        entry.WalkPoints = new System.Windows.Point[WalkGraphPointCount];
                        entry.WalkingLength = br.ReadSingle();
                        for (int k = 0; k < WalkGraphPointCount; k++)
                        {
                            entry.WalkPoints[k] = new System.Windows.Point(br.ReadSingle(), br.ReadSingle());
                        }
                    }
                }

                // Pointer.SoundEffect
                SoundEffectUndecodedBytes = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
            }
        }
        public void Save(string Path)
        {

            // Pointers are calculated on saving always for safety
            UpdatePointers();
        }
        #endregion

        #region Internal classes
        public class Material
        {
            public uint Index { get; set; }
            public string Path { get; set; } = string.Empty;
        }
        public class Mesh
        {
            public string Path { get; set; } = string.Empty;
            public uint UnkUInt01 { get; set; }
        }
        public class Animation
        {
            public string Path { get; set; } = string.Empty;
        }
        public class Skeleton
        {
            public string Path { get; set; } = string.Empty;
            public byte[] ExtraData { get; set; }
        }
        public class MeshGroup
        {
            public string Name { get; set; } = string.Empty;
            public uint[] FileIndexes { get; set; }
        }
        public class AnimationGroup
        {
            public string Name { get; set; } = string.Empty;
            public Entry[] Entries { get; set; }
            public class Entry
            {
                public ResourceAnimationType Type { get; set; }
                public uint FileIndex { get; set; }
                public Event[] Events { get; set; }
                public System.Windows.Point[] WalkPoints { get; set; }
                public float WalkingLength { get; set; }

                public class Event
                {
                    public uint KeyTime { get; set; }
                    public uint Type { get; set; }
                    public uint UnkUInt01 { get; set; }
                    public uint UnkUInt02 { get; set; }
                }
            }
        }
        #endregion
    }
}