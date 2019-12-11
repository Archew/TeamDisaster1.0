using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamDisaster1
{
    

    abstract class GameObject
    {

        protected Texture2D sprite;

        protected Vector2 position;

        protected Texture2D[] sprites;
        protected Texture2D spriteRun;
        protected Texture2D[] spritesRun;
        float Scale = 0.4f;

        protected int fps;

        public bool pendingDestruction = false; 
        public float timeElapsed;

        public int currentIndex;

        protected Vector2 origin;
        public Rectangle rectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, sprite.Width, sprite.Height); }
        }

        public Vector2 Position
        {
            get { return position; }

        }
        public Texture2D Sprite
        {
            get
        {
            return this.sprite;
        }
            set
        {
            this.sprite = value;
        }

}
        


public abstract void LoadContent(ContentManager content);

        public abstract GameObject Update(GameTime gameTime);

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(sprite, position,null, Color.White, 0, origin, 1, SpriteEffects.None, 0);
        }

        protected void Animate(GameTime gameTime)
        {
            //Adds time that has passed since last update
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Calculates the curent index
            currentIndex = (int)(timeElapsed * fps);

            sprite = sprites[currentIndex];

            //Checks if we need to restart the animation
            if (currentIndex >= sprites.Length - 1)
            {
                //Resets the animation
                timeElapsed = 0;
                currentIndex = 0;
            }
        }
        public virtual Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)position.X - (sprite.Width/2),
                (int)position.Y - (sprite.Height / 2), sprite.Width, sprite.Height);
            }
        }

        public object Rectangle { get; internal set; }

        /// <summary>
        /// Is executed whenever a collision occurs
        /// </summary>
        /// <param name="other">The object we collided with</param>
        public abstract void OnCollision(GameObject other);
        public abstract void IsColliding(GameObject other);
        /// <summary>
        /// Checks if this GameObject has collided with another GameObject
        /// </summary>
        /// <param name="other">The object we collided with</param>
        public void CheckCollision(GameObject other)
        {
            if (CollisionBox.Intersects(other.CollisionBox))
            {
                OnCollision(other);
                IsColliding(other);
            }
     
        }

        //protected void AnimateRun(GameTime gameTime)
        //{
        //    //Adds time that has passed since last update
        //    timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

        //    //Calculates the curent index
        //    currentIndex = (int)(timeElapsed * fps);

        //    spriteRun = spritesRun[currentIndex];

        //    //Checks if we need to restart the animation
        //    if (currentIndex >= sprites.Length - 1)
        //    {
        //        //Resets the animation
        //        timeElapsed = 0;
        //        currentIndex = 0;
        //    }
        //}
    }


}
