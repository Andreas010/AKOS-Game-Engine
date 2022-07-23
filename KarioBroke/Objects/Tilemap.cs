using KarioBroke.Components;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace KarioBroke.Objects
{
    public class Tilemap : IGameObject
    {
        private Texture2D tileset;
        private Rectangle[][] blockTable;
        private TileScreen[] tileScreens;
        private int animationIndex = 0;
        private double holder = 0.0;

        public void Start()
        {
            Texture2D level = Texture2D.FromFile(Game.graphics.GraphicsDevice, "Assets/Level1.png");
            Color[] colors = new Color[level.Width * level.Height];

            Color groundColor = new(0, 0, 0);
            Color questionBlockColor = new(255, 0, 0);
            Color brickBlockColor = new(127, 0, 0);

            level.GetData(colors);
            blockTable = new Rectangle[256][];

            blockTable[1] = new Rectangle[] { new Rectangle(0, 16, 16, 16) };
            blockTable[3] = new Rectangle[] { new Rectangle(17, 16, 16, 16) };
            blockTable[2] = new Rectangle[] { new Rectangle(298, 78, 16, 16), new Rectangle(298, 78, 16, 16), new Rectangle(298, 78, 16, 16), new Rectangle(298, 78, 16, 16), new Rectangle(315, 78, 16, 16), new Rectangle(332, 78, 16, 16), new Rectangle(315, 78, 16, 16) };

            tileScreens = new TileScreen[16];
            int off = 0;
            for(int i = 0; i < tileScreens.Length; i++)
            {
                TileScreen s = new();
                s.tiles = new byte[TileScreen.size];

                for(int y = 0; y < 18; y++)
                {
                    for(int x = 0; x < 32; x++)
                    {
                        byte idx = 0;
                        Color c = colors[x + off + (level.Height - 1 - y) * level.Width];
                        if (c == groundColor)
                            idx = 1;
                        else if (c == questionBlockColor)
                            idx = 2;
                        else if (c == brickBlockColor)
                            idx = 3;
                        s.tiles[x + y * 32] = idx;
                    }
                }
                off += 32;

                tileScreens[i] = s;
            }
        }

        public void LoadAssets(ContentManager content)
        {
            tileset = content.Load<Texture2D>("tileset/Tiles");
        }

        public void OnPhysicsUpdate()
        {

        }

        public void Update(double deltaTime, double time, KeyboardState keyState)
        {
            holder += deltaTime;
            if(holder >= 0.1)
            {
                holder -= 0.1;
                animationIndex++;
            }
        }

        public void Draw(SpriteBatch batch)
        {
            for(int i = 0; i < tileScreens.Length; i++)
            {
                int idx = 0;
                for (int y = -9; y < 9; y++)
                {
                    for (int x = -16; x < 16; x++)
                    {
                        byte d = tileScreens[i].tiles[idx];
                        idx++;
                        if (d == 0) continue;
                        Rectangle[] a = blockTable[d];
                        DrawTileAt(new Point(x + 1 + i * 32, y + 1), a[animationIndex % a.Length]);
                    }
                }
            }
        }

        void DrawTileAt(Point pos, Rectangle rect)
        {
            DrawUtility.Draw(tileset, pos.ToVector2() / 2f - Vector2.One / 4f, rect, Color.White);
        }
    }

    struct TileScreen
    {
        public const int size = 18 * 32;
        public byte[] tiles;
    }
}
