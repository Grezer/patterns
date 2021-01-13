namespace vectorPainter
{
    class RectangleCreator : FigureCreator
    {
        public override Figure CreateFigure()
        {
            Figure createdFigure = new Rectangle();
            createdFigure.Resize(60, 49);
            return createdFigure;
        }
    }
}
