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

        /** Number of manipulator active point 
         * -1   Nothing selected
         *  0   Center of the figure (drag)
         *  1   Top left corner of the figure (resize)
         *  2   Top right corner of the figure (resize)
         *  3   Bottom right corner of the figure (resize)
         *  4   Bottom left corner of the figure (resize)
         */
        sbyte activePoint;

        public Figure Selected => selectedFigure;

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
            g.DrawRectangle(Pens.Red, xAxis - 5, yAxis - 5, width + 10, height + 10);

            g.FillRectangle(Brushes.Red, xAxis - 10, yAxis - 10, 10, 10);
            g.FillRectangle(Brushes.Red, xAxis - 10, yAxis + height, 10, 10);
            g.FillRectangle(Brushes.Red, xAxis + width, yAxis - 10, 10, 10);
            g.FillRectangle(Brushes.Red, xAxis + width, yAxis + height, 10, 10);
        }

        public void Drag(float dx, float dy)
        {
            switch (activePoint)
            {
                case 0:
                    this.Move(xAxis + dx, yAxis + dy);
                    break;

                case 1:
                    this.Move(xAxis + dx, yAxis + dy);
                    this.Resize(-dx + width, -dy + height);
                    break;

                case 2:
                    this.Move(xAxis, yAxis + dy);
                    this.Resize(dx + width, -dy + height);
                    break;

                case 3:
                    this.Resize(dx + width, dy + height);
                    break;

                case 4:
                    this.Move(xAxis + dx, yAxis);
                    this.Resize(-dx + width, dy + height);
                    break;

                default:
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

            // Top left corner of the figure
            if (Math.Abs(xTouch - xAxis) <= 10 && Math.Abs(yTouch - yAxis) <= 10)
            {
                activePoint = 1;
                return true;
            }

            // Top right corner of the figure
            if (Math.Abs(xTouch - xAxis - width) <= 10 && Math.Abs(yTouch - yAxis) <= 10)
            {
                activePoint = 2;
                return true;
            }

            // Bottom right corner of the figure
            if (Math.Abs(xTouch - xAxis - width) <= 10 && Math.Abs(yTouch - yAxis - height) <= 10)
            {
                activePoint = 3;
                return true;
            }

            // Bottom left corner of the figure
            if (Math.Abs(xTouch - xAxis) <= 10 && Math.Abs(yTouch - yAxis - height) <= 10)
            {
                activePoint = 4;
                return true;
            }

            // Center of the figure
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
