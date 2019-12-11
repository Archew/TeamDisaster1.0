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
    enum Direction { Left, Right }
    

    class Player : GameObject
    {
        private Direction direction = Direction.Right;
        private float speed;
        public bool isGrounded = true;
        public bool isMoving = true;
        public string tag = "player";
        private Vector2 velocity;
        private float Scale = 0.4f;
        private bool shotBullet = false;
        

        //PLAYER MOVEMENT SPEED
        public Player()
        {
            speed = 200;
        }

        public Player(Vector2 position)
        {
            this.position = position;
        }
        //HER LOADES SPRITE OG SÆTTES ANIMATIONS HASTIGHED
        public override void LoadContent(ContentManager content)
        {
            //Instantiates the sprite array
            sprites = new Texture2D[10];

            //Loads all sprites into the array
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>(i + 1 + "Idle");


            }
            //Sets a default sprite
            sprite = sprites[0];
            fps = 5;

            this.origin = new Vector2(sprite.Width / 2, sprite.Height / 2);

            this.position = new Vector2(GameWorld.ScreenSize.X / 2, GameWorld.ScreenSize.Y - (sprite.Height / 2));

            spritesRun = new Texture2D[8];

            for (int i = 0; i < spritesRun.Length; i++)
            {
                spritesRun[i] = content.Load<Texture2D>(i + 1 + "Run");
            }


        }
        //HER ER UPDATE ALT HVAD DER SKAL TJEKKES KONSTANT KØRER HER
        public override GameObject Update(GameTime gameTime )
        {
           
            HandleInput();
            Move(gameTime);
            Animate(gameTime);
            Gravity(gameTime);

            if (this.shotBullet)
            {
                this.shotBullet = false;

                return new Bullet(new Vector2(this.position.X, this.position.Y));
            }
            return null; 
            
        }

        //private new void AnimateRun(GameTime gameTime)
        //{
        //    KeyboardState keyState = Keyboard.GetState();
        //    if (keyState.IsKeyDown(Keys.D))

        //    {//Adds time that has passed since last update
        //        timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

        //        //Calculates the curent index
        //        currentIndex = (int)(timeElapsed * fps);

        //        spriteRun = spritesRun[currentIndex];

        //        //Checks if we need to restart the animation
        //        if (currentIndex >= sprites.Length - 1)
        //        {
        //            //Resets the animation
        //            timeElapsed = 0;
        //            currentIndex = 0;

        //        }
        //    }
        //}

        //HER STYRE VI INPUT
        private void HandleInput()
        {
            isMoving = false;
            //Resets velocity
            //Makes sure that we will stop moving
            //When no keys are pressed
            //velocity = Vector2.Zero
            //Gravity();
            //Get the current keyboard state
            KeyboardState keyState = Keyboard.GetState();
            if (isMoving == false)
            {
                velocity = new Vector2(velocity.X - velocity.X, velocity.Y);
            }
            //If we press W
            //if (keyState.IsKeyDown(Keys.W)) //SKAL BRUGES HVIS VI TILFØJER STIGER
            //{
            //    //Move up
            //    velocity += new Vector2(0, -1);
            //    isMoving = true;

            //}
            if (keyState.IsKeyDown(Keys.A))
            {
                //Move left
                velocity += new Vector2(-1, 0);
                isMoving = true;
                this.direction = Direction.Left;
            }
            //if (keyState.IsKeyDown(Keys.S)) //SKAL BRUGES HVIS VI TILFØJER STIGER
            //{
            //    //Move down
            //    //velocity += new Vector2(0, 1);
            //    isMoving = true;
            //}
            if (keyState.IsKeyDown(Keys.D))
            {
                //Move right

                velocity += new Vector2(1, 0);
                isMoving = true;
                this.direction = Direction.Right;
            }
            if (keyState.IsKeyDown(Keys.W) && (isGrounded == true))
            {
                //Jump
                Jump();
                isMoving = true;
            }
            if (keyState.IsKeyDown(Keys.Space))
            {
                this.shotBullet = true;
                
            }
            
            //If pressed a key, then we need to normalize the vector
            //If we don't do this we will move faster 
            //while pressing two keys at once
            //if (velocity != Vector2.Zero)
            //{
            //    velocity.Normalize();
            //}
        }
        //LAD DETTE VÆRE
        private void Move(GameTime gameTime)
        {
            //Calculates deltaTime based on t he gameTime
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Moves the player based on the result from HandleInput, speed and deltatime
            position += ((velocity * speed) * deltaTime);
        }
        private void Jump()
        {
            isGrounded = false;
            velocity = new Vector2(velocity.X, -2);
        }
        private void Gravity(GameTime gametime)
        {
            if (isGrounded == false)
            {
                velocity += new Vector2(0, 2 * (float)gametime.ElapsedGameTime.TotalSeconds);
            }
            else
            {
                velocity = Vector2.Zero;
            }//velocity += new Vector2(0, 2 * (float)gametime.ElapsedGameTime.TotalSeconds);

        }
        public override void IsColliding(GameObject other)
        {
            //Do something when we collide with another object
            if (other is Platform)
            {
                Platform p = (Platform)other;
                if (p.tag == "Terrain")
                {
                    //Bottom Player Collision
                    if (p.Position.Y - p.Sprite.Height / 2 < this.position.Y + sprite.Height / 2 && p.Position.Y - p.Sprite.Height / 3 > this.position.Y + sprite.Height / 2 && p.Position.X - p.Sprite.Width / 3 * 1.40 < this.position.X + sprite.Width / 2 && p.Position.X + p.Sprite.Width / 3 * 1.40 > this.position.X - sprite.Width / 2)
                    {
                        int height = CollisionBox.Bottom - other.CollisionBox.Top;
                        position.Y -= height;

                        isGrounded = true;

                    }
                    //Top Player Collision
                    if (p.Position.Y + p.Sprite.Height / 2 > this.position.Y - sprite.Height / 2 && p.Position.X - p.Sprite.Width / 3 * 1.40 < this.position.X + sprite.Width / 2 && p.Position.X + p.Sprite.Width / 3 * 1.40 > this.position.X - sprite.Width / 2 && isGrounded == false)
                    {
                        velocity = new Vector2(velocity.X, velocity.Y - velocity.Y * 2);
                    }
                    //Right Player Collision
                    if (p.Position.X - sprite.Width / 2 >= this.position.X + sprite.Width / 2 && p.Position.X > this.position.X + sprite.Width / 2 && p.Position.Y + p.Sprite.Height / 3 > this.position.Y - sprite.Height / 2 && isGrounded == false)
                    {
                        int width = CollisionBox.Right - other.CollisionBox.Left;
                        position.X -= width;
                    }
                    //Left Player Collision
                    if (p.Position.X + sprite.Width / 2 <= this.position.X - sprite.Width / 2 && p.Position.X < this.position.X - sprite.Width / 2 && p.Position.Y + p.Sprite.Height / 3 > this.position.Y - sprite.Height / 2 && isGrounded == false)
                    {
                        int width = CollisionBox.Left - other.CollisionBox.Right;
                        position.X -= width;
                    }

                }

            }
            else if (other is Bullet) {}
            else
            {
                isGrounded = false;
            }
        }
        public override void OnCollision(GameObject other)
        {

        }
    }
}
