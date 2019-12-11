using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TeamDisaster1
{
    class BackgroundSprites
    {
      //Creating Backgrounds.
      //defining size of sprite
      public Rectangle Size;
        public Vector2 position;
      //Size of sprites. defined by a float.
      public float Scale = -1.0f;
      private Texture2D mSpriteTexture;
        internal Vector2 Position;

        //Load background content through pipeline tool.
        public void LoadContent(ContentManager content, string background_jungle_master2_1500)
        {
            mSpriteTexture = content.Load<Texture2D>(background_jungle_master2_1500);
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
        }

        //Draw Background.
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(mSpriteTexture, position, new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height), Color.White, 0.0f, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

    }
    
   
}   






