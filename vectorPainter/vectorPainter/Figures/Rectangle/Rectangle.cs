using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
