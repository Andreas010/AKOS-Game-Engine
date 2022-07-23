using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarioBroke.Components
{
    public interface IGameObject
    {
        public void LoadAssets(ContentManager content);
        public void Start();
        public void Update(double deltaTime, double time, KeyboardState keyState);
        public void OnPhysicsUpdate();
        public void Draw(SpriteBatch batch);
    }
}