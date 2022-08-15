using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace JMXFileEditor.ViewModels
{
    /// <summary>
    /// ViewModel representing a data structure which contains more properties
    /// </summary>
    public class JMXStructure : JMXProperty
    {
        #region Private Members
        /// <summary>
        /// Supported structure formats 
        /// </summary>
        protected Dictionary<Type, AddChildEventHandler> m_SupportedFormats = new Dictionary<Type, AddChildEventHandler>();
        private bool m_IsSizeable;
        #endregion

        #region Public Properties
        /// <summary>
        /// Check if child nodes can be resized
        /// </summary>
        public bool IsSizeable
        {
            get => m_IsSizeable;
            set
            {
                m_IsSizeable = value;
                OnPropertyChanged(nameof(IsSizeable));
            }
        }
        /// <summary>
        /// All the properties this structure contains
        /// </summary>
        public ObservableCollection<JMXProperty> Childs { get; } = new ObservableCollection<JMXProperty>();
        /// <summary>
        /// Type of the nodes to be added
        /// </summary>
        public Type ChildType { get; }
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
        /// Creates a simple root node as view model
        /// </summary>
        public JMXStructure(string Name, bool IsEditable) : base(Name, IsEditable)
        {
            AddFormats();

            ChildType = null;
            /// Commands setup
            CommandAddChild = new RelayCommand(() => AddChild(null));
            CommandRemoveChild = new RelayParameterizedCommand((object parameter) => RemoveChild(parameter));
        }
        /// <summary>
        /// Creates a flexible root node as view model
        /// </summary>
        public JMXStructure(string Name, bool IsEditable, Type ChildType, bool IsSizeable = true) : base(Name, IsEditable)
        {
            this.IsSizeable = ChildType != null && IsSizeable;
            this.ChildType = ChildType;
            /// Commands setup
            CommandAddChild = new RelayCommand(() => AddChild(null));
            CommandRemoveChild = new RelayParameterizedCommand((object parameter) => RemoveChild(parameter));
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Adds a new class format to be handled
        /// </summary>
        /// <param name="Type">Type from the new class</param>
        public void AddFormatHandler(Type Type, AddChildEventHandler Handler)
        {
            // Override handler
            m_SupportedFormats[Type] = Handler;
        }
        /// <summary>
        /// Adds a generic data array
        /// </summary>
        public void AddChildArray(string Name, Array Values, bool IsEditable, bool IsSizeable)
        {
            var section = new JMXStructure(Name, IsEditable, Values.GetType().GetElementType(), true);
            section.m_SupportedFormats = m_SupportedFormats; // Keep all formats currently handled
            for (int i = 0; i < Values.Length; i++)
                section.AddChild(Values.GetValue(i));
            section.IsSizeable = IsSizeable;
            Childs.Add(section);
        }
        /// <summary>
        /// Adds a child node using the type specified. Return success.
        /// </summary>
        /// <param name="Obj">Object with the values, or default values will be added if null</param>
        public bool AddChild(object Obj)
        {
            // It cannot add/remove objects
            if (IsSizeable)
            {
                if (m_SupportedFormats.TryGetValue(ChildType, out AddChildEventHandler handler))
                {
                    handler.Invoke(this, new AddChildEventArgs(Obj, Childs, IsEditable));
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Remove a child object specified
        /// </summary>
        /// <param name="Object">The child to be removed</param>
        public bool RemoveChild(object Object)
        {
            // Check sizeable
            if (IsSizeable)
            {
                // Make sure the item to remove is a correct type
                if (Object is JMXProperty property)
                    // Try to remove it
                    return Childs.Remove(property);
            }
            return false;
        }
        /// <summary>
        /// Get the original class handled by this node
        /// </summary>
        public object GetClass()
        {
            return GetClassFrom(this);
        }
        /// <summary>
        /// Get the class from provided structure
        /// </summary>
        public virtual object GetClassFrom(JMXStructure Structure)
        {
            return GetClassFrom(Structure, 0);
        }
        /// <summary>
        /// Get the class from provided structure starting from the child index specified
        /// </summary>
        public virtual object GetClassFrom(JMXStructure Structure, int StartIndex)
        {
            return null;
        }
        /// <summary>
        /// Try to convert this structure to generic list
        /// </summary>
        public List<T> GetChildList<T>()
        {
            if (ChildType != null)
            {
                List<T> array = new List<T>();
                for (int i = 0; i < Childs.Count; i++)
                {
                    if (Childs[i] is JMXAttribute attribute)
                    {
                        array.Add((T)attribute.Value);
                    }
                    else if (Childs[i] is JMXOption option)
                    {
                        array.Add((T)option.Value);
                    }
                    else if (Childs[i] is JMXStructure structure)
                    {
                        array.Add((T)structure.GetClass());
                    }
                }
                return array;
            }
            return null;
        }
        #endregion

        #region Private Helpers
        /// <summary>
        /// Base formats supported as nodes
        /// </summary>
        private void AddFormats()
        {
            AddFormatHandler(typeof(byte), (s, e) => {
                e.Childs.Add(new JMXAttribute("[" + e.Childs.Count + "]", e.Obj is byte _byte ? _byte : default, e.IsEditable));
            });
            AddFormatHandler(typeof(sbyte), (s, e) => {
                e.Childs.Add(new JMXAttribute("[" + e.Childs.Count + "]", e.Obj is sbyte _sbyte ? _sbyte : default, e.IsEditable));
            });
            AddFormatHandler(typeof(ushort), (s, e) => {
                e.Childs.Add(new JMXAttribute("[" + e.Childs.Count + "]", e.Obj is ushort _ushort ? _ushort : default, e.IsEditable));
            });
            AddFormatHandler(typeof(short), (s, e) => {
                e.Childs.Add(new JMXAttribute("[" + e.Childs.Count + "]", e.Obj is short _short ? _short : default, e.IsEditable));
            });
            AddFormatHandler(typeof(uint), (s, e) => {
                e.Childs.Add(new JMXAttribute("[" + e.Childs.Count + "]", e.Obj is uint _uint ? _uint : default, e.IsEditable));
            });
            AddFormatHandler(typeof(int), (s, e) => {
                e.Childs.Add(new JMXAttribute("[" + e.Childs.Count + "]", e.Obj is int _int ? _int : default, e.IsEditable));
            });
            AddFormatHandler(typeof(ulong), (s, e) => {
                e.Childs.Add(new JMXAttribute("[" + e.Childs.Count + "]", e.Obj is ulong _ulong ? _ulong : default, e.IsEditable));
            });
            AddFormatHandler(typeof(long), (s, e) => {
                e.Childs.Add(new JMXAttribute("[" + e.Childs.Count + "]", e.Obj is long _long ? _long : default, e.IsEditable));
            });
            AddFormatHandler(typeof(float), (s, e) => {
                e.Childs.Add(new JMXAttribute("[" + e.Childs.Count + "]", e.Obj is float _float ? _float : default, e.IsEditable));
            });
            AddFormatHandler(typeof(double), (s, e) => {
                e.Childs.Add(new JMXAttribute("[" + e.Childs.Count + "]", e.Obj is double _double ? _double : default, e.IsEditable));
            });
            AddFormatHandler(typeof(char), (s, e) => {
                e.Childs.Add(new JMXAttribute("[" + e.Childs.Count + "]", e.Obj is char _char ? _char : default, e.IsEditable));
            });
            AddFormatHandler(typeof(string), (s, e) => {
                e.Childs.Add(new JMXAttribute("[" + e.Childs.Count + "]", e.Obj is string _string ? _string : string.Empty, e.IsEditable));
            });
        }
        #endregion

        #region Internal Classes
        /// <summary>
        /// Called when class format structure is found
        /// </summary>
        /// <param name="sender">Parent object adding this child</param>
        public delegate void AddChildEventHandler(object sender, AddChildEventArgs e);
        public class AddChildEventArgs
        {
            /// <summary>
            /// Child object to be added
            /// </summary>
            public object Obj { get; }
            /// <summary>
            /// Parent nodes
            /// </summary>
            public ObservableCollection<JMXProperty> Childs { get; }
            public bool IsEditable { get; }
            public AddChildEventArgs(object Obj, ObservableCollection<JMXProperty> Childs, bool IsEditable)
            {
                this.Obj = Obj;
                this.Childs = Childs;
                this.IsEditable = IsEditable;
            }
        }
        #endregion
    }
}