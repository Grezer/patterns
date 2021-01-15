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

        public override Figure Clone()
        {
            Rectangle clonedFigure = new Rectangle();
            clonedFigure.Move(this.xAxis, this.yAxis);
            clonedFigure.Resize(this.width, this.height);
            return clonedFigure;
        }
    }
}
