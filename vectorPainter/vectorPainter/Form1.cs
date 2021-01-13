using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace vectorPainter
{


    public partial class Form1 : Form
    {
        // bool shiftPressed = false;
        FigureCreator currentCreator = null;
        Dictionary<string, FigureCreator> figureCreators = new Dictionary<string, FigureCreator>();
        private Picture currentCanvas = new Picture();

        public Form1()
        {
            InitializeComponent();
            figureCreators["Rectangle"] = new RectangleCreator();
            figureCreators["Ellipse"] = new EllipseCreator();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Figure newFigure = null;
            if (currentCreator != null)
            {
                newFigure = currentCreator.CreateFigure();
                newFigure.Move(e.X, e.Y);
                currentCanvas.Add(newFigure);
            }
            Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            currentCanvas.Draw(e.Graphics);
        }

        private void chooseFigureCreator(object sender, EventArgs e)
        {
             string figureName = sender.ToString();
             currentCreator = GetFigureCreatorByName(figureName);
        }

        private FigureCreator GetFigureCreatorByName(string figureName)
        {
            if (figureCreators.Keys.Contains(figureName))
                return figureCreators[figureName];
            return null;
        }
    }
}
