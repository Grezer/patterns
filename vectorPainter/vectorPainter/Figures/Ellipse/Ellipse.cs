using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
