
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ChipmunkDemo
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class ChipmunkDemoGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        PrimitiveBatch primitiveBatch;
        ChipmunkDebugDraw debugDraw;
        PyramidStack demo;

        static Color backgroundColor = new Color(0x07, 0x36, 0x42);

        public ChipmunkDemoGame()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;

            Content.RootDirectory = "Content";

            demo = new PyramidStack();
        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            demo.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            primitiveBatch = new PrimitiveBatch(GraphicsDevice);
            debugDraw = new ChipmunkDebugDraw(primitiveBatch);

            GraphicsDevice.BlendState = BlendState.NonPremultiplied;

            primitiveBatch.LoadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            demo.Update(gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(backgroundColor);
            float left = -graphics.GraphicsDevice.Viewport.Width / 2 - 320;
            float bottom = -graphics.GraphicsDevice.Viewport.Height / 2 - 240;
            var projection = Matrix.CreateOrthographicOffCenter(left, 640, 480, bottom, 0, -1);
            var view = Matrix.CreateLookAt(new Vector3(0, 0, 0), Vector3.Forward, Vector3.Down);

            primitiveBatch.Begin(ref projection, ref view);

            demo.Draw(debugDraw);

            primitiveBatch.End();

            base.Draw(gameTime);
        }
    }
}
