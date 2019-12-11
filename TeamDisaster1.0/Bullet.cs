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
    class Bullet : GameObject
    {
        private float lastTime = 0;  
        private float firstTime;     // sætter første målte tid
        private float speed = 800;   // sætter hastighed på bullet
        
        private Vector2 velocity = new Vector2(1, 0);   // sætter vinkel af skud
        
        public Bullet(Vector2 position)
        {

            this.position = position;
        }
        
        public override GameObject Update(GameTime gameTime)
        {
            var currentTime = (float)gameTime.TotalGameTime.TotalSeconds;

            if (lastTime != 0)
            {
                
                var deltaTime = currentTime - lastTime;
                this.position += velocity * speed * deltaTime;  // sætter projektil vinkel og hastighed
            }
            else firstTime = currentTime;

            lastTime = currentTime;
            if (firstTime + 3 < currentTime) { this.pendingDestruction = true; } // efter 3 sek, projektil = pendingDestruction
            return null;
        }


        public override void LoadContent(ContentManager content)
        {
            this.sprite = content.Load<Texture2D>("Bullet");  //henter sprite texture fra GameObject klassen + loader bullet
        }
        public override void IsColliding(GameObject other)
        {
           
        }
        public override void OnCollision(GameObject other)
        {
            //tilføj terrain og enemies senere
            
        }
    }
    
}
