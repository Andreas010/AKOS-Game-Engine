using System;
using System.Collections.Generic;
using KarioBroke.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KarioBroke.Objects;
using KarioBroke.Engine;

namespace KarioBroke
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Point screenSize = new(1280, 720);
        private float aspect;
        public static Point renderSize;
        private double curPhysicsTime = 0.0;
        public const double maxPhysicsTime = 1.0 / 50.0;
        public const double squareMaxPhysicsTime = maxPhysicsTime * maxPhysicsTime;

        private List<IGameObject> objects;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);

            Window.Title = "Kario Broke";
            Window.AllowUserResizing = false;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            objects = new();
            objects.Add(new Player());
            objects.Add(new Tilemap());
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 60.0);
            IsFixedTimeStep = true;
            graphics.SynchronizeWithVerticalRetrace = false;

            screenSize.X = graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width;
            screenSize.Y = graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height;

            graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            aspect = (float)screenSize.X / screenSize.Y;
            renderSize = new(512, (int)(512 / aspect));

            for (int i = 0; i < objects.Count; i++)
                objects[i].Start();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            DrawUtility.batch = spriteBatch;

            for (int i = 0; i < objects.Count; i++)
                objects[i].LoadAssets(Content);

            // TODO: use this.Content to load your game content here
        }

        // TODO: Add pastKeyState
        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            curPhysicsTime += gameTime.ElapsedGameTime.TotalSeconds;
            while (curPhysicsTime >= maxPhysicsTime)
            {
                curPhysicsTime -= maxPhysicsTime;
                for (int i = 0; i < objects.Count; i++)
                    objects[i].OnPhysicsUpdate();
            }

            for (int i = 0; i < objects.Count; i++)
                objects[i].Update(gameTime.ElapsedGameTime.TotalSeconds, gameTime.TotalGameTime.TotalSeconds, state);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            RenderTarget2D target = new(GraphicsDevice, renderSize.X, renderSize.Y);
            GraphicsDevice.SetRenderTarget(target);
            GraphicsDevice.Clear(new Color(148, 148, 255));
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            // TODO: Add your drawing code here

            for (int i = 0; i < objects.Count; i++)
                objects[i].Draw(spriteBatch);

            spriteBatch.End();
            GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);
            spriteBatch.Draw(target, new Rectangle(0, 0, screenSize.X, screenSize.Y), Color.White);
            spriteBatch.End();

            target.Dispose();
            base.Draw(gameTime);
        }
    }
}
