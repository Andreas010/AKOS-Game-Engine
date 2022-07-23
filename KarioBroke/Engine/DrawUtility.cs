using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarioBroke
{
    public static class DrawUtility
    {
        public static SpriteBatch batch;
        private readonly static Vector2 half = Vector2.One / 2f;

        public static void Draw(Texture2D tex, Vector2 position, Color c)
        {
            Draw(tex, position, half, c);
        }

        public static void Draw(Texture2D tex, Vector2 position, Vector2 pivot, Color c)
        {
            Draw(tex, position, pivot, null, c);
        }

        public static void Draw(Texture2D tex, Vector2 position, Rectangle rect, Color c)
        {
            Draw(tex, position, half, rect, c);
        }

        public static void Draw(Texture2D tex, Vector2 position, Vector2 pivot, Rectangle? rect, Color c)
        {
            batch.Draw(tex, Camera.WorldToPixel(position, pivot, rect), rect, c);
        }
    }
}