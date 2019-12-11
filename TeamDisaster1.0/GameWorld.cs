using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TeamDisaster1;

namespace TeamDisaster1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {

        public static int screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        public static int screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;

        private List<GameObject> gameObjects = new List<GameObject>();
        private static Vector2 screenSize;
        private BackgroundSprites mBackgroundOne;

        private Texture2D collisionTexture;
        private Camera camera;
        private Player player;


        public static Vector2 ScreenSize
        {
            get
            {
                return screenSize;
            }

            set
            {
                screenSize = value;
            }
        }

        public GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);

            screenSize = new Vector2(graphics.PreferredBackBufferWidth =screenWidth, graphics.PreferredBackBufferHeight =screenHeight);

            Content.RootDirectory = "Content";

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            screenHeight = graphics.PreferredBackBufferHeight;
            screenWidth = graphics.PreferredBackBufferWidth;
            player = new Player();
            gameObjects.Add(player);
            //Starting Run way
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 2, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 3, screenHeight - 40)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 4, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 5, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 6, screenHeight - 40)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 6, screenHeight - 130)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 7, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 8, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 9, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 10, screenHeight + 50)));
            //Holes in the ground
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 12, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 13, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 15, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 16, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 18, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 20, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 22, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 23, screenHeight + 50)));
            //The stairs
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 24, screenHeight - 40)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 25, screenHeight - 130)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 26, screenHeight - 220)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 27, screenHeight - 310)));
            //The End
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 30, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 31, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 32, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 33, screenHeight + 50)));
            gameObjects.Add(new Platform(new Vector2(screenWidth / 2 + 350 * 34, screenHeight - 40)));




            //Initialization of Background.
            mBackgroundOne = new BackgroundSprites();
            mBackgroundOne.Scale = 1.3f;




            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            camera = new Camera();
            //LoadContent Background.
            mBackgroundOne.LoadContent(Content, "background_jungle_master2_15001");
            mBackgroundOne.Position = new Vector2(0, 0);

            collisionTexture = Content.Load<Texture2D>("CollisionTexture");





            //LoadContent GameObjects.
            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(Content);
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {


            // TODO: Add your update logic here
            //foreach (GameObject gameObject in gameObjects)
            //{
            //    gameObject.Update(gameTime);
            //}
            List<GameObject> bullets = new List<GameObject>();
            List<int> pendingDestruction = new List<int>();
            var i = 0;
            foreach (var gameObject in gameObjects)
            {

                if (gameObject.pendingDestruction) pendingDestruction.Add(i - pendingDestruction.Count);
                var newObject = gameObject.Update(gameTime);
                if (newObject != null) bullets.Add(newObject);
                i++;
            }
             
            camera.Follow(player);


            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);
                foreach (GameObject other in gameObjects)
                {
                    go.CheckCollision(other);
                }
            }

            foreach (var bullet in bullets)
            {
                bullet.LoadContent(Content);
                gameObjects.Add(bullet);
            }

            foreach (var j in pendingDestruction)
            {
                gameObjects.RemoveAt(j);
            }

            base.Update(gameTime);
        }



        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            
            mBackgroundOne.Draw(spriteBatch);

            spriteBatch.End();


            // TODO: Add your drawing code here
            spriteBatch.Begin(transformMatrix: camera.Transform);



            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(spriteBatch);

                DrawCollisionBox(gameObject);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
        private void DrawCollisionBox(GameObject go)
        {
            Rectangle collisionBox = go.CollisionBox;
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            spriteBatch.Draw(collisionTexture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }
    }
}
