namespace JMXFileEditor.ViewModels
{
    /// <summary>
    /// Viewmodel to represent any type of property the JMX file has
    /// </summary>
    public abstract class JMXProperty : BaseViewModel
    {
		#region Private Members
		private bool m_IsEditable;
		#endregion

		#region Public Properties
		/// <summary>
		/// Name to identify the property
		/// </summary>
		public string Name { get; }
        /// <summary>
        /// Check if the value can be edited
        /// </summary>
        public bool IsEditable { 
			get { return m_IsEditable; }
			set
			{
				m_IsEditable = value;
				OnPropertyChanged(nameof(IsEditable));
			}
		}
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        protected JMXProperty(string Name, bool IsEditable)
        {
            this.Name = Name;
            this.IsEditable = IsEditable;
        }
        #endregion
    }
}
