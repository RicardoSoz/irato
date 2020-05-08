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
    class BasicSprite
    {
        Texture2D image;
        Rectangle pos;
        Color color;

        public BasicSprite(int x, int y, int width, int height)
        {
            pos = new Rectangle(x, y, width, height);
        }
        public Rectangle Pos
        {
            set
            {
                pos = value;
            }
            get
            {
                return pos;
            }
        }
        public void SetColor(Color color)
        {
            this.color = color;
        }



        public void LoadContent(ContentManager Content, String name)
        {
            image = Content.Load<Texture2D>(name);
            //pos = new Rectangle(0,0,image.Width,image.Height);
        }

        public void Update(GameTime gameTime)
        {
            pos.X -= 3;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(image, pos, color);
            spriteBatch.End();
        }
    }
}
