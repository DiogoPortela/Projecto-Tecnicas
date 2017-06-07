using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;

namespace Projecto
{
    //I'm fixin it.
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        static public GraphicsDeviceManager graphics;
        static public SpriteBatch spriteBatch;
        static public ContentManager content;
        static public Random random;
        static public Camera mainCamera;
        static public Dictionary<string, Texture2D> textureList;

        static public KeyboardState lastFrameState;
        static public KeyboardState currentFrameState;
        static public ScreenSelect selectedScreen;

        //------------->CONSTRUCTORS<-------------//

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
            content = Content;
            random = new Random();
            mainCamera = new Camera(Vector2.Zero, 100);

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
            #region LoadTextures

            textureList = new Dictionary<string, Texture2D>();
            List<string> auxString = new List<string>();

            auxString.Add("PlayerOne_SideWalk_NoWeapon");
            auxString.Add("PlayerOne_SideWalk2_NoWeapon");
            auxString.Add("PlayerOne_Standing_NoWeapon");
            auxString.Add("PlayerOne_Walking_NoWeapon");
            auxString.Add("PlayerOne_BackWalking_NoWeapon");
            auxString.Add("PlayerOne_attack");
            auxString.Add("PlayerOne_stick");

            auxString.Add("enemy");

            auxString.Add("portal");

            auxString.Add("Tile1");
            auxString.Add("Tile2");
            auxString.Add("Tile3");
            auxString.Add("Tile10");
            auxString.Add("Tile11");
            auxString.Add("Tile12");

            auxString.Add("DebugPixel");
            auxString.Add("Sword");
            auxString.Add("Staff");
            auxString.Add("ui_center");

            foreach (string s in auxString)
            {
                Texture2D aux = content.Load<Texture2D>(s);
                textureList.Add(s, aux);
            }
            #endregion

            SoundManager.Start();
            UI.Start(null);
            Debug.Start(null, 30);
            MainMenu.Start();

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
            if (InputManager.PressedLastFrame.F2 == ButtonState.Pressed)
            {
                Debug.Toggle();
            }
            if (selectedScreen == ScreenSelect.Quit)
                Exit();
            else if (selectedScreen == ScreenSelect.MainMenu)
                MainMenu.Update();
            else if (selectedScreen == ScreenSelect.Playing)
                GameState.StateUpdate(gameTime);
            base.Update(gameTime);
            lastFrameState = currentFrameState;
        }
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (selectedScreen == ScreenSelect.MainMenu)
                MainMenu.Draw();
            if (selectedScreen == ScreenSelect.Playing)
                GameState.Draw();

            base.Draw(gameTime);
        }
    }
}
