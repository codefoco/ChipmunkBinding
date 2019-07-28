
using System;
using ChipmunkBinding;
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
        DemoBase [] demo;

        static Vect chipmunkDemoMouse;
        Body cursorBody;
        Constraint cursorJoint;

        MouseState previousMouseState;
        KeyboardState previousKeyboardState;

        int currentDemo = 0;

        Matrix world;
        Matrix view;
        Matrix projection;

        Matrix inverse;

        Space space;

        static Color backgroundColor = new Color(0x07, 0x36, 0x42);

        public enum ShapeCategorie
        {
            GrabbableMaskBit = 1 << 31,
            NotGrabbableMaskBit = ~(1 << 31)
        }

        public static readonly ShapeFilter GrabbableFilter = new ShapeFilter(0, (int)ShapeCategorie.GrabbableMaskBit, (int)ShapeCategorie.GrabbableMaskBit);
        public static readonly ShapeFilter NotGrabbableFilter = new ShapeFilter(0, (int)ShapeCategorie.NotGrabbableMaskBit, (int)ShapeCategorie.NotGrabbableMaskBit);
        public static Vect ChipmunkDemoMouse => chipmunkDemoMouse;

        public ChipmunkDemoGame()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;

            Content.RootDirectory = "Content";

            cursorBody = new Body(BodyType.Kinematic);
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
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            GraphicsDevice.RasterizerState = new RasterizerState
            {
                CullMode = CullMode.None
            };
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch    = new SpriteBatch(GraphicsDevice);
            primitiveBatch = new PrimitiveBatch(GraphicsDevice);
            debugDraw      = new ChipmunkDebugDraw(primitiveBatch);

            demo = new DemoBase[6]
            {
                new LogoSmash(),
                new PyramidStack(),
                new Tumble(),
                new Plink(),
                new PyramidTopple(),
                new Slice()
            };

            float width  = GraphicsDevice.Viewport.Width;
            float height = GraphicsDevice.Viewport.Height;

            world      = Matrix.Identity;
            view       = Matrix.CreateLookAt(Vector3.Zero, Vector3.Forward, Vector3.Up);
            projection = Matrix.CreateOrthographic(width, height, 0, -1);
            inverse    = Matrix.CreateScale(1, -1, 1) * Matrix.CreateTranslation(-width / 2, height / 2, 0);

            primitiveBatch.LoadContent(ref world, ref view, ref projection);

            LoadDemo();
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

            HandleMouse();

            UpdateMouseBody();

            double dt = gameTime.ElapsedGameTime.TotalMilliseconds / 2000.0;

            demo[currentDemo].Update(dt);
            demo[currentDemo].Update(dt);

            base.Update(gameTime);

            HandleKeyboard();
        }

        private void HandleMouse()
        {
            MouseState state = Mouse.GetState();

            if (state.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton != ButtonState.Pressed)
                MouseLeftButtonDown(state);
            if (previousMouseState.LeftButton == ButtonState.Pressed && state.LeftButton != ButtonState.Pressed)
                MouseLeftButtonUp();
            if (state.RightButton == ButtonState.Pressed && previousMouseState.RightButton != ButtonState.Pressed)
                MouseRightButtonDown();
            if (previousMouseState.RightButton == ButtonState.Pressed && state.RightButton != ButtonState.Pressed)
                MouseRightButtonUp();

            MouseMove(state);

            previousMouseState = state;
        }

        private void HandleKeyboard()
        {
            var keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyUp(Keys.Left) && previousKeyboardState.IsKeyDown(Keys.Left))
                LeftKeyUp();
            if (keyboardState.IsKeyUp(Keys.Right) && previousKeyboardState.IsKeyDown(Keys.Right))
                RightKeyUp();

            previousKeyboardState = keyboardState;
        }

        private void LeftKeyUp()
        {
            if (currentDemo == 0)
                currentDemo = demo.Length - 1;
            else
                currentDemo = currentDemo - 1;

            LoadDemo();
        }

        private void RightKeyUp()
        {
            if (currentDemo == demo.Length - 1)
                currentDemo = 0;
            else
                currentDemo = currentDemo + 1;

            LoadDemo();
        }

        private void LoadDemo()
        {
            if (space != null)
            {
                FreeSpace();
            }

            space = demo[currentDemo].LoadContent();
        }

        private void MouseRightButtonUp()
        {
            demo[currentDemo].OnMouseRightButtonUp(chipmunkDemoMouse);
        }

        private void MouseRightButtonDown()
        {
            demo[currentDemo].OnMouseRightButtonDown(chipmunkDemoMouse);
        }

        private void UpdateMouseBody()
        {
            if (cursorJoint == null)
                return;

            Vect mousePosition = cursorBody.Position;

            Vect newPoint = mousePosition.Lerp(chipmunkDemoMouse, 0.25);

            cursorBody.Velocity = (newPoint - mousePosition) * 60;
            cursorBody.Position = newPoint;
        }

        private void MouseLeftButtonUp()
        {
            if (cursorJoint == null)
                return;

            space.RemoveConstraint(cursorJoint);
            cursorJoint.Dispose();
            cursorJoint = null;

            demo[currentDemo].OnMouseLeftButtonUp(chipmunkDemoMouse);
        }

        private void MouseMove(MouseState state)
        {
            chipmunkDemoMouse = MouseToSpace(state);

            demo[currentDemo].OnMouseMove(chipmunkDemoMouse);

            if (cursorJoint != null)
                return;

            cursorBody.Position = chipmunkDemoMouse;
        }

        private void MouseLeftButtonDown(MouseState state)
        {
            Vect mousePosition = MouseToSpace(state);
            // give the mouse click a little radius to make it easier to click small shapes.
            double radius = 5.0;

            PointQueryInfo info = space.PointQueryNearest(mousePosition, radius, GrabbableFilter);
            if (info == null)
                return;

            Shape shape = info.Shape;
            Body body = shape.Body;

            double mass = body.Mass;

            if (double.IsInfinity(mass))
                return;

            // Use the closest point on the surface if the click is outside of the shape.
            Vect nearest = info.Distance > 0.0 ? info.Point : mousePosition;

            cursorJoint = new PivotJoint(cursorBody, body, Vect.Zero, body.WorldToLocal(nearest));
            cursorJoint.MaxForce = 10000.0;
            cursorJoint.ErrorBias = Math.Pow(1.0 - 0.15, 60.0);

            space.AddConstraint(cursorJoint);

            demo[currentDemo].OnMouseLeftButtonDown(chipmunkDemoMouse);
        }

        private Vect MouseToSpace(MouseState state)
        {
            var position = new Vector2(state.X, state.Y);
            position = Vector2.Transform(position, inverse);

            return new Vect(position.X, position.Y);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(backgroundColor);

            primitiveBatch.Begin(ref projection, ref view);


            primitiveBatch.DrawCircle(new Vector2((float)cursorBody.Position.X, (float)cursorBody.Position.Y), 5, Color.BlueViolet, Color.WhiteSmoke);

            demo[currentDemo].Draw(gameTime, debugDraw);
           
            primitiveBatch.End();

            base.Draw(gameTime);
        }

        public void FreeSpace()
        {
            foreach (Shape s in space.Shapes)
            {
                space.RemoveShape(s);
                s.Dispose();
            }

            foreach (Constraint c in space.Constraints)
            {
                space.RemoveConstraint(c);
                c.Dispose();
            }

            space.Free();
            space = null;
        }
    }
}
