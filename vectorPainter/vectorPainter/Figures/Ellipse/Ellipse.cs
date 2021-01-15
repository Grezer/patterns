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

        public override Figure Clone()
        {
            Ellipse clonedFigure = new Ellipse();
            clonedFigure.Move(this.xAxis, this.yAxis);
            clonedFigure.Resize(this.width, this.height);
            return clonedFigure;
        }
    }
}
