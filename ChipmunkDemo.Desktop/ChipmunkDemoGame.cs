
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
        Plink demo;

        Vect chipmunkDemoMouse;
        Body mouseBody;
        Constraint mouseJoint;
        MouseState previousState;

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

        public ChipmunkDemoGame()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;

            Content.RootDirectory = "Content";

            mouseBody = new Body(BodyType.Kinematic);
            demo = new Plink();
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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            primitiveBatch = new PrimitiveBatch(GraphicsDevice);
            debugDraw = new ChipmunkDebugDraw(primitiveBatch);

            GraphicsDevice.BlendState = BlendState.NonPremultiplied;

            world = Matrix.CreateScale(1, -1, 1);
            view = Matrix.CreateLookAt(Vector3.Zero, Vector3.Forward, Vector3.Down);
            projection = Matrix.CreateOrthographic(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0, -1);

            Matrix matrix =  Matrix.Invert(view) * Matrix.CreateTranslation(GraphicsDevice.Viewport.Width/2, GraphicsDevice.Viewport.Height/2, 0);
            inverse = matrix;

            primitiveBatch.LoadContent(ref world);
            space = demo.LoadContent();
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

            MouseState state = Mouse.GetState();

            if (state.LeftButton == ButtonState.Pressed && previousState.LeftButton != ButtonState.Pressed)
                MouseLeftButtonDown(state);
            if (previousState.LeftButton == ButtonState.Pressed && state.LeftButton != ButtonState.Pressed)
                MouseLeftButtonUp();

            MouseMove(state);
            UpdateMouseBody();

            demo.Update(gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0);

            base.Update(gameTime);

            previousState = state;
        }

        private void UpdateMouseBody()
        {
            if (mouseJoint == null)
                return;

            Vect mousePosition = mouseBody.Position;

            Vect newPoint = mousePosition.Lerp(chipmunkDemoMouse, 0.25);

            mouseBody.Velocity = (newPoint - mousePosition) * 60;
            mouseBody.Position = newPoint;
        }

        private void MouseLeftButtonUp()
        {
            if (mouseJoint == null)
                return;

            space.Remove(mouseJoint);
            mouseJoint.Dispose();
            mouseJoint = null;
        }

        private void MouseMove(MouseState state)
        {
            chipmunkDemoMouse = MouseToSpace(state);

            if (mouseJoint != null)
                return;

            mouseBody.Position = chipmunkDemoMouse;
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

            mouseJoint = new PivotJoint(mouseBody, body, Vect.Zero, body.WorldToLocal(nearest));
            mouseJoint.MaxForce = 10000.0;
            mouseJoint.ErrorBias = Math.Pow(1.0 - 0.15, 60.0);

            space.AddConstraint(mouseJoint);
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

            space.DebugDraw(debugDraw);

            primitiveBatch.DrawCircle(new Vector2((float)chipmunkDemoMouse.X, (float)chipmunkDemoMouse.Y), 5, Color.LimeGreen, Color.Magenta);

            primitiveBatch.DrawCircle(new Vector2((float)mouseBody.Position.X, (float)mouseBody.Position.Y), 5, Color.BlueViolet, Color.WhiteSmoke);

            primitiveBatch.End();

            base.Draw(gameTime);
        }

        public void FreeSpace()
        {
            foreach (Shape s in space.Shapes)
            {
                space.Remove(s);
                s.Dispose();
            }

            foreach (Constraint c in space.Constraints)
            {
                space.Remove(c);
                c.Dispose();
            }

            space.Dispose();
        }
    }
}
