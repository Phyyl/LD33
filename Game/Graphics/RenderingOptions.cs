using OpenTK;
using System.Drawing;

namespace PhyylsGameLibrary.Graphics
{
	public class RenderingOptions
	{
        public Vector2 Origin { get; set; }
        public Vector2 Position { get; set; }
        public Color Color { get; set; }
		public float Scale { get; set; }

        public float AngleDegrees { get; set; }
		public float AngleRadians { get { return MathHelper.DegreesToRadians(AngleDegrees); } set { AngleDegrees = MathHelper.RadiansToDegrees(value); } }

		public RenderingOptions()
		{
			Color = Color.White;
			Scale = 1;
		}
	}
}
