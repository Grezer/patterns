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
        private Picture currentCanvas = new Picture();
        Dictionary<string, FigureCreator> figureCreators;

        public Form1()
        {
            InitializeComponent();
            FiguresDictionarySingleton figuresDictionarySingleton = FiguresDictionarySingleton.GetInstance();
            figureCreators = figuresDictionarySingleton.figureCreators;
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
