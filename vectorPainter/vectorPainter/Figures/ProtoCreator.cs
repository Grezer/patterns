using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vectorPainter
{
    class ProtoCreator : FigureCreator
    {
        public Figure Prototype { get; set; }

        public override Figure CreateFigure()
        {
            return Prototype.Clone();
        }
    }
}
