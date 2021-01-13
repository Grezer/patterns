namespace vectorPainter
{
    class EllipseCreator : FigureCreator
    {
        public override Figure CreateFigure()
        {
            Figure createdFigure = new Ellipse();
            createdFigure.Resize(60, 49);
            return createdFigure;
        }
    }
}
