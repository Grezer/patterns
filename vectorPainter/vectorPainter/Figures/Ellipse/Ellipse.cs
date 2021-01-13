using System.Drawing;

namespace vectorPainter
{
    public class Ellipse : Figure
    {
        // Draw ellipse method
        public override void Draw(Graphics g)
        {
            g.DrawEllipse(Pens.Navy, xAxis, yAxis, width, height);
        }
    }
}
