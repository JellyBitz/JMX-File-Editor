using JMXFileEditor.Silkroad.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
namespace JMXFileEditor.ViewModels
{
    /// <summary>
    /// ViewModel representing a data structure which contains more properties
    /// </summary>
    public class JMXStructure : JMXProperty
    {
        #region Public Properties
        /// <summary>
        /// All the properties this structure contains
        /// </summary>
        public ObservableCollection<JMXProperty> Childs { get; } = new ObservableCollection<JMXProperty>();
        #endregion
        
        #region Commands
        /// <summary>
        /// Add a child to the node queue
        /// </summary>
        public ICommand CommandAddChild { get; set; }
        /// <summary>
        /// Remove a child at index selected
        /// </summary>
        public ICommand CommandRemoveChild { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a root node view model
        /// </summary>
        private JMXStructure(string Name, Type ChildType = null) : base(Name, ChildType != null)
        {
            // Make sure the type is specified
            if (ChildType == null)
            {
                CommandAddChild = new RelayCommand(() => { /* Do Nothing... */ });
                CommandRemoveChild = new RelayCommand(() => { /* Do Nothing... */ });
                return;
            }
            
            /// Commands setup
            CommandAddChild = new RelayCommand(() =>
            {
                /// Create and add a default instance to nodes queue
                string name = "[" + Childs.Count.ToString() + "]";
                // default value types
                if (ChildType == typeof(byte))
                {
                    Childs.Add(new JMXAttribute(name, default(byte)));
                }
                else if (ChildType == typeof(uint))
                {
                    Childs.Add(new JMXAttribute(name, default(uint)));
                }
                else
                {
                    // default classes types
                    var nodeClass = new JMXStructure(name);
                    if (ChildType == typeof(JMXVRES_0109.Material))
                    {
                        var material = new JMXVRES_0109.Material();
                        nodeClass.Childs.Add(new JMXAttribute("Index", material.Index));
                        nodeClass.Childs.Add(new JMXAttribute("Path", material.Path));
                    }
                    else if (ChildType == typeof(JMXVRES_0109.Mesh))
                    {
                        var mesh = new JMXVRES_0109.Mesh();
                        nodeClass.Childs.Add(new JMXAttribute("Path", mesh.Path));
                        nodeClass.Childs.Add(new JMXAttribute("UnkUInt01", mesh.UnkUInt01));
                    }
                    else if (ChildType == typeof(JMXVRES_0109.Animation))
                    {
                        var animation = new JMXVRES_0109.Animation();
                        nodeClass.Childs.Add(new JMXAttribute("Path", animation.Path));
                    }
                    else if (ChildType == typeof(JMXVRES_0109.Skeleton))
                    {
                        var skeleton = new JMXVRES_0109.Skeleton();
                        nodeClass.Childs.Add(new JMXAttribute("Path", skeleton.Path));
                        nodeClass.Childs.Add(new JMXStructure("ExtraData", typeof(byte)));
                    }
                    else if (ChildType == typeof(JMXVRES_0109.MeshGroup))
                    {
                        var meshGroup = new JMXVRES_0109.MeshGroup();
                        nodeClass.Childs.Add(new JMXAttribute("Name", meshGroup.Name));
                        nodeClass.Childs.Add(new JMXStructure("FileIndexes", typeof(uint)));
                    }
                    else if (ChildType == typeof(JMXVRES_0109.AnimationGroup))
                    {
                        var animationGroup = new JMXVRES_0109.AnimationGroup();
                        nodeClass.Childs.Add(new JMXAttribute("Name", animationGroup.Name));
                        nodeClass.Childs.Add(new JMXStructure("Entries", typeof(JMXVRES_0109.AnimationGroup.Entry)));
                    }
                    else if (ChildType == typeof(JMXVRES_0109.AnimationGroup.Entry))
                    {
                        var animationGroupEntry = new JMXVRES_0109.AnimationGroup.Entry();
                        nodeClass.Childs.Add(new JMXAttribute("Type", animationGroupEntry.Type));
                        nodeClass.Childs.Add(new JMXAttribute("FileIndex", animationGroupEntry.FileIndex));
                        nodeClass.Childs.Add(new JMXStructure("Events", typeof(JMXVRES_0109.AnimationGroup.Entry.Event)));
                        nodeClass.Childs.Add(new JMXAttribute("WalkingLength", animationGroupEntry.WalkingLength));
                        nodeClass.Childs.Add(new JMXStructure("WalkPoints", typeof(JMXVRES_0109.AnimationGroup.Entry.Point)));
                    }
                    else if (ChildType == typeof(JMXVRES_0109.AnimationGroup.Entry.Event))
                    {
                        var animationGroupEntryEvent = new JMXVRES_0109.AnimationGroup.Entry.Event();
                        nodeClass.Childs.Add(new JMXAttribute("KeyTime", animationGroupEntryEvent.KeyTime));
                        nodeClass.Childs.Add(new JMXAttribute("Type", animationGroupEntryEvent.Type));
                        nodeClass.Childs.Add(new JMXAttribute("UnkUInt01", animationGroupEntryEvent.UnkUInt01));
                        nodeClass.Childs.Add(new JMXAttribute("UnkUInt02", animationGroupEntryEvent.UnkUInt02));
                    }
                    else if (ChildType == typeof(JMXVRES_0109.AnimationGroup.Entry.Point))
                    {
                        var animationGroupEntryEventPoint = new JMXVRES_0109.AnimationGroup.Entry.Point();
                        nodeClass.Childs.Add(new JMXAttribute("X", animationGroupEntryEventPoint.X));
                        nodeClass.Childs.Add(new JMXAttribute("Y", animationGroupEntryEventPoint.Y));
                    }
                    else
                    {
                        // type not found
                        return;
                    }
                    Childs.Add(nodeClass);
                }
            });
            CommandRemoveChild = new RelayParameterizedCommand((object parameter) =>
            {
                // Make sure the item to remove is a correct value
                if (parameter is JMXProperty property)
                    // Try to remove it
                    Childs.Remove(property);
            });
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Converts the structure into a JMXFile.
        /// This method only work with a root JMXStructure
        /// </summary>
        public IJMXFile ToJMXFile()
        {
            // Read format
            string header = (string)((JMXAttribute)Childs[0]).Value;
            if (header == JMXVRES_0109.FileHeader)
            {
                JMXVRES_0109 file = new JMXVRES_0109();
                file.Header = header;
                // Pointers will be calculated in the process
                // Flags
                file.FlagUInt01 = (uint)((JMXAttribute)Childs[9]).Value;
                file.FlagUInt02 = (uint)((JMXAttribute)Childs[10]).Value;
                file.FlagUInt03 = (uint)((JMXAttribute)Childs[11]).Value;
                file.FlagUInt04 = (uint)((JMXAttribute)Childs[12]).Value;
                file.FlagUInt05 = (uint)((JMXAttribute)Childs[13]).Value;
                // Details
                file.ResourceType = (ResourceType)((JMXOption)Childs[14]).Value;
                file.Name = (string)((JMXAttribute)Childs[15]).Value;
                var nodeChilds = ((JMXStructure)Childs[16]).Childs;
                file.UnkByteArray01 = new byte[nodeChilds.Count];
                for (int i = 0; i < nodeChilds.Count; i++)
                {
                    file.UnkByteArray01[i] = (byte)((JMXAttribute)nodeChilds[i]).Value;
                }

                // Pointer.BoundingBox
                file.RootMesh = (string)((JMXAttribute)Childs[17]).Value;
                nodeChilds = ((JMXStructure)Childs[18]).Childs;
                file.BoundingBox01 = new float[nodeChilds.Count];
                for (int i = 0; i < nodeChilds.Count; i++)
                {
                    file.BoundingBox01[i] = (float)((JMXAttribute)nodeChilds[i]).Value;
                }
                nodeChilds = ((JMXStructure)Childs[19]).Childs;
                file.BoundingBox02 = new float[nodeChilds.Count];
                for (int i = 0; i < nodeChilds.Count; i++)
                {
                    file.BoundingBox02[i] = (float)((JMXAttribute)nodeChilds[i]).Value;
                }
                nodeChilds = ((JMXStructure)Childs[20]).Childs;
                file.ExtraBoundingData = new byte[nodeChilds.Count];
                for (int i = 0; i < nodeChilds.Count; i++)
                {
                    file.ExtraBoundingData[i] = (byte)((JMXAttribute)nodeChilds[i]).Value;
                }

                // Pointer.Material
                nodeChilds = ((JMXStructure)Childs[21]).Childs;
                file.Materials = new List<JMXVRES_0109.Material>(nodeChilds.Count);
                for (int i = 0; i < nodeChilds.Count; i++)
                {
                    // Create
                    var material = new JMXVRES_0109.Material();
                    file.Materials.Add(material);
                    // Copy
                    var nodeClass = ((JMXStructure)nodeChilds[i]).Childs;
                    material.Index = (uint)((JMXAttribute)nodeClass[0]).Value;
                    material.Path = (string)((JMXAttribute)nodeClass[1]).Value;
                }

                // Pointer.Mesh
                nodeChilds = ((JMXStructure)Childs[22]).Childs;
                file.Meshes = new List<JMXVRES_0109.Mesh>(nodeChilds.Count);
                for (int i = 0; i < nodeChilds.Count; i++)
                {
                    // Create
                    var mesh = new JMXVRES_0109.Mesh();
                    file.Meshes.Add(mesh);
                    // Copy
                    var nodeClass = ((JMXStructure)nodeChilds[i]).Childs;
                    mesh.Path = (string)((JMXAttribute)nodeClass[0]).Value;
                    mesh.UnkUInt01 = (uint)((JMXAttribute)nodeClass[1]).Value;
                }

                // Pointer.Animation
                file.UnkUInt01 = (uint)((JMXAttribute)Childs[23]).Value;
                file.UnkUInt02 = (uint)((JMXAttribute)Childs[24]).Value;
                nodeChilds = ((JMXStructure)Childs[25]).Childs;
                file.Animations = new List<JMXVRES_0109.Animation>(nodeChilds.Count);
                for (int i = 0; i < nodeChilds.Count; i++)
                {
                    // Create
                    var animation = new JMXVRES_0109.Animation();
                    file.Animations.Add(animation);
                    // Copy
                    var nodeClass = ((JMXStructure)nodeChilds[i]).Childs;
                    animation.Path = (string)((JMXAttribute)nodeClass[0]).Value;
                }

                // Pointer.Skeleton
                nodeChilds = ((JMXStructure)Childs[26]).Childs;
                file.Skeletons = new List<JMXVRES_0109.Skeleton>(nodeChilds.Count);
                for (int i = 0; i < nodeChilds.Count; i++)
                {
                    // Create
                    var skeleton = new JMXVRES_0109.Skeleton();
                    file.Skeletons.Add(skeleton);
                    // Copy
                    var nodeClass = ((JMXStructure)nodeChilds[i]).Childs;
                    skeleton.Path = (string)((JMXAttribute)nodeClass[0]).Value;
                    var _nodeChilds = ((JMXStructure)nodeClass[1]).Childs;
                    skeleton.ExtraData = new byte[_nodeChilds.Count];
                    for (int j = 0; j < _nodeChilds.Count; j++)
                    {
                        skeleton.ExtraData[j] = (byte)((JMXAttribute)_nodeChilds[j]).Value;
                    }
                }

                // Pointer.MeshGroup
                nodeChilds = ((JMXStructure)Childs[27]).Childs;
                file.MeshGroups = new List<JMXVRES_0109.MeshGroup>(nodeChilds.Count);
                for (int i = 0; i < nodeChilds.Count; i++)
                {
                    // Create
                    var meshGroup = new JMXVRES_0109.MeshGroup();
                    file.MeshGroups.Add(meshGroup);
                    // Copy
                    var nodeClass = ((JMXStructure)nodeChilds[i]).Childs;
                    meshGroup.Name = (string)((JMXAttribute)nodeClass[0]).Value;
                    var _nodeChilds = ((JMXStructure)nodeClass[1]).Childs;
                    meshGroup.FileIndexes = new uint[_nodeChilds.Count];
                    for (int j = 0; j < _nodeChilds.Count; j++)
                    {
                        meshGroup.FileIndexes[j] = (uint)((JMXAttribute)_nodeChilds[j]).Value;
                    }
                }

                // Pointer.AnimationGroups
                nodeChilds = ((JMXStructure)Childs[28]).Childs;
                file.AnimationGroups = new List<JMXVRES_0109.AnimationGroup>(nodeChilds.Count);
                for (int i = 0; i < nodeChilds.Count; i++)
                {
                    // Create
                    var animationGroup = new JMXVRES_0109.AnimationGroup();
                    file.AnimationGroups.Add(animationGroup);
                    // Copy
                    var nodeClass = ((JMXStructure)nodeChilds[i]).Childs;
                    animationGroup.Name = (string)((JMXAttribute)nodeClass[0]).Value;
                    var _nodeChilds = ((JMXStructure)nodeClass[1]).Childs;
                    animationGroup.Entries = new List<JMXVRES_0109.AnimationGroup.Entry>();;
                    for (int j = 0; j < _nodeChilds.Count; j++)
                    {
                        // Create
                        var entry = new JMXVRES_0109.AnimationGroup.Entry();
                        animationGroup.Entries.Add(entry);
                        // Copy
                        var _nodeClass = ((JMXStructure)_nodeChilds[j]).Childs;
                        entry.Type = (ResourceAnimationType)((JMXOption)_nodeClass[0]).Value;
                        entry.FileIndex = (uint)((JMXAttribute)_nodeClass[1]).Value;
                        var __nodeChilds = ((JMXStructure)_nodeClass[2]).Childs;
                        entry.Events = new List<JMXVRES_0109.AnimationGroup.Entry.Event>();
                        for (int k = 0; k < __nodeChilds.Count; k++)
                        {
                            // Create
                            var _event = new JMXVRES_0109.AnimationGroup.Entry.Event();
                            entry.Events.Add(_event);
                            // Copy
                            var __nodeClass = ((JMXStructure)__nodeChilds[k]).Childs;
                            _event.KeyTime = (uint)((JMXAttribute)__nodeClass[0]).Value;
                            _event.Type = (uint)((JMXAttribute)__nodeClass[1]).Value;
                            _event.UnkUInt01 = (uint)((JMXAttribute)__nodeClass[2]).Value;
                            _event.UnkUInt02 = (uint)((JMXAttribute)__nodeClass[3]).Value;
                        }
                        entry.WalkingLength = (float)((JMXAttribute)_nodeClass[3]).Value;
                        __nodeChilds = ((JMXStructure)_nodeClass[4]).Childs;
                        entry.WalkPoints = new List<JMXVRES_0109.AnimationGroup.Entry.Point>();
                        for (int k = 0; k < __nodeChilds.Count; k++)
                        {
                            // Create
                            var point = new JMXVRES_0109.AnimationGroup.Entry.Point();
                            entry.WalkPoints.Add(point);
                            // Copy
                            var __nodeClass = ((JMXStructure)__nodeChilds[k]).Childs;
                            point.X = (float)((JMXAttribute)__nodeClass[0]).Value;
                            point.Y = (float)((JMXAttribute)__nodeClass[1]).Value;
                        }
                    }
                }

                // Pointer.SoundEffect
                nodeChilds = ((JMXStructure)Childs[29]).Childs;
                file.SoundEffectUndecodedBytes = new byte[nodeChilds.Count];
                for (int i = 0; i < nodeChilds.Count; i++)
                {
                    file.SoundEffectUndecodedBytes[i] = (byte)((JMXAttribute)nodeChilds[i]).Value;
                }

                // Return result
                return file;
            }
            return null;
        }
        #endregion

        #region Static Helpers
        /// <summary>
        /// Creates a root node containing everything from the given file
        /// </summary>
        public static JMXStructure Create(IJMXFile File)
        {
            JMXStructure root = new JMXStructure(File.Format);
            // Create nodes
            if (File is JMXVRES_0109 jmxvres_0109)
            {
                root.Childs.Add(new JMXAttribute("Header", jmxvres_0109.Header,false));
                // Pointers
                root.Childs.Add(new JMXAttribute("Pointer.Material", jmxvres_0109.PointerMaterial, false));
                root.Childs.Add(new JMXAttribute("Pointer.Mesh", jmxvres_0109.PointerMesh, false));
                root.Childs.Add(new JMXAttribute("Pointer.Skeleton", jmxvres_0109.PointerSkeleton, false));
                root.Childs.Add(new JMXAttribute("Pointer.Animation", jmxvres_0109.PointerAnimation, false));
                root.Childs.Add(new JMXAttribute("Pointer.MeshGroup", jmxvres_0109.PointerMeshGroup, false));
                root.Childs.Add(new JMXAttribute("Pointer.AnimationGroup", jmxvres_0109.PointerAnimationGroup, false));
                root.Childs.Add(new JMXAttribute("Pointer.SoundEffect", jmxvres_0109.PointerSoundEffect, false));
                root.Childs.Add(new JMXAttribute("Pointer.BoundingBox", jmxvres_0109.PointerBoundingBox, false));
                // Flags
                root.Childs.Add(new JMXAttribute("Flags.UInt01", jmxvres_0109.FlagUInt01));
                root.Childs.Add(new JMXAttribute("Flags.UInt02", jmxvres_0109.FlagUInt02));
                root.Childs.Add(new JMXAttribute("Flags.UInt03", jmxvres_0109.FlagUInt03));
                root.Childs.Add(new JMXAttribute("Flags.UInt04", jmxvres_0109.FlagUInt04));
                root.Childs.Add(new JMXAttribute("Flags.UInt05", jmxvres_0109.FlagUInt05));
                // Details
                root.Childs.Add(new JMXOption("ResourceType", jmxvres_0109.ResourceType, GetValues<object>(typeof(ResourceType))));
                root.Childs.Add(new JMXAttribute("Name", jmxvres_0109.Name));
                var nodeLevel1 = new JMXStructure("UnkByteArray01");
                for (int i = 0; i < jmxvres_0109.UnkByteArray01.Length; i++)
                    nodeLevel1.Childs.Add(new JMXAttribute("[" + i + "]", jmxvres_0109.UnkByteArray01[i]));
                root.Childs.Add(nodeLevel1);
                // Pointer.BoundingBox
                root.Childs.Add(new JMXAttribute("RootMesh", jmxvres_0109.RootMesh));
                nodeLevel1 = new JMXStructure("BoundingBox01");
                for (int i = 0; i < jmxvres_0109.BoundingBox01.Length; i++)
                    nodeLevel1.Childs.Add(new JMXAttribute("[" + i + "]", jmxvres_0109.BoundingBox01[i]));
                root.Childs.Add(nodeLevel1);
                nodeLevel1 = new JMXStructure("BoundingBox02");
                for (int i = 0; i < jmxvres_0109.BoundingBox02.Length; i++)
                    nodeLevel1.Childs.Add(new JMXAttribute("[" + i + "]", jmxvres_0109.BoundingBox02[i]));
                root.Childs.Add(nodeLevel1);
                nodeLevel1 = new JMXStructure("ExtraBoundingData", typeof(byte));
                for (int i = 0; i < jmxvres_0109.ExtraBoundingData.Length; i++)
                    nodeLevel1.Childs.Add(new JMXAttribute("[" + i + "]", jmxvres_0109.ExtraBoundingData[i]));
                root.Childs.Add(nodeLevel1);
                // Pointer.Material
                nodeLevel1 = new JMXStructure("Materials", typeof(JMXVRES_0109.Material));
                for (int i = 0; i < jmxvres_0109.Materials.Count; i++)
                {
                    var nodeClassLevel1 = new JMXStructure("[" + i + "]");
                    nodeClassLevel1.Childs.Add(new JMXAttribute("Index", jmxvres_0109.Materials[i].Index));
                    nodeClassLevel1.Childs.Add(new JMXAttribute("Path", jmxvres_0109.Materials[i].Path));
                    nodeLevel1.Childs.Add(nodeClassLevel1);
                }
                root.Childs.Add(nodeLevel1);
                // Pointer.Mesh
                nodeLevel1 = new JMXStructure("Meshes", typeof(JMXVRES_0109.Mesh));
                for (int i = 0; i < jmxvres_0109.Meshes.Count; i++)
                {
                    var nodeClassLevel1 = new JMXStructure("[" + i + "]");
                    nodeClassLevel1.Childs.Add(new JMXAttribute("Path", jmxvres_0109.Meshes[i].Path));
                    nodeClassLevel1.Childs.Add(new JMXAttribute("UnkUInt01", jmxvres_0109.Meshes[i].UnkUInt01));
                    nodeLevel1.Childs.Add(nodeClassLevel1);
                }
                root.Childs.Add(nodeLevel1);
                // Pointer.Animation
                root.Childs.Add(new JMXAttribute("UnkUInt01", jmxvres_0109.UnkUInt01));
                root.Childs.Add(new JMXAttribute("UnkUInt02", jmxvres_0109.UnkUInt02));
                nodeLevel1 = new JMXStructure("Animations", typeof(JMXVRES_0109.Animation));
                for (int i = 0; i < jmxvres_0109.Animations.Count; i++)
                {
                    var nodeClassLevel1 = new JMXStructure("[" + i + "]");
                    nodeClassLevel1.Childs.Add(new JMXAttribute("Path", jmxvres_0109.Animations[i].Path));
                    nodeLevel1.Childs.Add(nodeClassLevel1);
                }
                root.Childs.Add(nodeLevel1);
                // Pointer.Skeleton
                nodeLevel1 = new JMXStructure("Skeletons", typeof(JMXVRES_0109.Skeleton));
                for (int i = 0; i < jmxvres_0109.Skeletons.Count; i++)
                {
                    var nodeClassLevel1 = new JMXStructure("[" + i + "]");
                    nodeClassLevel1.Childs.Add(new JMXAttribute("Path", jmxvres_0109.Skeletons[i].Path));
                    var nodeLevel2 = new JMXStructure("ExtraData", typeof(byte));
                    nodeClassLevel1.Childs.Add(nodeLevel2);
                    for (int j = 0; j < jmxvres_0109.Skeletons[i].ExtraData.Length; j++)
                    {
                        nodeLevel2.Childs.Add(new JMXAttribute("[" + j + "]", jmxvres_0109.Skeletons[i].ExtraData[j]));
                    }
                    nodeLevel1.Childs.Add(nodeClassLevel1);
                }
                root.Childs.Add(nodeLevel1);
                // Pointer.MeshGroup
                nodeLevel1 = new JMXStructure("MeshGroups", typeof(JMXVRES_0109.MeshGroup));
                for (int i = 0; i < jmxvres_0109.MeshGroups.Count; i++)
                {
                    var nodeClassLevel1 = new JMXStructure("[" + i + "]");
                    nodeClassLevel1.Childs.Add(new JMXAttribute("Name", jmxvres_0109.MeshGroups[i].Name));
                    var nodeLevel2 = new JMXStructure("FileIndexes", typeof(uint));
                    nodeClassLevel1.Childs.Add(nodeLevel2);
                    for (int j = 0; j < jmxvres_0109.MeshGroups[i].FileIndexes.Length; j++)
                    {
                        nodeLevel2.Childs.Add(new JMXAttribute("[" + j + "]", jmxvres_0109.MeshGroups[i].FileIndexes[j]));
                    }
                    nodeLevel1.Childs.Add(nodeClassLevel1);
                }
                root.Childs.Add(nodeLevel1);
                // Pointer.AnimationGroup
                nodeLevel1 = new JMXStructure("AnimationGroups",typeof(JMXVRES_0109.AnimationGroup));
                for (int i = 0; i < jmxvres_0109.AnimationGroups.Count; i++)
                {
                    var nodeClassLevel1 = new JMXStructure("[" + i + "]");
                    nodeClassLevel1.Childs.Add(new JMXAttribute("Name", jmxvres_0109.AnimationGroups[i].Name));
                    var nodeLevel2 = new JMXStructure("Entries",typeof(JMXVRES_0109.AnimationGroup.Entry));
                    nodeClassLevel1.Childs.Add(nodeLevel2);
                    for (int j = 0; j < jmxvres_0109.AnimationGroups[i].Entries.Count; j++)
                    {
                        var nodeClassLevel2 = new JMXStructure("[" + j + "]");
                        var options = Enum.GetValues(typeof(ResourceAnimationType));
                        nodeClassLevel2.Childs.Add(new JMXOption("Type", jmxvres_0109.AnimationGroups[i].Entries[j].Type,GetValues<object>(typeof(ResourceAnimationType))));
                        nodeClassLevel2.Childs.Add(new JMXAttribute("FileIndex", jmxvres_0109.AnimationGroups[i].Entries[j].FileIndex));
                        var nodeLevel3 = new JMXStructure("Events",typeof(JMXVRES_0109.AnimationGroup.Entry.Event));
                        nodeClassLevel2.Childs.Add(nodeLevel3);
                        for (int k = 0; k < jmxvres_0109.AnimationGroups[i].Entries[j].Events.Count; k++)
                        {
                            var nodeClassLevel3 = new JMXStructure("[" + k + "]");
                            nodeClassLevel3.Childs.Add(new JMXAttribute("KeyTime", jmxvres_0109.AnimationGroups[i].Entries[j].Events[k].KeyTime));
                            nodeClassLevel3.Childs.Add(new JMXAttribute("Type", jmxvres_0109.AnimationGroups[i].Entries[j].Events[k].Type));
                            nodeClassLevel3.Childs.Add(new JMXAttribute("UnkUInt01", jmxvres_0109.AnimationGroups[i].Entries[j].Events[k].UnkUInt01));
                            nodeClassLevel3.Childs.Add(new JMXAttribute("UnkUInt02", jmxvres_0109.AnimationGroups[i].Entries[j].Events[k].UnkUInt02));
                            nodeLevel3.Childs.Add(nodeClassLevel3);
                        }
                        nodeClassLevel2.Childs.Add(new JMXAttribute("WalkingLength", jmxvres_0109.AnimationGroups[i].Entries[j].WalkingLength));
                        nodeLevel3 = new JMXStructure("WalkPoints",typeof(JMXVRES_0109.AnimationGroup.Entry.Point));
                        nodeClassLevel2.Childs.Add(nodeLevel3);
                        for (int k = 0; k < jmxvres_0109.AnimationGroups[i].Entries[j].WalkPoints.Count; k++)
                        {
                            var nodeClassLevel3 = new JMXStructure("[" + k + "]");
                            nodeClassLevel3.Childs.Add(new JMXAttribute("X", jmxvres_0109.AnimationGroups[i].Entries[j].WalkPoints[k].X));
                            nodeClassLevel3.Childs.Add(new JMXAttribute("Y", jmxvres_0109.AnimationGroups[i].Entries[j].WalkPoints[k].Y));
                            nodeLevel3.Childs.Add(nodeClassLevel3);
                        }
                        nodeLevel2.Childs.Add(nodeClassLevel2);
                    }
                    nodeLevel1.Childs.Add(nodeClassLevel1);
                }
                root.Childs.Add(nodeLevel1);
                // Pointer.SoundEffect
                nodeLevel1 = new JMXStructure("SoundEffectUndecodedBytes");
                for (int i = 0; i < jmxvres_0109.SoundEffectUndecodedBytes.Length; i++)
                {
                    nodeLevel1.Childs.Add(new JMXAttribute("[" + i + "]", jmxvres_0109.SoundEffectUndecodedBytes[i]));
                }
                root.Childs.Add(nodeLevel1);
                return root;
            }
            return null;
        }
        #endregion

        #region Private Helpers
        /// <summary>
        /// Get all the values from the enum type specified as array list
        /// </summary>
        public static List<T> GetValues<T>(Type EnumType)
        {
            return Enum.GetValues(EnumType).Cast<T>().ToList();
        }
        #endregion
    }
}
