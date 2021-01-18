using System;
using System.Collections.Generic;
namespace JMXFileEditor.ViewModels
{
    /// <summary>
    /// ViewModel representing a strict variable in the structure
    /// </summary>
    public class JMXOption : JMXProperty
    {
        #region Private Members
        private object m_Value;
        #endregion

        #region Public Properties
        /// <summary>
        /// Value handled by this node
        /// </summary>
        public object Value
        {
            get { return m_Value; }
            set
            {
                // Check if value can be edited and is one of the options
                if (IsEditable && Options.Contains(Value))
                {
                    // Make sure the new value can be converted to the original value
                    var valueType = Value.GetType();
                    if (valueType.IsEnum)
                    {
                        m_Value = Enum.Parse(valueType, value.ToString(), true);
                    }
                    else
                    {
                        m_Value = Convert.ChangeType(value, valueType);
                    }
                    OnPropertyChanged(nameof(Value));
                }
            }
        }
        /// <summary>
        /// All possibilities the value can have
        /// </summary>
        public List<object> Options { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Creates a child node view model
        /// </summary>
        public JMXOption(string Name, object Value, List<object> Options,bool IsEditable = true) : base(Name, IsEditable)
        {
            m_Value = Value;
            this.Options = Options;
        }
        #endregion
    }
}
