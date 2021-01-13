using System.Collections.Generic;
using System.Drawing;

namespace vectorPainter
{
    class Picture
    {
        private List<Figure> listFigures = new List<Figure>();

        // Add new figure in listFigures
        public void Add(Figure newFigure)
        {
            if (newFigure == null) return;
            if (listFigures.Contains(newFigure)) return;
            listFigures.Add(newFigure);
        }

        // Clear graphics and draw all figures from listFigures
        public void Draw(Graphics g)
        {
            g.Clear(Color.White);
            foreach (var figure in listFigures)
                figure.Draw(g);
        }
    }
}
