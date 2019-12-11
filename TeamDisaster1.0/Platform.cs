using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamDisaster1
{
    class Platform : GameObject
    {
        public bool isGrounded = true;
        public bool isMoving = true;
        public string tag = "Terrain";
        

        public Platform(Vector2 position)
        {
            this.position = position;
        }


        //HER LOADES SPRITE OG SÆTTES ANIMATIONS HASTIGHED
        public override void LoadContent(ContentManager content)
        {
            //Instantiates the sprite array
            sprites = new Texture2D[1];

            //Loads all sprites into the array
            sprites[0] = content.Load<Texture2D>("Platform[PH]");
            sprite = sprites[0];
                
            this.origin = new Vector2(sprite.Width / 2, sprite.Height / 2);

            //this.position = new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y - (sprite.Height / 2));

        }
        public override void OnCollision(GameObject other)
        {
            //Do something when we collide with another object
        }
        public override void IsColliding(GameObject other)
        {

        }
        //HER ER UPDATE ALT HVAD DER SKAL TJEKKES KONSTANT KØRER HER
        public override GameObject Update(GameTime gameTime)
        {
            Animate(gameTime);
            return null;
        }
    }
}
