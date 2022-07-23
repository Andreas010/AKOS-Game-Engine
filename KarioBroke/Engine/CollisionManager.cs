using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace KarioBroke.Engine
{
    public class CollisionManager
    {
        public CollisionManager()
        {
            // (-15,-8) -> (496,9)
        }

        public static bool AABB(Rect rect1, Rect rect2)
        {
            return rect1.x < rect2.x + rect2.width &&
                rect1.x + rect1.width > rect2.x &&
                rect1.y < rect2.y + rect2.height &&
                rect1.height + rect1.y > rect2.y;
        }
    }
}
