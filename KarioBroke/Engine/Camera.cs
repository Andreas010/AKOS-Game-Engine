using Microsoft.Xna.Framework;

namespace KarioBroke
{
    public static class Camera
    {
        public static Vector2 position;

        public static Vector2 WorldToPixel(Vector2 world, Vector2 pivot, Rectangle? rect)
        {
            world = new Vector2(world.X, world.Y * -1);
            Vector2 center = Game.renderSize.ToVector2() / 2f;
            Vector2 camPosition = new(position.X * -32, position.Y * 32);
            if(rect.HasValue)
                return world * 32 - new Vector2(pivot.X * rect.Value.Width, pivot.Y * rect.Value.Height) + camPosition + center;
            return world * 32 - pivot * 32 + camPosition + center;
        }
    }
}