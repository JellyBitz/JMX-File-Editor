using JMXFileEditor.Utility;
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
        public uint PointerSystemMods { get; private set; }
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
        public uint UnkUInt03 { get; private set; }
        public List<SystemMod.Data> SystemMods { get; set; }
        public byte[] SystemModsUndecodedBytes { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// Calculate and update the values from file pointers
        /// </summary>
        public void UpdatePointers()
        {
            PointerBoundingBox = (uint)(Header.Length + (8 * 4) + (5 * 4) + (4) + (4 + Name.Length) + UnkByteArray01.Length);
            PointerMaterial = PointerBoundingBox + (uint)((4 + RootMesh.Length) + (BoundingBox01.Length * 4) + (BoundingBox02.Length * 4) + (4 + ExtraBoundingData.Length));
            PointerMesh = PointerMaterial + 4;
            for (int i = 0; i < Materials.Count; i++)
            {
                PointerMesh += (uint)(4 + (4 + Materials[i].Path.Length));
            }
            PointerAnimation = PointerMesh + 4;
            for (int i = 0; i < Meshes.Count; i++)
            {
                PointerAnimation += (uint)((4 + Meshes[i].Path.Length) + (FlagUInt01 == 1 ? 4 : 0));
            }
            PointerSkeleton = PointerAnimation + 8 + 4;
            for (int i = 0; i < Animations.Count; i++)
            {
                PointerSkeleton += (uint)(4 + Animations[i].Path.Length);
            }
            PointerMeshGroup = PointerSkeleton + 4;
            for (int i = 0; i < Skeletons.Count; i++)
            {
                PointerMeshGroup += (uint)((4 + Skeletons[i].Path.Length) + (4 + Skeletons[i].ExtraData.Length));
            }
            PointerAnimationGroup = PointerMeshGroup + 4;
            for (int i = 0; i < MeshGroups.Count; i++)
            {
                PointerAnimationGroup += (uint)((4 + MeshGroups[i].Name.Length) + (4 + MeshGroups[i].FileIndexes.Length * 4));
            }
            PointerSystemMods = PointerAnimationGroup + 4;
            for (int i = 0; i < AnimationGroups.Count; i++)
            {
                PointerSystemMods += (uint)((4 + AnimationGroups[i].Name.Length) + 4);
                for (int j = 0; j < AnimationGroups[i].Entries.Count; j++)
                {
                    PointerSystemMods += (uint)(4 + 4 + (4 + AnimationGroups[i].Entries[j].Events.Count * 16) + 4 + (4 + AnimationGroups[i].Entries[j].WalkPoints.Count * 8));
                }
            }
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
                PointerSystemMods = br.ReadUInt32();
                PointerBoundingBox = br.ReadUInt32();
                // Flags
                FlagUInt01 = br.ReadUInt32();
                FlagUInt02 = br.ReadUInt32();
                FlagUInt03 = br.ReadUInt32();
                FlagUInt04 = br.ReadUInt32();
                FlagUInt05 = br.ReadUInt32();
                // Details
                ResourceType = (ResourceType)br.ReadUInt32();
                Name = br.ReadString32();
                UnkByteArray01 = br.ReadBytes(48);

                // Pointer.BoundingBox
                RootMesh = br.ReadString32();
                BoundingBox01 = br.ReadSingleArray(6);
                BoundingBox02 = br.ReadSingleArray(6);
                if (br.ReadUInt32() == 1)
                    ExtraBoundingData = br.ReadBytes(64);
                else
                    ExtraBoundingData = new byte[0];

                // Pointer.Material
                var count = br.ReadInt32();
                Materials = new List<Material>();
                for (int i = 0; i < count; i++)
                {
                    // create
                    Materials.Add(new Material() {
                        Index = br.ReadUInt32(),
                        Path = br.ReadString32()
                });
                }

                // Pointer.Mesh
                count = br.ReadInt32();
                Meshes = new List<Mesh>();
                for (int i = 0; i < count; i++)
                {
                    // create
                    Meshes.Add(new Mesh()
                    {
                        Path = br.ReadString32(),
                        UnkUInt01 = FlagUInt01 == 1 ? br.ReadUInt32() : 0
                    });
                }

                // Pointer.Animation
                UnkUInt01 = br.ReadUInt32();
                UnkUInt02 = br.ReadUInt32();
                count = br.ReadInt32();
                Animations = new List<Animation>();
                for (int i = 0; i < count; i++)
                {
                    // create
                    Animations.Add(new Animation()
                    {
                        Path = br.ReadString32(),
                    });
                }

                // Pointer.Skeleton
                count = br.ReadInt32();
                Skeletons = new List<Skeleton>();
                for (int i = 0; i < count; i++)
                {
                    // create
                    Skeletons.Add(new Skeleton()
                    {
                        Path = br.ReadString32(),
                        ExtraData = br.ReadBytes(br.ReadInt32())
                    });
                }

                // Pointer.MeshGroup
                count = br.ReadInt32();
                MeshGroups = new List<MeshGroup>();
                for (int i = 0; i < count; i++)
                {
                    // create
                    MeshGroups.Add(new MeshGroup() {
                        Name = br.ReadString32(),
                        FileIndexes = br.ReadUInt32Array(br.ReadInt32())
                    });
                }

                // Pointer.AnimationGroup
                count = br.ReadInt32();
                AnimationGroups = new List<AnimationGroup>();
                for (int i = 0; i < count; i++)
                {
                    // create
                    var animationGroup = new AnimationGroup();
                    AnimationGroups.Add(animationGroup);
                    // read
                    animationGroup.Name = br.ReadString32();
                    var animationEntryCount = br.ReadUInt32();
                    animationGroup.Entries = new List<AnimationGroup.Entry>();
                    for (int j = 0; j < animationEntryCount; j++)
                    {
                        // create
                        var entry = new AnimationGroup.Entry();
                        animationGroup.Entries.Add(entry);
                        // read
                        entry.Type = (ResourceAnimationType)br.ReadUInt32();
                        entry.FileIndex = br.ReadUInt32();
                        var eventCount = br.ReadUInt32();
                        entry.Events = new List<AnimationGroup.Entry.Event>();
                        for (int k = 0; k < eventCount; k++)
                        {
                            // create
                            entry.Events.Add(new AnimationGroup.Entry.Event() {
                                KeyTime = br.ReadUInt32(),
                                Type = br.ReadUInt32(),
                                UnkUInt01 = br.ReadUInt32(),
                                UnkUInt02 = br.ReadUInt32()
                            });
                        }
                        var WalkGraphPointCount = br.ReadUInt32();
                        entry.WalkPoints = new List<AnimationGroup.Entry.Point>();
                        entry.WalkingLength = br.ReadSingle();
                        for (int k = 0; k < WalkGraphPointCount; k++)
                        {
                            // create
                            entry.WalkPoints.Add(new AnimationGroup.Entry.Point()
                            {
                                X = br.ReadSingle(),
                                Y = br.ReadSingle()
                            });
                        }
                    }
                }

                // Pointer.SystemMod
                UnkUInt03 = br.ReadUInt32();
                var systemModsCount = br.ReadUInt32();
                SystemMods = new List<SystemMod.Data>();
                for (int i = 0; i < systemModsCount; i++)
                {
                    var type = (SystemModType)br.ReadUInt32();
                    switch (type)
                    {
                        case SystemModType.SoundEffect:
                            {
                                var soundEffect = new SystemMod.SoundEffect(type);
                                soundEffect.UnkUInt01 = br.ReadUInt32();
                                soundEffect.GroupName = br.ReadString32();
                                soundEffect.UnkUInt02 = br.ReadUInt32();
                                soundEffect.UnkUInt03 = br.ReadUInt32();
                                soundEffect.UnkUInt04 = br.ReadUInt32();
                                soundEffect.UnkUInt05 = br.ReadUInt32();
                                soundEffect.UnkUInt06 = br.ReadUInt32();
                                soundEffect.UnkUInt07 = br.ReadUInt32();
                                soundEffect.UnkUInt08 = br.ReadUInt32();
                                soundEffect.UnkUInt09 = br.ReadUInt32();
                                soundEffect.UnkUInt10 = br.ReadUInt32();
                                soundEffect.UnkUInt11 = br.ReadUInt32();
                                soundEffect.UnkUInt12 = br.ReadUInt32();
                                soundEffect.UnkUInt13 = br.ReadUInt32();
                                soundEffect.UnkUInt14 = br.ReadUInt32();
                                soundEffect.UnkUInt15 = br.ReadUInt32();
                                soundEffect.UnkUInt16 = br.ReadUInt32();
                                soundEffect.UnkUInt17 = br.ReadUInt32();
                                soundEffect.UnkUInt18 = br.ReadUInt32();
                                soundEffect.UnkUInt19 = br.ReadUInt32();
                                soundEffect.UnkUInt20 = br.ReadUInt32();
                                soundEffect.UnkUInt21 = br.ReadUInt32();
                                soundEffect.UnkUInt22 = br.ReadUInt32();
                                soundEffect.Name = br.ReadString32();
                                var sounds = br.ReadUInt32();
                                soundEffect.Sounds = new List<SystemMod.SoundEffect.Sound>();
                                for (int j = 0; j < sounds; j++)
                                {
                                    // create
                                    soundEffect.Sounds.Add(new SystemMod.SoundEffect.Sound()
                                    {
                                        UnkUInt01 = br.ReadUInt32(),
                                        Path = br.ReadString32(),
                                        Time = br.ReadUInt32(),
                                        Key = br.ReadString32()
                                    });
                                }
                                // Add it
                                SystemMods.Add(soundEffect);
                            }
                            break;
                        default:
                            // Restore cursor and stop reading
                            br.BaseStream.Seek(-4, SeekOrigin.Current);
                            break;
                    }
                }

                // End reading undecoded bytes
                SystemModsUndecodedBytes = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
            }
        }
        public void Save(string Path)
        {
            // Override file structure
            using (BinaryWriter bw = new BinaryWriter(new FileStream(Path, FileMode.Create, FileAccess.Write)))
            {
                bw.Write(Header.ToCharArray());
                // Pointers are calculated always before saving for safety
                UpdatePointers();
                bw.Write(PointerMaterial);
                bw.Write(PointerMesh);
                bw.Write(PointerSkeleton);
                bw.Write(PointerAnimation);
                bw.Write(PointerMeshGroup);
                bw.Write(PointerAnimationGroup);
                bw.Write(PointerSystemMods);
                bw.Write(PointerBoundingBox);
                // Flags
                bw.Write(FlagUInt01);
                bw.Write(FlagUInt02);
                bw.Write(FlagUInt03);
                bw.Write(FlagUInt04);
                bw.Write(FlagUInt05);
                // Details
                bw.Write((uint)ResourceType);
                bw.WriteString32(Name);
                bw.Write(UnkByteArray01);

                // Pointer.BoundingBox
                bw.WriteString32(RootMesh);
                bw.Write(BoundingBox01);
                bw.Write(BoundingBox02);
                bw.Write(ExtraBoundingData.Length > 0);
                if (ExtraBoundingData.Length > 0)
                    bw.Write(ExtraBoundingData);

                // Pointer.Material
                bw.Write(Materials.Count);
                for (int i = 0; i < Materials.Count; i++)
                {
                    bw.Write(Materials[i].Index);
                    bw.WriteString32(Materials[i].Path);
                }

                // Pointer.Mesh
                bw.Write(Meshes.Count);
                for (int i = 0; i < Meshes.Count; i++)
                {
                    bw.WriteString32(Meshes[i].Path);
                    bw.Write(Meshes[i].UnkUInt01);
                }

                // Pointer.Animation
                bw.Write(UnkUInt01);
                bw.Write(UnkUInt02);
                bw.Write(Animations.Count);
                for (int i = 0; i < Animations.Count; i++)
                {
                    bw.WriteString32(Animations[i].Path);
                }

                // Pointer.Skeleton
                bw.Write(Skeletons.Count);
                for (int i = 0; i < Skeletons.Count; i++)
                {
                    bw.WriteString32(Skeletons[i].Path);
                    bw.Write(Skeletons[i].ExtraData.Length);
                    bw.Write(Skeletons[i].ExtraData);
                }

                // Pointer.MeshGroup
                bw.Write(MeshGroups.Count);
                for (int i = 0; i < MeshGroups.Count; i++)
                {
                    bw.WriteString32(MeshGroups[i].Name);
                    bw.Write(MeshGroups[i].FileIndexes.Length);
                    bw.Write(MeshGroups[i].FileIndexes);
                }

                // Pointer.AnimationGroup
                bw.Write(AnimationGroups.Count);
                for (int i = 0; i < AnimationGroups.Count; i++)
                {
                    bw.WriteString32(AnimationGroups[i].Name);
                    bw.Write(AnimationGroups[i].Entries.Count);
                    for (int j = 0; j < AnimationGroups[i].Entries.Count; j++)
                    {
                        bw.Write((uint)AnimationGroups[i].Entries[j].Type);
                        bw.Write(AnimationGroups[i].Entries[j].FileIndex);
                        bw.Write(AnimationGroups[i].Entries[j].Events.Count);
                        for (int k = 0; k < AnimationGroups[i].Entries[j].Events.Count; k++)
                        {
                            bw.Write(AnimationGroups[i].Entries[j].Events[k].KeyTime);
                            bw.Write(AnimationGroups[i].Entries[j].Events[k].Type);
                            bw.Write(AnimationGroups[i].Entries[j].Events[k].UnkUInt01);
                            bw.Write(AnimationGroups[i].Entries[j].Events[k].UnkUInt02);
                        }
                        bw.Write(AnimationGroups[i].Entries[j].WalkPoints.Count);
                        bw.Write(AnimationGroups[i].Entries[j].WalkingLength);
                        for (int k = 0; k < AnimationGroups[i].Entries[j].WalkPoints.Count; k++)
                        {
                            bw.Write(AnimationGroups[i].Entries[j].WalkPoints[k].X);
                            bw.Write(AnimationGroups[i].Entries[j].WalkPoints[k].Y);
                        }
                    }
                }

                // Pointer.SystemMod
                bw.Write(UnkUInt03);
                bw.Write(SystemMods.Count);
                for (int i = 0; i < SystemMods.Count; i++)
                {
                    bw.Write((uint)SystemMods[i].Type);
                    if (SystemMods[i] is SystemMod.SoundEffect soundEffect)
                    {
                        bw.Write(soundEffect.UnkUInt01);
                        bw.WriteString32(soundEffect.GroupName);
                        bw.Write(soundEffect.UnkUInt02);
                        bw.Write(soundEffect.UnkUInt03);
                        bw.Write(soundEffect.UnkUInt04);
                        bw.Write(soundEffect.UnkUInt05);
                        bw.Write(soundEffect.UnkUInt06);
                        bw.Write(soundEffect.UnkUInt07);
                        bw.Write(soundEffect.UnkUInt08);
                        bw.Write(soundEffect.UnkUInt09);
                        bw.Write(soundEffect.UnkUInt10);
                        bw.Write(soundEffect.UnkUInt11);
                        bw.Write(soundEffect.UnkUInt12);
                        bw.Write(soundEffect.UnkUInt13);
                        bw.Write(soundEffect.UnkUInt14);
                        bw.Write(soundEffect.UnkUInt15);
                        bw.Write(soundEffect.UnkUInt16);
                        bw.Write(soundEffect.UnkUInt17);
                        bw.Write(soundEffect.UnkUInt18);
                        bw.Write(soundEffect.UnkUInt19);
                        bw.Write(soundEffect.UnkUInt20);
                        bw.Write(soundEffect.UnkUInt21);
                        bw.Write(soundEffect.UnkUInt22);
                        bw.WriteString32(soundEffect.Name);
                        bw.Write(soundEffect.Sounds.Count);
                        for (int j = 0; j < soundEffect.Sounds.Count; j++)
                        {
                            bw.Write(soundEffect.Sounds[j].UnkUInt01);
                            bw.WriteString32(soundEffect.Sounds[j].Path);
                            bw.Write(soundEffect.Sounds[j].Time);
                            bw.WriteString32(soundEffect.Sounds[j].Key);
                        }
                    }
                }
                
                // Left bytes
                bw.Write(SystemModsUndecodedBytes);
            }
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
            public List<Entry> Entries { get; set; }
            public class Entry
            {
                public ResourceAnimationType Type { get; set; }
                public uint FileIndex { get; set; }
                public List<Event> Events { get; set; }
                public List<Point> WalkPoints { get; set; }
                public float WalkingLength { get; set; }

                public class Event
                {
                    public uint KeyTime { get; set; }
                    public uint Type { get; set; }
                    public uint UnkUInt01 { get; set; }
                    public uint UnkUInt02 { get; set; }
                }
                public class Point
                {
                    public float X { get; set; }
                    public float Y { get; set; }
                }
            }
        }
        /// <summary>
        /// Wrapper class
        /// </summary>
        public class SystemMod
        {
            /// <summary>
            /// Data abstraction depends on reading type
            /// </summary>
            public abstract class Data
            {
                public SystemModType Type { get; set; }
                public Data(SystemModType Type)
                {
                    this.Type = Type;
                }
            }
            public class SoundEffect : Data
            {
                public uint UnkUInt01 { get; set; }
                public string GroupName { get; set; }
                public uint UnkUInt02 { get; set; }
                public uint UnkUInt03 { get; set; }
                public uint UnkUInt04 { get; set; }
                public uint UnkUInt05 { get; set; }
                public uint UnkUInt06 { get; set; }
                public uint UnkUInt07 { get; set; }
                public uint UnkUInt08 { get; set; }
                public uint UnkUInt09 { get; set; }
                public uint UnkUInt10 { get; set; }
                public uint UnkUInt11 { get; set; }
                public uint UnkUInt12 { get; set; }
                public uint UnkUInt13 { get; set; }
                public uint UnkUInt14 { get; set; }
                public uint UnkUInt15 { get; set; }
                public uint UnkUInt16 { get; set; }
                public uint UnkUInt17 { get; set; }
                public uint UnkUInt18 { get; set; }
                public uint UnkUInt19 { get; set; }
                public uint UnkUInt20 { get; set; }
                public uint UnkUInt21 { get; set; }
                public uint UnkUInt22 { get; set; }
                public string Name { get; set; }
                public List<Sound> Sounds { get; set; }
                public SoundEffect(SystemModType Type) : base(Type) { }
                public class Sound
                {
                    public uint UnkUInt01 { get; set; }
                    public string Path { get; set; }
                    public uint Time { get; set; }
                    public string Key { get; set; }
                }
            }
        }
        #endregion
    }
}