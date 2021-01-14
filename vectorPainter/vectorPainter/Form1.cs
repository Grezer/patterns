using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace vectorPainter
{


    public partial class Form1 : Form
    {
        FigureCreator currentCreator = null;
        Dictionary<string, FigureCreator> figureCreators;
        private Picture currentCanvas = new Picture();

        float oldX, oldY;

        public Form1()
        {
            InitializeComponent();
            
            // Get dictionary of figure creators
            FiguresDictionarySingleton figuresDictionarySingleton = FiguresDictionarySingleton.GetInstance();
            figureCreators = figuresDictionarySingleton.figureCreators;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Figure newFigure = null;

            oldX = e.X;
            oldY = e.Y;

            if (currentCreator != null)
            {
                newFigure = currentCreator.CreateFigure();
                newFigure.Move(e.X, e.Y);
                currentCanvas.Add(newFigure);
            }
            else
            {
                Figure selectedFigure = currentCanvas.Select(e.X, e.Y);
                label1.Text = (selectedFigure != null).ToString();
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


        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                currentCanvas.FigureManipulator.Drag(e.X - oldX, e.Y - oldY);
                currentCanvas.FigureManipulator.UpdateFigure();
                Refresh();
            }
            oldX = e.X;
            oldY = e.Y;
        }
    }
}
