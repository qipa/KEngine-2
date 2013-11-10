#region Using Statements
using System;
using System.Linq;
using Kupiakos.KEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace KEngineTest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics;
        Engine engine;

        public Game1()
        {
            //Console.WriteLine(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width);

            graphics = new GraphicsDeviceManager(this);



            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
//      
//            graphics.PreferredBackBufferWidth = 1920;
//            graphics.PreferredBackBufferHeight = 1080;
//            graphics.IsFullScreen = true;
//            graphics.ApplyChanges();
//            this.Window.AllowUserResizing = true;
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            
            //Console.WriteLine("{0}x{1}", GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
            //   GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            graphics.ApplyChanges();
            
            //Console.WriteLine("{0}x{1}", graphics.PreferredBackBufferWidth,
            //     graphics.PreferredBackBufferHeight);

            graphics.IsFullScreen = true;

            engine = new Engine(this);


            this.Exit();


            engine.SwitchScene<TestScene>();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            //spriteBatch = new SpriteBatch(GraphicsDevice);

            //TODO: use this.Content to load your game content here 
        }

        protected override void BeginRun()
        {
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            
            //Console.WriteLine("{0}x{1}", GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
            //                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
            graphics.ApplyChanges();
            
            //Console.WriteLine("{0}x{1}", graphics.PreferredBackBufferWidth,
            //                graphics.PreferredBackBufferHeight);

            //this.Window.
            base.BeginRun();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and plYAccelerationing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            // TODO: Add your update logic here			
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
		
            //TODO: Add your drawing code here
            
            base.Draw(gameTime);
        }
    }
}

