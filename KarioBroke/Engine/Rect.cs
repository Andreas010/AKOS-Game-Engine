using System;

namespace KarioBroke.Engine
{
    public struct Rect
    {
        public float x;
        public float y;
        public float width;
        public float height;

        public Rect()
        {
            x = 0;
            y = 0;
            width = 0;
            height = 0;
        }

        public Rect(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public void SetCenter(float x, float y)
        {
            this.x = x - width / 2f;
            this.y = y - height / 2f;
        }
    }
}