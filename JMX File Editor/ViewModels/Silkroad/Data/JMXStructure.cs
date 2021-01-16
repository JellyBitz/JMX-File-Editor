using JMXFileEditor.Silkroad.Data;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace JMXFileEditor.ViewModels
{
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
        public JMXStructure(string Name, Type ChildType = null) : base(Name, ChildType != null)
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
                        nodeClass.Childs.Add(new JMXStructure("WalkPoints", typeof(System.Windows.Point)));
                    }
                    else if (ChildType == typeof(JMXVRES_0109.AnimationGroup.Entry.Event))
                    {
                        var animationGroupEntryEvent = new JMXVRES_0109.AnimationGroup.Entry.Event();
                        nodeClass.Childs.Add(new JMXAttribute("KeyTime", animationGroupEntryEvent.KeyTime));
                        nodeClass.Childs.Add(new JMXAttribute("Type", animationGroupEntryEvent.Type));
                        nodeClass.Childs.Add(new JMXAttribute("UnkUInt01", animationGroupEntryEvent.UnkUInt01));
                        nodeClass.Childs.Add(new JMXAttribute("UnkUInt02", animationGroupEntryEvent.UnkUInt02));
                    }
                    else if (ChildType == typeof(System.Windows.Point))
                    {
                        var animationGroupEntryEventPoint = new System.Windows.Point();
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
                root.Childs.Add(new JMXAttribute("Header", jmxvres_0109.m_Header,false));
                // Pointers
                root.Childs.Add(new JMXAttribute("Pointer.Material", jmxvres_0109.m_PointerMaterial, false));
                root.Childs.Add(new JMXAttribute("Pointer.Mesh", jmxvres_0109.m_PointerMesh, false));
                root.Childs.Add(new JMXAttribute("Pointer.Skeleton", jmxvres_0109.m_PointerSkeleton, false));
                root.Childs.Add(new JMXAttribute("Pointer.Animation", jmxvres_0109.m_PointerAnimation, false));
                root.Childs.Add(new JMXAttribute("Pointer.MeshGroup", jmxvres_0109.m_PointerMeshGroup, false));
                root.Childs.Add(new JMXAttribute("Pointer.AnimationGroup", jmxvres_0109.m_PointerAnimationGroup, false));
                root.Childs.Add(new JMXAttribute("Pointer.SoundEffect", jmxvres_0109.m_PointerSoundEffect, false));
                root.Childs.Add(new JMXAttribute("Pointer.BoundingBox", jmxvres_0109.m_PointerBoundingBox, false));
                // Flags
                root.Childs.Add(new JMXAttribute("Flags.UInt01", jmxvres_0109.m_FlagUInt01));
                root.Childs.Add(new JMXAttribute("Flags.UInt02", jmxvres_0109.m_FlagUInt02));
                root.Childs.Add(new JMXAttribute("Flags.UInt03", jmxvres_0109.m_FlagUInt03));
                root.Childs.Add(new JMXAttribute("Flags.UInt04", jmxvres_0109.m_FlagUInt04));
                root.Childs.Add(new JMXAttribute("Flags.UInt05", jmxvres_0109.m_FlagUInt05));
                // Details
                root.Childs.Add(new JMXAttribute("ResourceType", jmxvres_0109.m_ResourceType));
                root.Childs.Add(new JMXAttribute("Name", jmxvres_0109.m_Name));
                var nodeLevel1 = new JMXStructure("UnkByteArray01");
                for (int i = 0; i < jmxvres_0109.m_UnkByteArray01.Length; i++)
                    nodeLevel1.Childs.Add(new JMXAttribute("[" + i + "]", jmxvres_0109.m_UnkByteArray01[i]));
                root.Childs.Add(nodeLevel1);
                // Pointer.BoundingBox
                root.Childs.Add(new JMXAttribute("RootMesh", jmxvres_0109.m_RootMesh));
                nodeLevel1 = new JMXStructure("BoundingBox01");
                for (int i = 0; i < jmxvres_0109.m_BoundingBox01.Length; i++)
                    nodeLevel1.Childs.Add(new JMXAttribute("[" + i + "]", jmxvres_0109.m_BoundingBox01[i]));
                root.Childs.Add(nodeLevel1);
                nodeLevel1 = new JMXStructure("BoundingBox02");
                for (int i = 0; i < jmxvres_0109.m_BoundingBox02.Length; i++)
                    nodeLevel1.Childs.Add(new JMXAttribute("[" + i + "]", jmxvres_0109.m_BoundingBox02[i]));
                root.Childs.Add(nodeLevel1);
                nodeLevel1 = new JMXStructure("ExtraBoundingData", typeof(byte));
                for (int i = 0; i < jmxvres_0109.m_ExtraBoundingData.Length; i++)
                    nodeLevel1.Childs.Add(new JMXAttribute("[" + i + "]", jmxvres_0109.m_ExtraBoundingData[i]));
                root.Childs.Add(nodeLevel1);
                // Pointer.Material
                nodeLevel1 = new JMXStructure("Materials", typeof(JMXVRES_0109.Material));
                for (int i = 0; i < jmxvres_0109.m_Materials.Length; i++)
                {
                    var nodeClassLevel1 = new JMXStructure("[" + i + "]");
                    nodeClassLevel1.Childs.Add(new JMXAttribute("Index", jmxvres_0109.m_Materials[i].Index));
                    nodeClassLevel1.Childs.Add(new JMXAttribute("Path", jmxvres_0109.m_Materials[i].Path));
                    nodeLevel1.Childs.Add(nodeClassLevel1);
                }
                root.Childs.Add(nodeLevel1);
                // Pointer.Mesh
                nodeLevel1 = new JMXStructure("Meshes", typeof(JMXVRES_0109.Mesh));
                for (int i = 0; i < jmxvres_0109.m_Meshes.Length; i++)
                {
                    var nodeClassLevel1 = new JMXStructure("[" + i + "]");
                    nodeClassLevel1.Childs.Add(new JMXAttribute("Path", jmxvres_0109.m_Meshes[i].Path));
                    nodeClassLevel1.Childs.Add(new JMXAttribute("UnkUInt01", jmxvres_0109.m_Meshes[i].UnkUInt01));
                    nodeLevel1.Childs.Add(nodeClassLevel1);
                }
                root.Childs.Add(nodeLevel1);
                // Pointer.Animation
                root.Childs.Add(new JMXAttribute("UnkUInt01", jmxvres_0109.m_UnkUInt01));
                root.Childs.Add(new JMXAttribute("UnkUInt02", jmxvres_0109.m_UnkUInt02));
                nodeLevel1 = new JMXStructure("Animations", typeof(JMXVRES_0109.Animation));
                for (int i = 0; i < jmxvres_0109.m_Animations.Count; i++)
                {
                    var nodeClassLevel1 = new JMXStructure("[" + i + "]");
                    nodeClassLevel1.Childs.Add(new JMXAttribute("Path", jmxvres_0109.m_Animations[i].Path));
                    nodeLevel1.Childs.Add(nodeClassLevel1);
                }
                root.Childs.Add(nodeLevel1);
                // Pointer.Skeleton
                nodeLevel1 = new JMXStructure("Skeletons", typeof(JMXVRES_0109.Skeleton));
                for (int i = 0; i < jmxvres_0109.m_Skeletons.Length; i++)
                {
                    var nodeClassLevel1 = new JMXStructure("[" + i + "]");
                    nodeClassLevel1.Childs.Add(new JMXAttribute("Path", jmxvres_0109.m_Skeletons[i].Path));
                    var nodeLevel2 = new JMXStructure("ExtraData", typeof(byte));
                    nodeClassLevel1.Childs.Add(nodeLevel2);
                    for (int j = 0; j < jmxvres_0109.m_Skeletons[i].ExtraData.Length; j++)
                    {
                        nodeLevel2.Childs.Add(new JMXAttribute("[" + j + "]", jmxvres_0109.m_Skeletons[i].ExtraData[j]));
                    }
                    nodeLevel1.Childs.Add(nodeClassLevel1);
                }
                root.Childs.Add(nodeLevel1);
                // Pointer.MeshGroup
                nodeLevel1 = new JMXStructure("MeshGroups", typeof(JMXVRES_0109.MeshGroup));
                for (int i = 0; i < jmxvres_0109.m_MeshGroups.Length; i++)
                {
                    var nodeClassLevel1 = new JMXStructure("[" + i + "]");
                    nodeClassLevel1.Childs.Add(new JMXAttribute("Name", jmxvres_0109.m_MeshGroups[i].Name));
                    var nodeLevel2 = new JMXStructure("FileIndexes", typeof(uint));
                    nodeClassLevel1.Childs.Add(nodeLevel2);
                    for (int j = 0; j < jmxvres_0109.m_MeshGroups[i].FileIndexes.Length; j++)
                    {
                        nodeLevel2.Childs.Add(new JMXAttribute("[" + j + "]", jmxvres_0109.m_MeshGroups[i].FileIndexes[j]));
                    }
                    nodeLevel1.Childs.Add(nodeClassLevel1);
                }
                root.Childs.Add(nodeLevel1);
                // Pointer.AnimationGroup
                nodeLevel1 = new JMXStructure("AnimationGroups",typeof(JMXVRES_0109.AnimationGroup));
                for (int i = 0; i < jmxvres_0109.m_AnimationGroups.Length; i++)
                {
                    var nodeClassLevel1 = new JMXStructure("[" + i + "]");
                    nodeClassLevel1.Childs.Add(new JMXAttribute("Name", jmxvres_0109.m_AnimationGroups[i].Name));
                    var nodeLevel2 = new JMXStructure("Entries",typeof(JMXVRES_0109.AnimationGroup.Entry));
                    nodeClassLevel1.Childs.Add(nodeLevel2);
                    for (int j = 0; j < jmxvres_0109.m_AnimationGroups[i].Entries.Length; j++)
                    {
                        var nodeClassLevel2 = new JMXStructure("[" + j + "]");
                        nodeClassLevel2.Childs.Add(new JMXAttribute("Type", jmxvres_0109.m_AnimationGroups[i].Entries[j].Type));
                        nodeClassLevel2.Childs.Add(new JMXAttribute("FileIndex", jmxvres_0109.m_AnimationGroups[i].Entries[j].FileIndex));
                        var nodeLevel3 = new JMXStructure("Events",typeof(JMXVRES_0109.AnimationGroup.Entry.Event));
                        nodeClassLevel2.Childs.Add(nodeLevel3);
                        for (int k = 0; k < jmxvres_0109.m_AnimationGroups[i].Entries[j].Events.Length; k++)
                        {
                            var nodeClassLevel3 = new JMXStructure("[" + k + "]");
                            nodeClassLevel3.Childs.Add(new JMXAttribute("KeyTime", jmxvres_0109.m_AnimationGroups[i].Entries[j].Events[k].KeyTime));
                            nodeClassLevel3.Childs.Add(new JMXAttribute("Type", jmxvres_0109.m_AnimationGroups[i].Entries[j].Events[k].Type));
                            nodeClassLevel3.Childs.Add(new JMXAttribute("UnkUInt01", jmxvres_0109.m_AnimationGroups[i].Entries[j].Events[k].UnkUInt01));
                            nodeClassLevel3.Childs.Add(new JMXAttribute("UnkUInt02", jmxvres_0109.m_AnimationGroups[i].Entries[j].Events[k].UnkUInt02));
                            nodeLevel3.Childs.Add(nodeClassLevel3);
                        }
                        nodeClassLevel2.Childs.Add(new JMXAttribute("WalkingLength", jmxvres_0109.m_AnimationGroups[i].Entries[j].WalkingLength));
                        nodeLevel3 = new JMXStructure("WalkPoints",typeof(System.Windows.Point));
                        nodeClassLevel2.Childs.Add(nodeLevel3);
                        for (int k = 0; k < jmxvres_0109.m_AnimationGroups[i].Entries[j].WalkPoints.Length; k++)
                        {
                            var nodeClassLevel3 = new JMXStructure("[" + k + "]");
                            nodeClassLevel3.Childs.Add(new JMXAttribute("X", jmxvres_0109.m_AnimationGroups[i].Entries[j].WalkPoints[k].X));
                            nodeClassLevel3.Childs.Add(new JMXAttribute("Y", jmxvres_0109.m_AnimationGroups[i].Entries[j].WalkPoints[k].Y));
                            nodeLevel3.Childs.Add(nodeClassLevel3);
                        }
                        nodeLevel2.Childs.Add(nodeClassLevel2);
                    }
                    nodeLevel1.Childs.Add(nodeClassLevel1);
                }
                root.Childs.Add(nodeLevel1);
                // Pointer.SoundEffect
                nodeLevel1 = new JMXStructure("NonDecodedBytes");
                for (int i = 0; i < jmxvres_0109.m_NonDecodedBytes.Length; i++)
                {
                    nodeLevel1.Childs.Add(new JMXAttribute("[" + i + "]", jmxvres_0109.m_NonDecodedBytes[i]));
                }
                root.Childs.Add(nodeLevel1);
            }
            return root;
        }
        #endregion
    }
}
