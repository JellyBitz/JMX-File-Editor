namespace JMXFileEditor.Silkroad.Mathematics
{
    public class Color3
    {
        #region Public Properties
        public float Red { get; set; }
        public float Green { get; set; }
        public float Blue { get; set; }
        #endregion

        #region Constructor
        public Color3()
        {
        }
        public Color3(float red, float green, float blue)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }
        #endregion
    }
}