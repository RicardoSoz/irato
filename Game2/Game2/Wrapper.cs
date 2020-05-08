using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Game2
{
    enum Directions { STAND_RIGHT, RUN_RIGHT, JUMP, HIT, KICK }
    class Wrapper
    {
        Keys up, left, down, right, start;
        Rectangle pos;
        Vector2 jumpMeasure = new Vector2(30, 30);
        BasicAnimatedSprite runRight, jump, hit, kick;
        bool isPressed, hasJumped, isRunning;
        Point speed;
        Directions direction = new Directions();

        bool collision;

        public Wrapper(Rectangle pos, Point speed)
        {
            this.pos = pos;
            this.speed = speed;
            isPressed = false;
            direction = Directions.RUN_RIGHT;
            hasJumped = true;
            isRunning = false;
            collision = false;
        }

        public void ResetCollision()
        {
            collision = false;
        }

        public bool Collision(Rectangle check)
        {

            if (this.pos.Intersects(check) || collision)
            {
                collision = true;
            }
            return collision;
        }

        public void SetKeys(Keys up, Keys left, Keys down, Keys right, Keys start)
        {
            this.up = up;
            this.left = left;
            this.down = down;
            this.right = right;
            this.start = start;
        }

        public void LoadContent(ContentManager content)
        {
            jump = new BasicAnimatedSprite(pos);
            hit = new BasicAnimatedSprite(pos);
            kick = new BasicAnimatedSprite(pos);
            runRight = new BasicAnimatedSprite(pos);
            jump.LoadContent(content, "Andy/Jump", "Jump_f", 3);
            hit.LoadContent(content, "Andy/Hit", "Hit_f", 2);
            kick.LoadContent(content, "Andy/Duck", "Duck_f", 5);
            runRight.LoadContent(content, "Andy/RunRight", "run_f", 5);
        }

        public void Update(GameTime gameTime)
        {
            jump.Update(gameTime);
            hit.Update(gameTime);
            kick.Update(gameTime);
            runRight.Update(gameTime);

            pos = Rect;

            pos.Y += speed.Y;
            if (pos.Y >= Game1.ScreenHeight - Game1.size) pos.Y = Game1.ScreenHeight - Game1.size;
            if (pos.Y <= 0) pos.Y = 0;

            if (Keyboard.GetState().IsKeyDown(up) && hasJumped == false)
            {
                pos.Y -= (int) jumpMeasure.Y;
                speed.Y = (int)-jumpMeasure.Y/2;
                hasJumped = true;
                direction = Directions.JUMP;
                
                
            }

            if (hasJumped == true)
            {
                int i = 1;
                speed.Y += i ;
            }

            if (pos.Y >= Game1.ScreenHeight - Game1.size)
            {
                hasJumped = false;
            }

            if (hasJumped == false)
            {
                speed.Y = 0;
                direction = Directions.RUN_RIGHT;
            }

            if (Keyboard.GetState().IsKeyDown(down))
            {
                direction = Directions.KICK;
            }

            if (Keyboard.GetState().IsKeyDown(right))
            {
                isRunning = true;
                pos.X += speed.X;
                direction = Directions.RUN_RIGHT;
                if (pos.X + pos.Width >= runRight.Wnd().Width) pos.X = runRight.Wnd().Width - pos.Width;
            }

            if (isRunning == false)
            {
                pos.X -= speed.X/2;
                if (pos.X <= 50) pos.X = 50;
            }

            if (pos.X >= 50)
            {
                isRunning = false;
            }

            if (isPressed == false)
            {
                if (Keyboard.GetState().IsKeyDown(start))
                {
                    Stop();
                }
                isPressed = true;
            }
            if (Keyboard.GetState().IsKeyUp(start)) isPressed = false;

            Rect = pos;
        }

        public void Stop()
        {
            jump.Start();
            hit.Start();
            runRight.Start();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (direction == Directions.JUMP)
                jump.Draw(spriteBatch);
            if (direction == Directions.HIT)
                hit.Draw(spriteBatch);
            if (direction == Directions.KICK)
                kick.Draw(spriteBatch);
            if (direction == Directions.RUN_RIGHT)
                runRight.Draw(spriteBatch);
        }

        public Rectangle Rect
        {
            get { return jump.Pos; }
            set
            {
                jump.Pos = value;
                runRight.Pos = value;
                kick.Pos = value;
                hit.Pos = value;
            }
        }
    }
}
