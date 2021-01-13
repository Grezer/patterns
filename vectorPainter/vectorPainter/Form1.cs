using System;
using System.Windows.Forms;

namespace vectorPainter
{


    public partial class Form1 : Form
    {
        // bool shiftPressed = false;
        FigureCreator currentCreator = null;
        // Dictionary<string, Creator>
        private Picture picture = new Picture();

        public Form1()
        {
            InitializeComponent();
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentCreator = new RectangleCreator();
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentCreator = new EllipseCreator();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Figure newFigure = null;
            if (currentCreator != null)
            {
                newFigure = currentCreator.CreateFigure();
                newFigure.Move(e.X, e.Y);
                picture.Add(newFigure);
            }
            Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            picture.Draw(e.Graphics);
        }
    }
}
