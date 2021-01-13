using System;
using System.Windows.Forms;

namespace vectorPainter
{


    public partial class Form1 : Form
    {
        int mode = 0;
        bool shiftPressed = false;
        // Creator current = null;
        // Dictionary<string, Creator>
        private Picture picture = new Picture();

        public Form1()
        {
            InitializeComponent();
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = 1;
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mode = 2;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            Figure newFigure = null;

            switch (mode)
            {
                case 1:
                    newFigure = new Rectangle();
                    newFigure.Resize(50, 40);
                    newFigure.Move(e.X, e.Y);
                    newFigure.Draw(panel1.CreateGraphics());
                    break;

                case 2:
                    newFigure = new Ellipse();
                    newFigure.Resize(50, 40);
                    newFigure.Move(e.X, e.Y);
                    newFigure.Draw(panel1.CreateGraphics());
                    break;

                default:
                    break;
            }

            picture.Add(newFigure);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            picture.Draw(e.Graphics);
        }
    }
}
