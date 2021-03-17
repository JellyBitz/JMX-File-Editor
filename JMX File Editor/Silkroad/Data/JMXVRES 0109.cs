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
        public bool HasExtraBoundingData { get; set; }
        public byte[] ExtraBoundingData { get; set; }
        public List<Material> Materials { get; set; }
        public List<Mesh> Meshes { get; set; }
        public uint UnkUInt01 { get; set; }
        public uint UnkUInt02 { get; set; }
        public List<Animation> Animations { get; set; }
        public List<Skeleton> Skeletons { get; set; }
        public List<MeshGroup> MeshGroups { get; set; }
        public List<AnimationGroup> AnimationGroups { get; set; }
        public List<List<SystemModSet.Mod>> SystemMods { get; set; }
        public byte[] SystemModsNonDecodedBytes { get; set; }
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
                PointerMeshGroup += (uint)((4 + Skeletons[i].Path.Length) + (4 + Skeletons[i].ExtraData.Count));
            }
            PointerAnimationGroup = PointerMeshGroup + 4;
            for (int i = 0; i < MeshGroups.Count; i++)
            {
                PointerAnimationGroup += (uint)((4 + MeshGroups[i].Name.Length) + (4 + MeshGroups[i].FileIndexes.Count * 4));
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
                HasExtraBoundingData = br.ReadUInt32() != 0;
                if (HasExtraBoundingData)
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
                        UnkUInt01 = FlagUInt01 != 0 ? br.ReadUInt32() : 0
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
                        ExtraData = new List<byte>(br.ReadBytes(br.ReadInt32()))
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
                        FileIndexes = new List<uint>(br.ReadUInt32Array(br.ReadInt32()))
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
                    var animationEntryCount = br.ReadInt32();
                    animationGroup.Entries = new List<AnimationGroup.Entry>(animationEntryCount);
                    for (int j = 0; j < animationEntryCount; j++)
                    {
                        // create
                        var entry = new AnimationGroup.Entry();
                        animationGroup.Entries.Add(entry);
                        // read
                        entry.Type = (ResourceAnimationType)br.ReadUInt32();
                        entry.FileIndex = br.ReadUInt32();
                        var eventCount = br.ReadInt32();
                        entry.Events = new List<AnimationGroup.Entry.Event>(eventCount);
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
                        var WalkGraphPointCount = br.ReadInt32();
                        entry.WalkingLength = br.ReadSingle();
                        entry.WalkPoints = new List<AnimationGroup.Entry.Point>(WalkGraphPointCount);
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
                SystemMods = new List<List<SystemModSet.Mod>>(2);
                long lastSystemModPosition = default;
                // Try to read it
                try
                {
                    // Both systems follow the same pattern but the first one is about enviroment stuffs only
                    for (int sysCount = 0; sysCount < 2; sysCount++)
                    {
                        lastSystemModPosition = br.BaseStream.Position;
                        // create
                        var mods = new List<SystemModSet.Mod>();
                        // read
                        var modCount = br.ReadUInt32();
                        for (int i = 0; i < modCount; i++)
                        {
                            // create
                            var mod = new SystemModSet.Mod();
                            mods.Add(mod);
                            // read
                            mod.UnkUInt01 = br.ReadUInt32(); // 1, 2
                            mod.UnkUInt02 = br.ReadUInt32(); // 1, 2, 4, 9, 13, 15, 16, 17, 19, 22, 4294967295
                            mod.GroupName = br.ReadString32(); // default, ambient
                            count = br.ReadInt32();
                            mod.ModsData = new List<SystemModSet.ModData>(count);
                            for (int r = 0; r < count; r++)
                            {
                                // create
                                var modData = new SystemModSet.ModData();
                                mod.ModsData.Add(modData);
                                // read
                                modData.UnkUShort01 = br.ReadUInt16(); // 0
                                modData.UnkUShort02 = br.ReadUInt16(); // 0, 4, 5, 6
                                modData.UnkFloat01 = br.ReadSingle(); // 0.5
                                modData.UnkUInt01 = br.ReadUInt32(); // 1
                                modData.IDataFlags = br.ReadUInt32(); // 0, 16, 48, 256 (avatar), 272, 768 (Weapon)
                                modData.UnkUInt02 = br.ReadUInt32(); // 4294967295
                                modData.UnkUInt03 = br.ReadUInt32(); // 0
                                modData.UnkUInt04 = br.ReadUInt32(); // 0, 1 (Weapon), 3 (avatar)
                                modData.UnkUInt05 = br.ReadUInt32(); // 0
                                switch (modData.IDataFlags)
                                {
                                    case 0:
                                        // Has no more data
                                        break;
                                    case 16:
                                        {
                                            // abstraction
                                            var data = modData as SystemModSet.IDataEnvMap;
                                            // read
                                            data.IsEnabled = br.ReadUInt32();
                                            if (data.IsEnabled == 0)
                                                break;
                                            data.UnkUInt01 = br.ReadUInt32(); // 192
                                            data.UnkUInt02 = br.ReadUInt32(); // 3
                                            data.UnkUInt03 = br.ReadUInt32(); // 0
                                            data.UnkFloat02 = br.ReadSingle(); // 10
                                            data.UnkFloat03 = br.ReadSingle(); // 100
                                            data.UnkUInt04 = br.ReadUInt32(); // 0
                                            data.UnkUInt05 = br.ReadUInt32(); // 0
                                            data.UnkUInt06 = br.ReadUInt32(); // 0
                                            data.UnkUInt07 = br.ReadUInt32(); // 0
                                            data.UnkUInt08 = br.ReadUInt32(); // 0
                                            data.UnkUInt09 = br.ReadUInt32(); // 0
                                            data.Name = br.ReadString32(); // default
                                            var eventCount = br.ReadInt32();
                                            data.Events = new List<SystemModSet.IDataEnvMapEvent>(eventCount);
                                            for (int j = 0; j < eventCount; j++)
                                            {
                                                // create
                                                var e = new SystemModSet.IDataEnvMapEvent();
                                                data.Events.Add(e);
                                                // read
                                                e.IsEnabled = br.ReadUInt32();
                                                if (e.IsEnabled == 0)
                                                    continue;
                                                e.Path = br.ReadString32();
                                                e.Time = br.ReadUInt32();
                                                e.Keyword = br.ReadString32();
                                            }
                                        }
                                        break;
                                    case 48:
                                        {
                                            // abstraction
                                            var data = modData as SystemModSet.IDataParticle;
                                            // read
                                            data.IsEnabled = br.ReadUInt32();
                                            if (data.IsEnabled == 0)
                                                break;
                                            data.UnkUInt01 = br.ReadUInt32(); // 1
                                            data.Path = br.ReadString32(); // *.efp
                                            data.UnkUInt02 = br.ReadUInt32(); // 0
                                            data.UnkUInt03 = br.ReadUInt32(); // 0
                                            data.UnkUInt04 = br.ReadUInt32(); // 0
                                            data.UnkUInt05 = br.ReadUInt32(); // 0 
                                            data.UnkUInt06 = br.ReadUInt32(); // 0
                                            data.UnkUInt07 = br.ReadUInt32(); // 0
                                        }
                                        break;
                                    case 256:
                                        {
                                            // abstraction
                                            var data = modData as SystemModSet.IData256;
                                            // read
                                            data.IsEnabled = br.ReadUInt32();
                                            if (data.IsEnabled == 0)
                                                break;
                                            data.UnkUShort01 = br.ReadUInt16(); // 0
                                            data.UnkUShort02 = br.ReadUInt16(); // 1
                                            data.UnkUInt01 = br.ReadUInt32(); // 1
                                            data.UnkUInt02 = br.ReadUInt32(); // 7
                                        }
                                        break;
                                    case 272:
                                        {
                                            // abstraction
                                            var data = modData as SystemModSet.IData272;
                                            // read
                                            data.UnkUInt01 = br.ReadUInt32(); // 1000
                                            data.UnkUInt02 = br.ReadUInt32(); // 2
                                            data.UnkUInt03 = br.ReadUInt32(); // 0
                                            data.UnkUInt04 = br.ReadUInt32(); // 2
                                            data.UnkUInt05 = br.ReadUInt32(); // 0
                                            data.UnkFloat01 = br.ReadSingle(); // 0.58
                                            data.UnkFloat02 = br.ReadSingle(); // 0.58
                                            data.UnkFloat03 = br.ReadSingle(); // 0.58
                                            data.UnkFloat04 = br.ReadSingle(); // 1
                                            data.UnkUInt06 = br.ReadUInt32(); // 1000
                                            data.UnkFloat05 = br.ReadSingle(); // 0.58
                                            data.UnkFloat06 = br.ReadSingle(); // 0.58
                                            data.UnkFloat07 = br.ReadSingle(); // 0.58
                                            data.UnkFloat08 = br.ReadSingle(); // 1
                                            data.UnkUInt07 = br.ReadUInt32(); // 0
                                            data.UnkUInt08 = br.ReadUInt32(); // 0
                                            data.UnkUInt09 = br.ReadUInt32(); // 0
                                            data.UnkUInt10 = br.ReadUInt32(); // 0
                                            data.UnkUShort01 = br.ReadUInt16(); // 1541
                                            data.UnkUShort02 = br.ReadUInt16(); // 5
                                            data.UnkUShort03 = br.ReadUInt16(); // 514
                                            data.UnkUShort04 = br.ReadUInt16(); // 514
                                            data.UnkUShort05 = br.ReadUInt16(); // 1920
                                            data.UnkUShort06 = br.ReadUInt16(); // 51300
                                            data.UnkFloat09 = br.ReadSingle(); // 1
                                            data.UnkUInt11 = br.ReadUInt32(); // 0
                                        }
                                        break;
                                    case 768:
                                        {
                                            // abstraction
                                            var data = modData as SystemModSet.IData768;
                                            // read
                                            data.UnkUShort01 = br.ReadUInt16(); // 1
                                            data.UnkUInt01 = br.ReadUInt32(); // 0
                                            data.UnkUInt02 = br.ReadUInt32(); // 3
                                            data.UnkUInt03 = br.ReadUInt32(); // 0
                                            data.UnkUShort02 = br.ReadUInt16(); // 31744
                                            data.UnkUInt04 = br.ReadUInt32(); // 0
                                            data.UnkUInt05 = br.ReadUInt32(); // 1
                                            data.UnkUInt06 = br.ReadUInt32(); // 7
                                            data.UnkUInt07 = br.ReadUInt32(); // 2
                                            data.UnkUInt08 = br.ReadUInt32(); // 1
                                            data.UnkUInt09 = br.ReadUInt32(); // 9
                                        }
                                        break;
                                    default:
                                        // Unknown flags
                                        System.Diagnostics.Debugger.Break();
                                        throw new System.NotImplementedException();
                                }
                            }
                        }
                        // add
                        SystemMods.Add(mods);
                    }
                }
                catch
                {
                    // Section not correctly parsed, reset and show it as non decoded
                    br.BaseStream.Seek(lastSystemModPosition, SeekOrigin.Begin);

                    // TO DO: Parse it
                    System.Diagnostics.Debugger.Break();
                }


                // Stuffs about hidden mesh if some equipment is putting on
                //if (this.ResourceType == ResourceType.Character || this.ResourceType == ResourceType.NPC)
                //{
                //    uint unkUInt01 = br.ReadUInt32(); // 0, 4294967295
                //    if (this.ResourceType == ResourceType.Character || this.ResourceType == ResourceType.NPC && unkUInt01 == uint.MaxValue)
                //    {
                //        ushort unkFlag01 = br.ReadUInt16(); // 13
                //        if (unkFlag01 != 0)
                //        {
                //            var unkUShort01 = br.ReadUInt16(); // 0
                //            uint unkUInt03 = br.ReadUInt32(); // 0
                //            count = br.ReadInt32();
                //            for (int i = 0; i < count; i++)
                //            {
                //                uint index = br.ReadUInt32();
                //                uint value = br.ReadUInt32();
                //            }
                //            uint unkUInt23 = br.ReadUInt32(); // 0
                //        }
                //    }
                //}

                // End reading non decoded bytes
                long nonDecodedBytesCount = br.BaseStream.Length - br.BaseStream.Position;
                SystemModsNonDecodedBytes = br.ReadBytes((int)nonDecodedBytesCount);

                // print for checking it later
                System.Diagnostics.Debug.WriteLineIf(nonDecodedBytesCount > 0, SystemModsNonDecodedBytes.ToHexDump());
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
                bw.Write(HasExtraBoundingData ? 1 : 0);
                if(HasExtraBoundingData && ExtraBoundingData.Length == 0)
                	ExtraBoundingData = new byte[64];
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
                    if(FlagUInt01 != 0)
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
                    bw.Write(Skeletons[i].ExtraData.Count);
                    bw.Write(Skeletons[i].ExtraData.ToArray());
                }

                // Pointer.MeshGroup
                bw.Write(MeshGroups.Count);
                for (int i = 0; i < MeshGroups.Count; i++)
                {
                    bw.WriteString32(MeshGroups[i].Name);
                    bw.Write(MeshGroups[i].FileIndexes.Count);
                    bw.Write(MeshGroups[i].FileIndexes.ToArray());
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

                // Pointer.SystemModSet
                foreach (var systemMod in SystemMods)
                {
					bw.Write(systemMod.Count);
					foreach (var mod in systemMod)
					{
						bw.Write(mod.UnkUInt01);
                        bw.Write(mod.UnkUInt02);
						bw.WriteString32(mod.GroupName);
						bw.Write(mod.ModsData.Count);
						foreach (var modData in mod.ModsData)
                        {
                            bw.Write(modData.UnkUShort01);
                            bw.Write(modData.UnkUShort02);
                            bw.Write(modData.UnkFloat01);
                            bw.Write(modData.UnkUInt01);
                            bw.Write(modData.IDataFlags);
                            bw.Write(modData.UnkUInt02);
                            bw.Write(modData.UnkUInt03);
                            bw.Write(modData.UnkUInt04);
                            bw.Write(modData.UnkUInt05);
                            switch (modData.IDataFlags)
                            {
                                case 16:
                                    {
                                        var data = modData as JMXVRES_0109.SystemModSet.IDataEnvMap;
                                        bw.Write(data.IsEnabled);
                                        if (data.IsEnabled == 0)
                                            break;
                                        bw.Write(data.UnkUInt01);
                                        bw.Write(data.UnkUInt02);
                                        bw.Write(data.UnkUInt03);
                                        bw.Write(data.UnkFloat02);
                                        bw.Write(data.UnkFloat03);
                                        bw.Write(data.UnkUInt04);
                                        bw.Write(data.UnkUInt05);
                                        bw.Write(data.UnkUInt06);
                                        bw.Write(data.UnkUInt07);
                                        bw.Write(data.UnkUInt08);
                                        bw.Write(data.UnkUInt09);
                                        bw.WriteString32(data.Name);
                                        bw.Write(data.Events.Count);
										foreach(var e in data.Events)
										{
                                            bw.Write(e.IsEnabled);
                                            if(e.IsEnabled == 0)
                                                continue;
                                            bw.WriteString32(e.Path);
                                            bw.Write(e.Time);
                                            bw.WriteString32(e.Keyword);
										}
                                    }
                                    break;
                                case 48:
                                    {
                                        var data = modData as JMXVRES_0109.SystemModSet.IDataParticle;
                                        bw.Write(data.IsEnabled);
                                        if (data.IsEnabled == 0)
                                            break;
                                        bw.Write(data.UnkUInt01);
                                        bw.WriteString32(data.Path);
                                        bw.Write(data.UnkUInt02);
                                        bw.Write(data.UnkUInt03);
                                        bw.Write(data.UnkUInt04);
                                        bw.Write(data.UnkUInt05);
                                        bw.Write(data.UnkUInt06);
                                        bw.Write(data.UnkUInt07);
                                    }
                                    break;
                                case 256:
                                    {
                                        var data = modData as JMXVRES_0109.SystemModSet.IData256;
                                        bw.Write(data.IsEnabled);
                                        if (data.IsEnabled == 0)
                                            break;
                                        bw.Write(data.UnkUShort01);
                                        bw.Write(data.UnkUShort02);
                                        bw.Write(data.UnkUInt01);
                                        bw.Write(data.UnkUInt02);
                                    }
                                    break;
                                case 272:
                                    {
                                        var data = modData as JMXVRES_0109.SystemModSet.IData272;
                                        bw.Write(data.UnkUInt01);
                                        bw.Write(data.UnkUInt02);
                                        bw.Write(data.UnkUInt03);
                                        bw.Write(data.UnkUInt04);
                                        bw.Write(data.UnkUInt05);
                                        bw.Write(data.UnkFloat01);
                                        bw.Write(data.UnkFloat02);
                                        bw.Write(data.UnkFloat03);
                                        bw.Write(data.UnkFloat04);
                                        bw.Write(data.UnkUInt06);
                                        bw.Write(data.UnkFloat05);
                                        bw.Write(data.UnkFloat06);
                                        bw.Write(data.UnkFloat07);
                                        bw.Write(data.UnkFloat08);
                                        bw.Write(data.UnkUInt07);
                                        bw.Write(data.UnkUInt08);
                                        bw.Write(data.UnkUInt09);
                                        bw.Write(data.UnkUInt10);
                                        bw.Write(data.UnkUShort01);
                                        bw.Write(data.UnkUShort02);
                                        bw.Write(data.UnkUShort03);
                                        bw.Write(data.UnkUShort04);
                                        bw.Write(data.UnkUShort05);
                                        bw.Write(data.UnkUShort06);
                                        bw.Write(data.UnkFloat09);
                                        bw.Write(data.UnkUInt11);
                                    }
                                    break;
                                case 768:
                                    {
                                        var data = modData as JMXVRES_0109.SystemModSet.IData768;
                                        bw.Write(data.UnkUShort01);
                                        bw.Write(data.UnkUInt01);
                                        bw.Write(data.UnkUInt02);
                                        bw.Write(data.UnkUInt03);
                                        bw.Write(data.UnkUShort02);
                                        bw.Write(data.UnkUInt04);
                                        bw.Write(data.UnkUInt05);
                                        bw.Write(data.UnkUInt06);
                                        bw.Write(data.UnkUInt07);
                                        bw.Write(data.UnkUInt08);
                                        bw.Write(data.UnkUInt09);
                                    }
                                    break;
                            }
                        }
                    }
                }

                // Write remaining bytes
                bw.Write(SystemModsNonDecodedBytes);
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
            public List<byte> ExtraData { get; set; } = new List<byte>();
        }
        public class MeshGroup
        {
            public string Name { get; set; } = string.Empty;
            public List<uint> FileIndexes { get; set;  } = new List<uint>();
        }
        public class AnimationGroup
        {
            public string Name { get; set; } = string.Empty;
            public List<Entry> Entries { get; set;  } = new List<Entry>();
            public class Entry
            {
                public ResourceAnimationType Type { get; set; }
                public uint FileIndex { get; set; }
                public List<Event> Events { get; set;  } = new List<Event>();
                public List<Point> WalkPoints { get; set;  } = new List<Point>();
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
        public class SystemModSet
        {
            public class Mod
            {
                public uint UnkUInt01 { get; set; }
                public uint UnkUInt02 { get; set; }
                public string GroupName { get; set; }
                public List<ModData> ModsData { get; set; }
            }
            public class ModData : IDataEmpty, IDataEnvMap, IDataParticle, IData256, IData272, IData768
            {
                public ushort UnkUShort01 { get; set; }
                public ushort UnkUShort02 { get; set; }
                public float UnkFloat01 { get; set; }
                public uint UnkUInt01 { get; set; }
                public uint IDataFlags { get; set; }
                public uint UnkUInt02 { get; set; }
                public uint UnkUInt03 { get; set; }
                public uint UnkUInt04 { get; set; }
                public uint UnkUInt05 { get; set; }

                #region Interface Implementations

                #region IDataEnvMap
                uint IDataEnvMap.IsEnabled { get; set; }
                uint IDataEnvMap.UnkUInt01 { get; set; }
                float IDataEnvMap.UnkFloat02 { get; set; }
                float IDataEnvMap.UnkFloat03 { get; set; }
                uint IDataEnvMap.UnkUInt02 { get; set; }
                uint IDataEnvMap.UnkUInt03 { get; set; }
                uint IDataEnvMap.UnkUInt04 { get; set; }
                uint IDataEnvMap.UnkUInt05 { get; set; }
                uint IDataEnvMap.UnkUInt06 { get; set; }
                uint IDataEnvMap.UnkUInt07 { get; set; }
                uint IDataEnvMap.UnkUInt08 { get; set; }
                uint IDataEnvMap.UnkUInt09 { get; set; }
                string IDataEnvMap.Name { get; set; } = string.Empty;
                List<IDataEnvMapEvent> IDataEnvMap.Events { get; set; } = new List<IDataEnvMapEvent>();
                #endregion

                #region IDataPartricle
                uint IDataParticle.IsEnabled { get; set; }
                uint IDataParticle.UnkUInt01 { get; set; }
                string IDataParticle.Path { get; set; } = string.Empty;
                uint IDataParticle.UnkUInt02 { get; set; }
                uint IDataParticle.UnkUInt03 { get; set; }
                uint IDataParticle.UnkUInt04 { get; set; }
                uint IDataParticle.UnkUInt05 { get; set; }
                uint IDataParticle.UnkUInt06 { get; set; }
                uint IDataParticle.UnkUInt07 { get; set; }
                #endregion

                #region IData256
                uint IData256.IsEnabled { get; set; }
                ushort IData256.UnkUShort01 { get; set; }
                ushort IData256.UnkUShort02 { get; set; }
                uint IData256.UnkUInt01 { get; set; }
                uint IData256.UnkUInt02 { get; set; }
                #endregion

                #region IData272
                uint IData272.UnkUInt01 { get; set; }
                uint IData272.UnkUInt02 { get; set; }
                uint IData272.UnkUInt03 { get; set; }
                uint IData272.UnkUInt04 { get; set; }
                uint IData272.UnkUInt05 { get; set; }
                float IData272.UnkFloat01 { get; set; }
                float IData272.UnkFloat02 { get; set; }
                float IData272.UnkFloat03 { get; set; }
                float IData272.UnkFloat04 { get; set; }
                uint IData272.UnkUInt06 { get; set; }
                float IData272.UnkFloat05 { get; set; }
                float IData272.UnkFloat06 { get; set; }
                float IData272.UnkFloat07 { get; set; }
                float IData272.UnkFloat08 { get; set; }
                uint IData272.UnkUInt07 { get; set; }
                uint IData272.UnkUInt08 { get; set; }
                uint IData272.UnkUInt09 { get; set; }
                uint IData272.UnkUInt10 { get; set; }
                ushort IData272.UnkUShort01 { get; set; }
                ushort IData272.UnkUShort02 { get; set; }
                ushort IData272.UnkUShort03 { get; set; }
                ushort IData272.UnkUShort04 { get; set; }
                ushort IData272.UnkUShort05 { get; set; }
                ushort IData272.UnkUShort06 { get; set; }
                float IData272.UnkFloat09 { get; set; }
                uint IData272.UnkUInt11 { get; set; }
                #endregion

                #region IData768
                ushort IData768.UnkUShort01 { get; set; }
                uint IData768.UnkUInt01 { get; set; }
                uint IData768.UnkUInt02 { get; set; }
                uint IData768.UnkUInt03 { get; set; }
                ushort IData768.UnkUShort02 { get; set; }
                uint IData768.UnkUInt04 { get; set; }
                uint IData768.UnkUInt05 { get; set; }
                uint IData768.UnkUInt06 { get; set; }
                uint IData768.UnkUInt07 { get; set; }
                uint IData768.UnkUInt08 { get; set; }
                uint IData768.UnkUInt09 { get; set; }
                #endregion

                #endregion
            }
            public interface IDataEmpty
			{

			}
            public interface IDataEnvMap
            {
                uint IsEnabled { get; set; }
                uint UnkUInt01 { get; set; }
                float UnkFloat02 { get; set; }
                float UnkFloat03 { get; set; }
                uint UnkUInt02 { get; set; }
                uint UnkUInt03 { get; set; }
                uint UnkUInt04 { get; set; }
                uint UnkUInt05 { get; set; }
                uint UnkUInt06 { get; set; }
                uint UnkUInt07 { get; set; }
                uint UnkUInt08 { get; set; }
                uint UnkUInt09 { get; set; }
                string Name { get; set; }
                List<IDataEnvMapEvent> Events { get; set; }
            }
            public class IDataEnvMapEvent
            {
                public uint IsEnabled { get; set; }
                public string Path { get; set; } = string.Empty;
                public uint Time { get; set; }
                public string Keyword { get; set; } = string.Empty;
            }
            public interface IDataParticle
            {
                uint IsEnabled { get; set; }
                uint UnkUInt01 { get; set; }
                string Path { get; set; }
                uint UnkUInt02 { get; set; }
                uint UnkUInt03 { get; set; }
                uint UnkUInt04 { get; set; }
                uint UnkUInt05 { get; set; }
                uint UnkUInt06 { get; set; }
                uint UnkUInt07 { get; set; }
            }
            public interface IData256
            {
                uint IsEnabled { get; set; }
                ushort UnkUShort01 { get; set; }
                ushort UnkUShort02 { get; set; }
                uint UnkUInt01 { get; set; }
                uint UnkUInt02 { get; set; }
            }
            public interface IData272
            {
                uint UnkUInt01 { get; set; }
                uint UnkUInt02 { get; set; }
                uint UnkUInt03 { get; set; }
                uint UnkUInt04 { get; set; }
                uint UnkUInt05 { get; set; }
                float UnkFloat01 { get; set; }
                float UnkFloat02 { get; set; }
                float UnkFloat03 { get; set; }
                float UnkFloat04 { get; set; }
                uint UnkUInt06 { get; set; }
                float UnkFloat05 { get; set; }
                float UnkFloat06 { get; set; }
                float UnkFloat07 { get; set; }
                float UnkFloat08 { get; set; }
                uint UnkUInt07 { get; set; }
                uint UnkUInt08 { get; set; }
                uint UnkUInt09 { get; set; }
                uint UnkUInt10 { get; set; }
                ushort UnkUShort01 { get; set; }
                ushort UnkUShort02 { get; set; }
                ushort UnkUShort03 { get; set; }
                ushort UnkUShort04 { get; set; }
                ushort UnkUShort05 { get; set; }
                ushort UnkUShort06 { get; set; }
                float UnkFloat09 { get; set; }
                uint UnkUInt11 { get; set; }
            }
            public interface IData768
            {
                ushort UnkUShort01 { get; set; }
                uint UnkUInt01 { get; set; }
                uint UnkUInt02 { get; set; }
                uint UnkUInt03 { get; set; }
                ushort UnkUShort02 { get; set; }
                uint UnkUInt04 { get; set; }
                uint UnkUInt05 { get; set; }
                uint UnkUInt06 { get; set; }
                uint UnkUInt07 { get; set; }
                uint UnkUInt08 { get; set; }
                uint UnkUInt09 { get; set; }
            }
        }
        #endregion
    }
}