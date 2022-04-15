using System;
using System.Collections.Generic;
namespace JMXFileEditor.ViewModels
{
	/// <summary>
	/// ViewModel representing an abstract data structure which contains more properties
	/// </summary>
	public abstract class JMXAbstract : JMXStructure
	{
		#region Private Members
		private Type m_CurrentType = null;
		#endregion

		#region Public Properties
		/// <summary>
		/// All types this abstract property supports
		/// </summary>
		public List<Type> AvailableTypes { get; } = new List<Type>();
		/// <summary>
		/// Current type from property
		/// </summary>
		public Type CurrentType
		{
			get { return m_CurrentType; }
			set { SetCurrentType(value); }
		}
		#endregion

		#region Constructor
		/// <summary>
		/// Creates a abstract node view model
		/// </summary>
		public JMXAbstract(string Name, Type[] AvailableTypes, Type CurrentType = null, object CurrentObject = null) : base(Name, AvailableTypes != null)
		{
			this.AvailableTypes.AddRange(AvailableTypes);
			// Set initial type/values
			SetCurrentType(CurrentType, CurrentObject);
		}
		#endregion

		#region Public Methods
		public abstract override object GetClassFrom(JMXStructure Structure);
		#endregion

		#region Private Helpers
		/// <summary>
		/// Set a concrete type of this class
		/// </summary>
		/// <param name="Type">The type to be converted this property</param>
		/// <param name="Obj">The object which contains the values</param>
		public void SetCurrentType(Type Type, object Obj = null)
		{
			// Make sure type provided is right
			if (Type != null && !AvailableTypes.Contains(Type))
				throw new ArgumentException("JMXAbstract error. Type not supported!");

			// Set current values
			m_CurrentType = Type;
			OnPropertyChanged(nameof(CurrentType));

			// Clear old values
			Childs.Clear();

			// Try to create it
			if (Type != null && m_SupportedFormats.TryGetValue(Type, out AddChildEventHandler handler))
				handler.Invoke(this, new AddChildEventArgs(Obj, Childs, IsEditable));
		}
		#endregion

		#region Public Static Helpers
		/// <summary>
		/// Converts quickly all types to array
		/// </summary>
		public static Type[] GetTypes(params Type[] types)
		{
			return types;
		}
		#endregion
	}
}
