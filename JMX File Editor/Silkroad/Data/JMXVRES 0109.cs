using JMXFileEditor.Utility;
using System;
using System.IO;
namespace JMXFileEditor.Silkroad.Data
{
    public class JMXVRES_0109 : IJMXFile
    {
        #region Private Members
        public string m_Header;
        public uint m_PointerMaterial;
        public uint m_PointerMesh;
        public uint m_PointerSkeleton;
        public uint m_PointerAnimation;
        public uint m_PointerMeshGroup;
        public uint m_PointerAnimationGroup;
        public uint m_PointerSoundEffect;
        public uint m_PointerBoundingBox;
        public uint m_FlagUInt01;
        public uint m_FlagUInt02;
        public uint m_FlagUInt03;
        public uint m_FlagUInt04;
        public uint m_FlagUInt05;
        public ResourceType m_ResourceType;
        public string m_Name;
        public byte[] m_UnkByteArray01;
        public string m_RootMesh;
        public float[] m_BoundingBox01;
        public float[] m_BoundingBox02;
        public byte[] m_ExtraBoundingData;
        public Material[] m_Materials;
        public string[] m_MeshPath;
        public uint[] m_MeshUnkUInt01;
        public uint m_UnkUInt01;
        public uint m_UnkUInt02;
        public string[] m_AnimationPath;
        public string[] m_SkeletonPath;
        public byte[][] m_SkeletonExtraBytes;
        public MeshGroup[] m_MeshGroups;
        public AnimationGroup[] m_AnimationGroups;
        public byte[] m_NonDecodedBytes;
        #endregion

        #region Public Methods
        /// <summary>
        /// Original header used by Joymax
        /// </summary>
        public static string FileHeader { get; } = "JMXVRES 0109";
        #endregion

        #region Interface Implementation
        public string Format { get { return FileHeader; } }
        public string Extension { get; } = "bsr";

        public void Load(FileStream FileStream)
        {
            // Read file structure
            using (var br = new BinaryReader(FileStream))
            {
                m_Header = new string(br.ReadChars(12));
                // Pointers
                m_PointerMaterial = br.ReadUInt32();
                m_PointerMesh = br.ReadUInt32();
                m_PointerSkeleton = br.ReadUInt32();
                m_PointerAnimation = br.ReadUInt32();
                m_PointerMeshGroup = br.ReadUInt32();
                m_PointerAnimationGroup = br.ReadUInt32();
                m_PointerSoundEffect = br.ReadUInt32();
                m_PointerBoundingBox = br.ReadUInt32();
                // Flags
                m_FlagUInt01 = br.ReadUInt32();
                m_FlagUInt02 = br.ReadUInt32();
                m_FlagUInt03 = br.ReadUInt32();
                m_FlagUInt04 = br.ReadUInt32();
                m_FlagUInt05 = br.ReadUInt32();
                // Details
                m_ResourceType = (ResourceType)br.ReadUInt32();
                m_Name = new string(br.ReadChars(br.ReadInt32()));
                m_UnkByteArray01 = br.ReadBytes(48);

                // Pointer.BoundingBox
                m_RootMesh = new string(br.ReadChars(br.ReadInt32()));
                m_BoundingBox01 = br.ReadSingleArray(6);
                m_BoundingBox02 = br.ReadSingleArray(6);
                var hasExtraBoundingData = br.ReadUInt32() != 0;
                if (hasExtraBoundingData)
                    m_ExtraBoundingData = br.ReadBytes(64);
                else
                    m_ExtraBoundingData = new byte[0];

                // Pointer.Material
                var materialSetCount = br.ReadUInt32();
                m_Materials = new Material[materialSetCount];
                for (int i = 0; i < materialSetCount; i++)
                {
                    // create
                    var material = new Material();
                    m_Materials[i] = material;
                    // read
                    material.Index = br.ReadUInt32();
                    material.Path = new string(br.ReadChars(br.ReadInt32()));
                }

                // Pointer.Mesh
                var meshCount = br.ReadUInt32();
                m_MeshPath = new string[meshCount];
                for (int i = 0; i < meshCount; i++)
                {
                    m_MeshPath[i] = new string(br.ReadChars(br.ReadInt32()));
                    if(m_FlagUInt01 == 1)
                    {
                        m_MeshUnkUInt01[i] = br.ReadUInt32();
                    }
                }

                // Pointer.Animation
                m_UnkUInt01 = br.ReadUInt32();
                m_UnkUInt02 = br.ReadUInt32();
                var animationCount = br.ReadUInt32();
                m_AnimationPath = new string[animationCount];
                for (int i = 0; i < animationCount; i++)
                {
                    m_AnimationPath[i] = new string(br.ReadChars(br.ReadInt32()));
                }
                
                // Pointer.Skeleton
                var skeletonCount = br.ReadUInt32();
                m_SkeletonPath = new string[skeletonCount];
                m_SkeletonExtraBytes = new byte[skeletonCount][];
                for (int i = 0; i < skeletonCount; i++)
                {
                    m_SkeletonPath[i] = new string(br.ReadChars(br.ReadInt32()));
                    m_SkeletonExtraBytes[i] = br.ReadBytes(br.ReadInt32());
                }

                // Pointer.MeshGroup
                var meshGroupCount = br.ReadUInt32();
                m_MeshGroups = new MeshGroup[meshGroupCount];
                for (int i = 0; i < meshGroupCount; i++)
                {
                    // create
                    var meshGroup = new MeshGroup();
                    m_MeshGroups[i] = meshGroup;
                    // read

                    meshGroup.Name = new string(br.ReadChars(br.ReadInt32()));
                    meshGroup.FileIndexes = br.ReadUInt32Array(br.ReadInt32());
                }
                
                // Pointer.AnimationGroup
                var animationGroupCount = br.ReadUInt32();
                m_AnimationGroups = new AnimationGroup[animationGroupCount];
                for (int i = 0; i < animationGroupCount; i++)
                {
                    // create
                    var animationGroup = new AnimationGroup();
                    m_AnimationGroups[i] = animationGroup;
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
                //br.BaseStream.Seek(m_PointerSoundEffect, SeekOrigin.Begin);
                m_NonDecodedBytes = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
            }
        }

        public void Save(string Path)
        {
            
        }
        #endregion

        #region Internal classes
        public class Material
        {
            public uint Index { get; set; }
            public string Path { get; set; }
        }


        public class MeshGroup
        {
            public string Name { get; set; }
            public uint[] FileIndexes { get; set; }
        }
        public class AnimationGroup
        {
            public string Name { get; set; }
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