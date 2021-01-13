using System.Collections.Generic;

namespace vectorPainter
{
    class FiguresDictionarySingleton
    {
        private static FiguresDictionarySingleton instance = new FiguresDictionarySingleton();

        public Dictionary<string, FigureCreator> figureCreators { get; }

        private FiguresDictionarySingleton()
        {
            figureCreators = new Dictionary<string, FigureCreator>
            {
                ["Rectangle"] = new RectangleCreator(),
                ["Ellipse"] = new EllipseCreator()
            };
        }

        public static FiguresDictionarySingleton GetInstance()
        {
            if (instance == null)
                instance = new FiguresDictionarySingleton();
            return instance;
        }
    }
}