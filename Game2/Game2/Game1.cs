using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections;

namespace Game2
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Random rand;
        

        //Wrapper link;
        Wrapper Andy;
        int sW, sH, x, y;
        public static int ScreenWidth = 1280;
        public static int ScreenHeight = 500;
        public static int size = 100;
        //Pipe
        BasicSprite Pipe;
        int pSize;
        ArrayList pipes;
        int sizeY;

        //cloud
        BasicSprite Cloud;
        int cSize;
        ArrayList clouds;
        int posY;

        float timer = 1, elapse, resetTimer = 4, lessTime = 1.5f;

        float timerCloud = 1, elapseCloud, resetTimerCloud = 4, lessTimeCloud = 1.5f;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            size = 100;
            pSize = 50;
            sW = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            sH = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            rand = new Random();
            BasicAnimatedSprite.SetWindoeSize(new Rectangle(0, 0, sW, sH));
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
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.ApplyChanges();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Andy = new Wrapper(new Rectangle(50, ScreenHeight - size - 9, size, size), new Point(10, 10));

            pipes = new ArrayList();

            clouds = new ArrayList();

            Andy.LoadContent(Content);

            Andy.SetKeys(Keys.Up, Keys.Left, Keys.Down, Keys.Right, Keys.Space);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
         
            //------------------------------------------------------------------------------------------------------------
            //Pipes
            int randomTexture = rand.Next(1, 3);
            
            if (randomTexture == 1)
            {
                sizeY = 50;
            }
            else if (randomTexture == 2)
            {
                sizeY = 100;
            }
            else if (randomTexture == 3)
            {
                sizeY = 150;
            }
            elapse = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timer -= elapse;
            int countCorona = pipes.Count;
            if (timer < 0)
            {
                Pipe = new BasicSprite(ScreenWidth, (ScreenHeight-sizeY), pSize, sizeY);
                Pipe.SetColor(Color.White);
                pipes.Add(Pipe);


                for (int i = countCorona; i < pipes.Count; i++)
                {
                    ((BasicSprite)pipes[i]).LoadContent(Content, "pipe1");
                }

                resetTimer -= 0.2f;

                if(resetTimer <= 1.5)
                {
                    resetTimer = 4;
                }
                else
                {
                    timer = resetTimer;
                }
                
            }

            for (int i = 0; i < pipes.Count; i++) {
                if (((BasicSprite)pipes[i]).Pos.X > ((BasicSprite)pipes[i]).Pos.X + size)
                {
                    pipes.RemoveAt(i);
                    break;
                }
            }

            //-----------------------------------------------------------------------------------------------------------
            //Clouds
            int randomTextureCloud = rand.Next(1, 3);

            if (randomTextureCloud == 1)
            {
                posY = 50;
            }
            else if (randomTextureCloud == 2)
            {
                posY = 25;
            }
            else if (randomTextureCloud == 3)
            {
                posY = 10;
            }

            elapseCloud = (float)gameTime.ElapsedGameTime.TotalSeconds;
            timerCloud -= elapseCloud;
            int countClouds = clouds.Count;

            if (timerCloud < 0)
            {
                Cloud = new BasicSprite(ScreenWidth, posY, 600, 400);
                Cloud.SetColor(Color.Black);
                clouds.Add(Cloud);


                for (int i = countClouds; i < clouds.Count; i++)
                {
                    ((BasicSprite)clouds[i]).LoadContent(Content, "nubes");
                }

                    timerCloud = resetTimerCloud;
                

            }

            for (int i = 0; i < clouds.Count; i++)
            {
                if (((BasicSprite)clouds[i]).Pos.X > ((BasicSprite)clouds[i]).Pos.X + size)
                {
                    clouds.RemoveAt(i);
                    break;
                }
            }

            //------------------------------------------------------------------------------------------------------------
            Andy.ResetCollision();

            for (int i = countCorona; i < pipes.Count; i++)
                if (Andy.Collision(((BasicSprite)pipes[i]).Pos))
                {
                    ((BasicSprite)pipes[i]).SetColor(Color.AliceBlue);
                    break;
                }

            Andy.Update(gameTime);

            foreach (BasicSprite pipe in pipes)
                pipe.Update(gameTime);

            foreach(BasicSprite cloud in clouds)
                cloud.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            //link.Draw(spriteBatch);
            foreach (BasicSprite pipe in pipes)
                pipe.Draw(spriteBatch);

            foreach (BasicSprite cloud in clouds)
                cloud.Draw(spriteBatch);

            Andy.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
