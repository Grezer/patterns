using System.Drawing;

namespace vectorPainter
{
    public abstract class Figure
    {
        // Basic figure parameters
        private float private_xAxis;
        private float private_yAxis;
        private float private_width;
        private float private_height;


        // maybe rewrite this to public float xAxis { get { return private_xAxis; } } ???
        public float xAxis => private_xAxis;
        public float yAxis => private_yAxis;
        public float width => private_width;
        public float height => private_height;


        // Move figure => change the private values ​​of the x and y axes
        public virtual void Move(float new_xAxis, float new_yAxis)
        {
            private_xAxis = new_xAxis;
            private_yAxis = new_yAxis;
        }


        // Resize figure => change the private values ​​of the width and height
        public virtual void Resize(float new_width, float new_height)
        {
            private_width = new_width;
            private_height = new_height;
        }

        // Draw figure => abstract method that inheritors must implement 
        public abstract void Draw(Graphics g);

        // Return true if input coords get inside the figure
        public virtual bool Touch(float xTouch, float yTouch)
        {
            return     xTouch >= private_xAxis
                    && xTouch <= private_xAxis + private_width
                    && yTouch >= private_yAxis
                    && yTouch <= private_yAxis + private_height;
        }
    }
}
