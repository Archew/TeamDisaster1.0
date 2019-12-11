using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamDisaster1
{
    class Camera
    {

        public Matrix Transform { get; private set; }
        public void Follow( Player target)
        {
            var position = Matrix.CreateTranslation(
                -target.Position.X - (target.rectangle.Width / 2),
                -0 - (GameWorld.screenHeight+ 50),
                0);
            //1200
            var offset = Matrix.CreateTranslation(
                    GameWorld.screenWidth / 2,
                    GameWorld.screenHeight * 1 -60, 0);

            Transform = position * offset;
        }
    }
}
