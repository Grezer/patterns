using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace vectorPainter
{


    public partial class Form1 : Form
    {
        // 
        FigureCreator currentCreator = null;
        Dictionary<string, FigureCreator> figureCreators;
        private Picture currentCanvas = new Picture();
        float oldX, oldY;
        sbyte numberOfCustomFigure = 1;
        FiguresDictionarySingleton figuresDictionarySingleton = FiguresDictionarySingleton.GetInstance();
        

        public Form1()
        {
            InitializeComponent();
            
            // Get dictionary of figure creators
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
                currentCanvas.Select(e.X, e.Y);
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

            // Remove manipulator selection
            currentCanvas.RemoveSelection();
            Refresh();
        }
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
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

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentCanvas.FigureManipulator.Selected == null) return;

            ToolStripMenuItem newFigureButton = new ToolStripMenuItem();
            newFigureButton.DisplayStyle = ToolStripItemDisplayStyle.Text;
            newFigureButton.Click += chooseFigureCreator;

            string newFigureName = "Fig." + numberOfCustomFigure++.ToString();
            newFigureButton.Text = newFigureName;

            ProtoCreator cr = new ProtoCreator();
            cr.Prototype = currentCanvas.FigureManipulator.Selected.Clone();
            figureCreators = figuresDictionarySingleton.Add(newFigureName, cr);

            customFiguresToolStripMenuItem.DropDownItems.Add(newFigureButton);
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
