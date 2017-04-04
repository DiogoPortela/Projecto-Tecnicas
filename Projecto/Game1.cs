﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Projecto
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        static public GraphicsDeviceManager graphics;
        static public SpriteBatch spriteBatch;
        static public ContentManager content;

        static GameState gameState;

        #region ZONA TESTE
        //private Texture2D player;
        Player player;
        float playerSpeed = 0.3f;
        Map map;
        #endregion


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
            content = Content;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            map = new Map();
            player = new Player("Drude", new Vector2(10, 10), 5f, PlayerNumber.playerOne);
            gameState = new GameState();

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
            //player = Content.Load<Texture2D>("Drude.png");
            //Camera.SetCameraWindow(new Vector2(0, 0), 100f);
            map.Generate(new int[,]
                {
                    {2,2,2,2,2},
                    {0,0,1,2,1},
                    {0,1,2,2,0},
                    {1,2,2,2,1},
                    {1,2,2,2,1},
                    {1,2,2,2,1},
                    {1,2,2,2,1},
                    {1,2,2,2,1},
                    {1,2,2,2,1},
                    {1,2,2,2,1},
                    {1,2,2,2,1},
                    {1,2,2,2,1},
                    {1,2,2,2,1},
                    {0,2,2,0,1},
                    {1,0,0,0,1}

                }, 5);
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
            gameState.StateUpdate(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.HotPink);

            gameState.Draw();

            spriteBatch.Begin();

            //map.Draw(spriteBatch);
            //player.DrawObject();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
