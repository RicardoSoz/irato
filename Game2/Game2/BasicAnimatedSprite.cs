using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Collections;

namespace Game2
{
    class BasicAnimatedSprite
    {
        Rectangle pos;
        Texture2D image;
        ArrayList imagesArrayList;
        static Rectangle wnd;

        double timer;
        double timePerFrame = .08f;
        int frameCount, currentFrame;
        bool isAnimated;

        bool collision;

        public BasicAnimatedSprite(Rectangle pos)
        {
            imagesArrayList = new ArrayList();
            this.pos = pos;
            isAnimated = true;
        }

        public void LoadContent(ContentManager content, string folder, string texture, int frameCount)
        {
            this.frameCount = frameCount;
            for (int i = 1; i <= frameCount; i++)
            {
                image = content.Load<Texture2D>(folder + "/" + texture + i.ToString("00"));
                imagesArrayList.Add(image);
            }
        }
        public void Update(GameTime gameTime)
        {
            if (isAnimated)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (timer >= timePerFrame)
                {
                    currentFrame = (currentFrame + 1) % frameCount;
                    timer = timer - timePerFrame;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw((Texture2D)imagesArrayList[currentFrame], pos, Color.White);
            spriteBatch.End();

        }

        public void ResetCollision()
        {
            collision = false;
        }

        public bool Collision(Rectangle check)
        {

            if (this.Pos.Intersects(check) || collision)
            {
                collision = true;
            }
            return collision;
        }

        public static void SetWindoeSize(Rectangle windowSize)
        {
            wnd = windowSize;
        }

        public Rectangle Wnd()
        {
            return wnd;
        }

        public Rectangle Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public void Start()
        {
            if (isAnimated) isAnimated = false;
            else isAnimated = true;
        }

    }
}
