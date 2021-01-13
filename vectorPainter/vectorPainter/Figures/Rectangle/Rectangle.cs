using System.Drawing;

namespace vectorPainter
{
    public class Rectangle : Figure
    {
        // Draw rectangle method
        public override void Draw(Graphics g)
        {
            g.DrawRectangle(Pens.Black, xAxis, yAxis, width, height);
        }
    }
}
