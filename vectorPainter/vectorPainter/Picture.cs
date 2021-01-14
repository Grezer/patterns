using System.Collections.Generic;
using System.Drawing;

namespace vectorPainter
{
    class Picture
    {
        private List<Figure> listFigures = new List<Figure>();

        private Manipulator figureManipulator = new Manipulator();
        public Manipulator FigureManipulator => figureManipulator;

        // Add new figure in listFigures
        public void Add(Figure newFigure)
        {
            if (newFigure == null) 
                return;

            if (listFigures.Contains(newFigure)) 
                return;

            listFigures.Add(newFigure);
        }

        // Draw manipulator (if is exist) and all figures from listFigures
        public void Draw(Graphics g)
        {
            g.Clear(Color.White);
            figureManipulator.Draw(g);
            foreach (var figure in listFigures)
                figure.Draw(g);
        }

        // Attach manipulator to the figure
        public Figure Select(float xAxis, float yAxis)
        {
            Figure selectedFigure = null;

            if (figureManipulator.Touch(xAxis, yAxis))
                return figureManipulator.Selected;

            foreach (var figure in listFigures)            
                if (figure.Touch(xAxis, yAxis))                
                    selectedFigure = figure;
            figureManipulator.Attach(selectedFigure);
            return selectedFigure;
        }
    }
}
