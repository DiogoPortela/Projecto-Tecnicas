using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

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
        static public Random random;
        static public Rectangle cameraArea;

        static public KeyboardState lastFrameState;
        static public KeyboardState currentFrameState;
        static public ScreenSelect selectedScreen;

        static MainMenu menu;
        static GameState gameState;

        //------------->CONSTRUCTORS<-------------//

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            cameraArea = new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            Content.RootDirectory = "Content";
            content = Content;
            random = new Random();

            selectedScreen = ScreenSelect.MainMenu;
        }

        //------------->FUNCTIONS && METHODS<-------------//
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            UI.Start(null);
            menu = new MainMenu();
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
            currentFrameState = Keyboard.GetState();
            UI.Update();
            if (selectedScreen == ScreenSelect.Quit)
                Exit();
            else if (selectedScreen == ScreenSelect.MainMenu)
                menu.Update();
            else if (selectedScreen == ScreenSelect.Playing)
                gameState.StateUpdate(gameTime);
            base.Update(gameTime);
            lastFrameState = currentFrameState;
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.HotPink);

            if (selectedScreen == ScreenSelect.MainMenu)
                menu.Draw();
            if (selectedScreen == ScreenSelect.Playing)
                gameState.Draw();

            base.Draw(gameTime);
        }
    }
}
