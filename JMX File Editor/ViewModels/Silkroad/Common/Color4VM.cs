using JMXFileEditor.Silkroad.Data.Common;
namespace JMXFileEditor.ViewModels.Silkroad.Common
{
	public class Color4VM : JMXStructure
	{
		public Color4VM(string Name, Color4 Color) : base(Name, true)
		{
			Childs.Add(new JMXAttribute("Red", Color.Red));
			Childs.Add(new JMXAttribute("Green", Color.Green));
			Childs.Add(new JMXAttribute("Blue", Color.Blue));
			Childs.Add(new JMXAttribute("Alpha", Color.Alpha));
		}
	}
}
