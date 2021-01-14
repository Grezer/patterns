using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vectorPainter
{
    class Manipulator : Figure
    {
        private Figure selectedFigure;

        // TODO: refactor this
        int activePoint;

        public Figure Selected { get { return selectedFigure; } }

        // Attach manipulator to figure
        public void Attach(Figure figure)
        {
            selectedFigure = figure;
            if (selectedFigure != null)
            {
                this.Move(figure.xAxis, figure.yAxis);
                this.Resize(figure.width, figure.height);
            }
        }

        // Draw manipulator
        public override void Draw(Graphics g)
        {
            if (selectedFigure == null) return;
            g.DrawRectangle(Pens.Red, xAxis - 2, yAxis - 2, width + 4, height + 4);
        }

        public void Drag(float dx, float dy)
        {
            switch (activePoint)
            {
                case 0:
                    this.Move(xAxis + dx, yAxis + dy);
                    break;
            }
        }

        public override bool Touch(float xTouch, float yTouch)
        {
            if (selectedFigure == null)
            {
                activePoint = -1;
                return false;
            }

            if (Math.Abs(xTouch - xAxis) <= 4 && Math.Abs(yTouch - yAxis) <= 4)
            {
                activePoint = 1;
                return true;
            }

            if (Math.Abs(xTouch - xAxis - width) <= 4 && Math.Abs(yTouch - yAxis) <= 4)
            {
                activePoint = 2;
                return true;
            }

            if (Math.Abs(xTouch - xAxis - width) <= 4 && Math.Abs(yTouch - yAxis - height) <= 4)
            {
                activePoint = 3;
                return true;
            }

            if (Math.Abs(xTouch - xAxis) <= 4 && Math.Abs(yTouch - yAxis - height) <= 4)
            {
                activePoint = 4;
                return true;
            }

            if (selectedFigure.Touch(xTouch, yTouch))
            {
                activePoint = 0;
                return true;
            }

            activePoint = -1;
            return false;
        }

        public void UpdateFigure()
        {
            if (selectedFigure != null)
            {
                selectedFigure.Move(this.xAxis, this.yAxis);
                selectedFigure.Resize(this.width, this.height);
            }
        }
    }
}
