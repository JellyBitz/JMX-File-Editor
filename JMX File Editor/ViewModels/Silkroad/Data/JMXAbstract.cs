using JMXFileEditor.Silkroad.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace JMXFileEditor.ViewModels
{
	/// <summary>
	/// ViewModel representing an abstract data structure which contains more properties
	/// </summary>
	public class JMXAbstract : JMXProperty
	{
		#region Private Members
		/// <summary>
		/// Current
		/// </summary>
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
		public Type CurrentType {
			get { return m_CurrentType; }
			set
			{
				SetCurrentType(value);
			}
		}
		/// <summary>
		/// All the properties this structure contains
		/// </summary>
		public ObservableCollection<JMXProperty> Childs { get; } = new ObservableCollection<JMXProperty>();
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

		#region Private Helpers
		/// <summary>
		/// Set a concrete type of this class
		/// </summary>
		/// <param name="Type">The type to be converted this property</param>
		/// <param name="Object">The object which contains the values</param>
		public void SetCurrentType(Type Type, object Object = null)
		{
			// Make sure type provided is right
			if(Type != null && !this.AvailableTypes.Contains(Type))
				throw new ArgumentException("JMXAbstract error. Type not supported!");

			// Set value
			m_CurrentType = Type;
			OnPropertyChanged(nameof(CurrentType));

			// Clear old values
			Childs.Clear();

			// Unspecified type
			if (Type == null)
				return;
			// Concrete type
			if (Type == typeof(JMXVRES_0109.SystemModSet.IDataEnvMap))
			{
				var data = Object is JMXVRES_0109.SystemModSet.IDataEnvMap ? Object as JMXVRES_0109.SystemModSet.IDataEnvMap : new JMXVRES_0109.SystemModSet.ModData();
				Childs.Add(new JMXAttribute("IsEnabled", data.IsEnabled));
				Childs.Add(new JMXAttribute("UnkUInt01", data.UnkUInt01));
				Childs.Add(new JMXAttribute("UnkUInt02", data.UnkUInt02));
				Childs.Add(new JMXAttribute("UnkUInt03", data.UnkUInt03));
				Childs.Add(new JMXAttribute("UnkFloat02", data.UnkFloat02));
				Childs.Add(new JMXAttribute("UnkFloat03", data.UnkFloat03));
				Childs.Add(new JMXAttribute("UnkUInt04", data.UnkUInt04));
				Childs.Add(new JMXAttribute("UnkUInt05", data.UnkUInt05));
				Childs.Add(new JMXAttribute("UnkUInt06", data.UnkUInt06));
				Childs.Add(new JMXAttribute("UnkUInt07", data.UnkUInt07));
				Childs.Add(new JMXAttribute("UnkUInt08", data.UnkUInt08));
				Childs.Add(new JMXAttribute("UnkUInt09", data.UnkUInt09));
				Childs.Add(new JMXAttribute("Name", data.Name));
				var n5 = new JMXStructure("Events", typeof(JMXVRES_0109.SystemModSet.IDataEnvMapEvent));
				Childs.Add(n5);
				for (int x = 0; x < data.Events.Count; x++)
				{
					var nc5 = new JMXStructure("[" + x + "]");
					n5.Childs.Add(nc5);
					nc5.Childs.Add(new JMXAttribute("IsEnabled", data.Events[x].IsEnabled));
					nc5.Childs.Add(new JMXAttribute("Path", data.Events[x].Path));
					nc5.Childs.Add(new JMXAttribute("Time", data.Events[x].Time));
					nc5.Childs.Add(new JMXAttribute("Keyword", data.Events[x].Keyword));
				}
			}
			else if (Type == typeof(JMXVRES_0109.SystemModSet.IDataParticle))
			{
				var data = Object is JMXVRES_0109.SystemModSet.IDataParticle ? Object as JMXVRES_0109.SystemModSet.IDataParticle : new JMXVRES_0109.SystemModSet.ModData();
				var n5 = new JMXStructure("Particles", typeof(JMXVRES_0109.SystemModSet.IDataPatricleInfo));
				Childs.Add(n5);
				foreach (var particleInfo in data.Particles)
					n5.AddChild(particleInfo);
			}
			else if (Type == typeof(JMXVRES_0109.SystemModSet.IData256))
			{
				var data = Object is JMXVRES_0109.SystemModSet.IData256 ? Object as JMXVRES_0109.SystemModSet.IData256 : new JMXVRES_0109.SystemModSet.ModData();
				Childs.Add(new JMXAttribute("IsEnabled", data.IsEnabled));
				Childs.Add(new JMXAttribute("UnkUShort01", data.UnkUShort01));
				Childs.Add(new JMXAttribute("UnkUShort02", data.UnkUShort02));
				Childs.Add(new JMXAttribute("UnkUInt01", data.UnkUInt01));
				Childs.Add(new JMXAttribute("UnkUInt02", data.UnkUInt02));
			}
			else if (Type == typeof(JMXVRES_0109.SystemModSet.IData272))
			{
				var data = Object is JMXVRES_0109.SystemModSet.IData272 ? Object as JMXVRES_0109.SystemModSet.IData272 : new JMXVRES_0109.SystemModSet.ModData();
				Childs.Add(new JMXAttribute("UnkUInt01", data.UnkUInt01));
				Childs.Add(new JMXAttribute("UnkUInt02", data.UnkUInt02));
				Childs.Add(new JMXAttribute("UnkUInt03", data.UnkUInt03));
				Childs.Add(new JMXAttribute("UnkUInt04", data.UnkUInt04));
				Childs.Add(new JMXAttribute("UnkUInt05", data.UnkUInt05));
				Childs.Add(new JMXAttribute("UnkFloat01", data.UnkFloat01));
				Childs.Add(new JMXAttribute("UnkFloat02", data.UnkFloat02));
				Childs.Add(new JMXAttribute("UnkFloat03", data.UnkFloat03));
				Childs.Add(new JMXAttribute("UnkFloat04", data.UnkFloat04));
				Childs.Add(new JMXAttribute("UnkUInt06", data.UnkUInt06));
				Childs.Add(new JMXAttribute("UnkFloat05", data.UnkFloat05));
				Childs.Add(new JMXAttribute("UnkFloat06", data.UnkFloat06));
				Childs.Add(new JMXAttribute("UnkFloat07", data.UnkFloat07));
				Childs.Add(new JMXAttribute("UnkFloat08", data.UnkFloat08));
				Childs.Add(new JMXAttribute("UnkUInt07", data.UnkUInt07));
				Childs.Add(new JMXAttribute("UnkUInt08", data.UnkUInt08));
				Childs.Add(new JMXAttribute("UnkUInt09", data.UnkUInt09));
				Childs.Add(new JMXAttribute("UnkUInt10", data.UnkUInt10));
				Childs.Add(new JMXAttribute("UnkUShort01", data.UnkUShort01));
				Childs.Add(new JMXAttribute("UnkUShort02", data.UnkUShort02));
				Childs.Add(new JMXAttribute("UnkUShort03", data.UnkUShort03));
				Childs.Add(new JMXAttribute("UnkUShort04", data.UnkUShort04));
				Childs.Add(new JMXAttribute("UnkUShort05", data.UnkUShort05));
				Childs.Add(new JMXAttribute("UnkUShort06", data.UnkUShort06));
				Childs.Add(new JMXAttribute("UnkFloat09", data.UnkFloat09));
				Childs.Add(new JMXAttribute("UnkUInt11", data.UnkUInt11));
			}
			else if (Type == typeof(JMXVRES_0109.SystemModSet.IData768))
			{
				var data = Object is JMXVRES_0109.SystemModSet.IData768 ? Object as JMXVRES_0109.SystemModSet.IData768 : new JMXVRES_0109.SystemModSet.ModData();
				Childs.Add(new JMXAttribute("UnkUShort01", data.UnkUShort01));
				Childs.Add(new JMXAttribute("UnkUInt01", data.UnkUInt01));
				Childs.Add(new JMXAttribute("UnkUInt02", data.UnkUInt02));
				Childs.Add(new JMXAttribute("UnkUInt03", data.UnkUInt03));
				Childs.Add(new JMXAttribute("UnkUShort02", data.UnkUShort02));
				Childs.Add(new JMXAttribute("UnkUInt04", data.UnkUInt04));
				Childs.Add(new JMXAttribute("UnkUInt05", data.UnkUInt05));
				Childs.Add(new JMXAttribute("UnkUInt06", data.UnkUInt06));
				Childs.Add(new JMXAttribute("UnkUInt07", data.UnkUInt07));
				Childs.Add(new JMXAttribute("UnkUInt08", data.UnkUInt08));
				Childs.Add(new JMXAttribute("UnkUInt09", data.UnkUInt09));
			}
			// JMXVBMT 
			else if (Type == typeof(JMXVBMT_0102.Color4))
			{
				var data = Object is JMXVBMT_0102.Color4 ? Object as JMXVBMT_0102.Color4 : new JMXVBMT_0102.Color4();
				Childs.Add(new JMXAttribute("Red", data.Red));
				Childs.Add(new JMXAttribute("Green", data.Green));
				Childs.Add(new JMXAttribute("Blue", data.Blue));
				Childs.Add(new JMXAttribute("Alpha", data.Alpha));
			}
		}
		#endregion
	}
}
