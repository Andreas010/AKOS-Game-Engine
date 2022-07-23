using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using KarioBroke.Components;
using KarioBroke.Engine;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace KarioBroke.Objects
{
    public class Player : Transform, IGameObject
    {
        private Texture2D tex;

        public void LoadAssets(ContentManager content)
        {
            tex = content.Load<Texture2D>("ball");
        }

        public void Start()
        {
            Camera.position = Vector2.Zero;
        }

        public void OnPhysicsUpdate()
        {
            position += velocity;
            velocity *= 0.9f;
        }

        public void Update(double deltaTime, double time, KeyboardState keyboardState)
        {
            if (keyboardState.IsKeyDown(Keys.D))
                velocity.X += 1f * (float)deltaTime;
            if (keyboardState.IsKeyDown(Keys.A))
                velocity.X -= 1f * (float)deltaTime;

            Camera.position.X = position.X;
        }
        
        public void Draw(SpriteBatch batch)
        {
            DrawUtility.Draw(tex, position, Color.White);
        }
    }
}