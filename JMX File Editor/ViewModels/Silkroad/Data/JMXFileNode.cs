using JMXFileEditor.Silkroad.Data;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace JMXFileEditor.ViewModels
{
    /// <summary>
    /// Node to represent one JMX file property
    /// </summary>
    public class JMXFileNode : BaseViewModel
    {
        #region Private Members
        private object m_Value;
        #endregion

        #region Public Properties
        /// <summary>
        /// Descriptive title for the node
        /// </summary>
        public string Title { get; }
        /// <summary>
        /// Value handled by this node
        /// </summary>
        public object Value {
            get { return m_Value; }
            set {
                // Ignore sets if cannot be edited
                if (IsEditable)
                {
                    // Make sure the new value can be converted to the original value
                    var valueType = Value.GetType();
                    if (valueType.IsEnum)
                    {
                        m_Value = System.Enum.Parse(valueType, value.ToString(), true);
                    }
                    else
                    {
                        m_Value = System.Convert.ChangeType(value, valueType);
                    }
                    OnPropertyChanged(nameof(Value));
                }
            }
        }
        /// <summary>
        /// Check if the value has been set
        /// </summary>
        public bool HasValue { get { return Value != null; } }
        /// <summary>
        /// Check if the value can be edited
        /// </summary>
        public bool IsEditable { get; }
        /// <summary>
        /// Child nodes
        /// </summary>
        public ObservableCollection<JMXFileNode> Nodes { get; } = new ObservableCollection<JMXFileNode>();
        /// <summary>
        /// Check if the child nodes length can be modified
        /// </summary>
        public bool CanResizeChilds { get; }
        #endregion

        #region Commands
        /// <summary>
        /// Add a child to the node queue
        /// </summary>
        public ICommand CommandAddChild { get; set; }
        /// <summary>
        /// Insert a child at index selected
        /// </summary>
        public ICommand CommandInsertChild { get; set; }
        /// <summary>
        /// Remove a child at index selected
        /// </summary>
        public ICommand CommandRemoveChild { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a root node which contains childs only
        /// </summary>
        public JMXFileNode(string Title,bool CanResizeChilds = false)
        {
            this.Title = Title;
            this.CanResizeChilds = CanResizeChilds;
            // Command setup
            CommandAddChild = new RelayCommand(() => {

            });
            CommandInsertChild = new RelayCommand(() => {

            });
            CommandRemoveChild = new RelayCommand(() => {

            });
        }
        /// <summary>
        /// Creates a child node which contains the value
        /// </summary>
        public JMXFileNode(string Title, object Value, bool IsEditable = true)
        {
            this.Title = Title;
            this.m_Value = Value;
            this.IsEditable = IsEditable;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Creates a node containing everything from the file
        /// </summary>
        public static JMXFileNode Create(IJMXFile File)
        {
            JMXFileNode root = new JMXFileNode(File.Format);
            // Create nodes
            if (File is JMXVRES_0109 jmxvres_0109)
            {
                root.Nodes.Add(new JMXFileNode("Header", jmxvres_0109.m_Header, false));
                // Pointers
                root.Nodes.Add(new JMXFileNode("Pointer.Material", jmxvres_0109.m_PointerMaterial, false));
                root.Nodes.Add(new JMXFileNode("Pointer.Mesh", jmxvres_0109.m_PointerMesh, false));
                root.Nodes.Add(new JMXFileNode("Pointer.Skeleton", jmxvres_0109.m_PointerSkeleton, false));
                root.Nodes.Add(new JMXFileNode("Pointer.Animation", jmxvres_0109.m_PointerAnimation, false));
                root.Nodes.Add(new JMXFileNode("Pointer.MeshGroup", jmxvres_0109.m_PointerMeshGroup, false));
                root.Nodes.Add(new JMXFileNode("Pointer.AnimationGroup", jmxvres_0109.m_PointerAnimationGroup, false));
                root.Nodes.Add(new JMXFileNode("Pointer.SoundEffect", jmxvres_0109.m_PointerSoundEffect, false));
                root.Nodes.Add(new JMXFileNode("Pointer.BoundingBox", jmxvres_0109.m_PointerBoundingBox, false));
                // Flags
                root.Nodes.Add(new JMXFileNode("Flags.UInt01", jmxvres_0109.m_FlagUInt01));
                root.Nodes.Add(new JMXFileNode("Flags.UInt02", jmxvres_0109.m_FlagUInt02));
                root.Nodes.Add(new JMXFileNode("Flags.UInt03", jmxvres_0109.m_FlagUInt03));
                root.Nodes.Add(new JMXFileNode("Flags.UInt04", jmxvres_0109.m_FlagUInt04));
                root.Nodes.Add(new JMXFileNode("Flags.UInt05", jmxvres_0109.m_FlagUInt05));
                // Details
                root.Nodes.Add(new JMXFileNode("ResourceType", jmxvres_0109.m_ResourceType));
                root.Nodes.Add(new JMXFileNode("Name", jmxvres_0109.m_Name));
                var nodeLevel1 = new JMXFileNode("UnkByteArray01");
                for (int i = 0; i < jmxvres_0109.m_UnkByteArray01.Length; i++)
                    nodeLevel1.Nodes.Add(new JMXFileNode("[" + i + "]", jmxvres_0109.m_UnkByteArray01[i]));
                root.Nodes.Add(nodeLevel1);
                // Pointer.BoundingBox
                root.Nodes.Add(new JMXFileNode("RootMesh", jmxvres_0109.m_RootMesh));
                nodeLevel1 = new JMXFileNode("BoundingBox01");
                for (int i = 0; i < jmxvres_0109.m_BoundingBox01.Length; i++)
                    nodeLevel1.Nodes.Add(new JMXFileNode("[" + i + "]", jmxvres_0109.m_BoundingBox01[i]));
                root.Nodes.Add(nodeLevel1);
                nodeLevel1 = new JMXFileNode("BoundingBox02");
                for (int i = 0; i < jmxvres_0109.m_BoundingBox02.Length; i++)
                    nodeLevel1.Nodes.Add(new JMXFileNode("[" + i + "]", jmxvres_0109.m_BoundingBox02[i]));
                root.Nodes.Add(nodeLevel1);
                nodeLevel1 = new JMXFileNode("ExtraBoundingData");
                for (int i = 0; i < jmxvres_0109.m_ExtraBoundingData.Length; i++)
                    nodeLevel1.Nodes.Add(new JMXFileNode("[" + i + "]", jmxvres_0109.m_ExtraBoundingData[i]));
                root.Nodes.Add(nodeLevel1);
                // Pointer.Material
                nodeLevel1 = new JMXFileNode("Materials");
                for (int i = 0; i < jmxvres_0109.m_Materials.Length; i++)
                {
                    var nodeClassLevel1 = new JMXFileNode("[" + i + "]");
                    nodeClassLevel1.Nodes.Add(new JMXFileNode("Index", jmxvres_0109.m_Materials[i].Index));
                    nodeClassLevel1.Nodes.Add(new JMXFileNode("Path", jmxvres_0109.m_Materials[i].Path));
                    nodeLevel1.Nodes.Add(nodeClassLevel1);
                }
                root.Nodes.Add(nodeLevel1);
                // Pointer.Mesh
                nodeLevel1 = new JMXFileNode("Meshes");
                for (int i = 0; i < jmxvres_0109.m_Meshes.Length; i++)
                {
                    var nodeClassLevel1 = new JMXFileNode("[" + i + "]");
                    nodeClassLevel1.Nodes.Add(new JMXFileNode("Path", jmxvres_0109.m_Meshes[i].Path));
                    nodeClassLevel1.Nodes.Add(new JMXFileNode("UnkUInt01", jmxvres_0109.m_Meshes[i].UnkUInt01));
                    nodeLevel1.Nodes.Add(nodeClassLevel1);
                }
                root.Nodes.Add(nodeLevel1);
                // Pointer.Animation
                root.Nodes.Add(new JMXFileNode("UnkUInt01", jmxvres_0109.m_UnkUInt01));
                root.Nodes.Add(new JMXFileNode("UnkUInt02", jmxvres_0109.m_UnkUInt02));
                nodeLevel1 = new JMXFileNode("Animations", true);
                for (int i = 0; i < jmxvres_0109.m_Animations.Count; i++)
                {
                    var nodeClassLevel1 = new JMXFileNode("[" + i + "]");
                    nodeClassLevel1.Nodes.Add(new JMXFileNode("Path", jmxvres_0109.m_Animations[i].Path));
                    nodeLevel1.Nodes.Add(nodeClassLevel1);
                }
                root.Nodes.Add(nodeLevel1);
                // Pointer.Skeleton
                nodeLevel1 = new JMXFileNode("Skeletons");
                for (int i = 0; i < jmxvres_0109.m_Skeletons.Length; i++)
                {
                    var nodeClassLevel1 = new JMXFileNode("[" + i + "]");
                    nodeClassLevel1.Nodes.Add(new JMXFileNode("Path", jmxvres_0109.m_Skeletons[i].Path));
                    var nodeLevel2 = new JMXFileNode("ExtraData");
                    nodeClassLevel1.Nodes.Add(nodeLevel2);
                    for (int j = 0; j < jmxvres_0109.m_Skeletons[i].ExtraData.Length; j++)
                    {
                        nodeLevel2.Nodes.Add(new JMXFileNode("[" + j + "]", jmxvres_0109.m_Skeletons[i].ExtraData[j]));
                    }
                    nodeLevel1.Nodes.Add(nodeClassLevel1);
                }
                root.Nodes.Add(nodeLevel1);
                // Pointer.MeshGroup
                nodeLevel1 = new JMXFileNode("MeshGroups");
                for (int i = 0; i < jmxvres_0109.m_MeshGroups.Length; i++)
                {
                    var nodeClassLevel1 = new JMXFileNode("[" + i + "]");
                    nodeClassLevel1.Nodes.Add(new JMXFileNode("Name", jmxvres_0109.m_MeshGroups[i].Name));
                    var nodeLevel2 = new JMXFileNode("FileIndexes");
                    nodeClassLevel1.Nodes.Add(nodeLevel2);
                    for (int j = 0; j < jmxvres_0109.m_MeshGroups[i].FileIndexes.Length; j++)
                    {
                        nodeLevel2.Nodes.Add(new JMXFileNode("[" + j + "]", jmxvres_0109.m_MeshGroups[i].FileIndexes[j]));
                    }
                    nodeLevel1.Nodes.Add(nodeClassLevel1);
                }
                root.Nodes.Add(nodeLevel1);
                // Pointer.AnimationGroup
                nodeLevel1 = new JMXFileNode("AnimationGroups");
                for (int i = 0; i < jmxvres_0109.m_AnimationGroups.Length; i++)
                {
                    var nodeClassLevel1 = new JMXFileNode("[" + i + "]");
                    nodeClassLevel1.Nodes.Add(new JMXFileNode("Name", jmxvres_0109.m_AnimationGroups[i].Name));
                    var nodeLevel2 = new JMXFileNode("Entries");
                    nodeClassLevel1.Nodes.Add(nodeLevel2);
                    for (int j = 0; j < jmxvres_0109.m_AnimationGroups[i].Entries.Length; j++)
                    {
                        var nodeClassLevel2 = new JMXFileNode("[" + j + "]");
                        nodeClassLevel2.Nodes.Add(new JMXFileNode("Type", jmxvres_0109.m_AnimationGroups[i].Entries[j].Type));
                        nodeClassLevel2.Nodes.Add(new JMXFileNode("FileIndex", jmxvres_0109.m_AnimationGroups[i].Entries[j].FileIndex));
                        var nodeLevel3 = new JMXFileNode("Events");
                        nodeClassLevel2.Nodes.Add(nodeLevel3);
                        for (int k = 0; k < jmxvres_0109.m_AnimationGroups[i].Entries[j].Events.Length; k++)
                        {
                            var nodeClassLevel3 = new JMXFileNode("[" + k + "]");
                            nodeClassLevel3.Nodes.Add(new JMXFileNode("KeyTime", jmxvres_0109.m_AnimationGroups[i].Entries[j].Events[k].KeyTime));
                            nodeClassLevel3.Nodes.Add(new JMXFileNode("Type", jmxvres_0109.m_AnimationGroups[i].Entries[j].Events[k].Type));
                            nodeClassLevel3.Nodes.Add(new JMXFileNode("UnkUInt01", jmxvres_0109.m_AnimationGroups[i].Entries[j].Events[k].UnkUInt01));
                            nodeClassLevel3.Nodes.Add(new JMXFileNode("UnkUInt02", jmxvres_0109.m_AnimationGroups[i].Entries[j].Events[k].UnkUInt02));
                            nodeLevel3.Nodes.Add(nodeClassLevel3);
                        }
                        nodeClassLevel2.Nodes.Add(new JMXFileNode("WalkingLength", jmxvres_0109.m_AnimationGroups[i].Entries[j].WalkingLength));
                        nodeLevel3 = new JMXFileNode("WalkPoints");
                        nodeClassLevel2.Nodes.Add(nodeLevel3);
                        for (int k = 0; k < jmxvres_0109.m_AnimationGroups[i].Entries[j].WalkPoints.Length; k++)
                        {
                            var nodeClassLevel3 = new JMXFileNode("[" + k + "]");
                            nodeClassLevel3.Nodes.Add(new JMXFileNode("X", jmxvres_0109.m_AnimationGroups[i].Entries[j].WalkPoints[k].X));
                            nodeClassLevel3.Nodes.Add(new JMXFileNode("Y", jmxvres_0109.m_AnimationGroups[i].Entries[j].WalkPoints[k].Y));
                            nodeLevel3.Nodes.Add(nodeClassLevel3);
                        }
                        nodeLevel2.Nodes.Add(nodeClassLevel2);
                    }
                    nodeLevel1.Nodes.Add(nodeClassLevel1);
                }
                root.Nodes.Add(nodeLevel1);
                // Pointer.SoundEffect
                nodeLevel1 = new JMXFileNode("NonDecodedBytes");
                for (int i = 0; i < jmxvres_0109.m_NonDecodedBytes.Length; i++)
                {
                    nodeLevel1.Nodes.Add(new JMXFileNode("[" + i + "]", jmxvres_0109.m_NonDecodedBytes[i]));
                }
                root.Nodes.Add(nodeLevel1);
            }
            return root;
        }
        #endregion
    }
}
