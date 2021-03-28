
using System;
using ChipmunkBinding;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

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
        bool touchDragging;

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
            touchDragging = false;

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

            TouchPanel.EnabledGestures = GestureType.FreeDrag | GestureType.DragComplete | GestureType.DoubleTap | GestureType.Pinch | GestureType.PinchComplete;
        }

        public static Space CreateSpace()
        {
#if __MOBILE__
            return new HastySpace();
#else
            return new Space();
#endif
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

            demo = new DemoBase[]
            {
                new LogoSmash(),
                new PyramidStack(),
                new Tumble(),
                new Plink(),
                new PyramidTopple(),
                new Slice(),
                new Planet(),
                new Tank()
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
#if !__MOBILE__
            // For Mobile devices, this logic will close the Game when the Back button is pressed
            // Exit() is obsolete on iOS
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            HandleMouse();
#endif

            HandleTouchGesture();

            UpdateMouseBody();

            double dt = gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0;

            demo[currentDemo].Update(dt);
            //demo[currentDemo].Update(dt);

            base.Update(gameTime);

            HandleKeyboard();
        }

        private void HandleMouse()
        {
            MouseState state = Mouse.GetState();

            if (state.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton != ButtonState.Pressed)
                MouseLeftButtonDown(state.Position.ToVector2());
            if (previousMouseState.LeftButton == ButtonState.Pressed && state.LeftButton != ButtonState.Pressed)
                MouseLeftButtonUp();
            if (state.RightButton == ButtonState.Pressed && previousMouseState.RightButton != ButtonState.Pressed)
                MouseRightButtonDown();
            if (previousMouseState.RightButton == ButtonState.Pressed && state.RightButton != ButtonState.Pressed)
                MouseRightButtonUp();

            MouseMove(state.Position.ToVector2());

            previousMouseState = state;
        }

        private void HandleTouchGesture()
        {
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample sampledGesture = TouchPanel.ReadGesture();

                if ((GestureType.DoubleTap & sampledGesture.GestureType) == GestureType.DoubleTap)
                {
                    HandleDoubleTap(sampledGesture);
                }
                else if ((GestureType.FreeDrag & sampledGesture.GestureType) == GestureType.FreeDrag)
                {
                    HandleDrag(sampledGesture);
                }
                else if ((GestureType.DragComplete & sampledGesture.GestureType) == GestureType.DragComplete)
                {
                    HandleDragComplete(sampledGesture);
                }
                else if ((GestureType.PinchComplete & sampledGesture.GestureType) == GestureType.PinchComplete)
                {
                    HandlePinchComplete(sampledGesture);
                }
                else if ((GestureType.Pinch & sampledGesture.GestureType) == GestureType.Pinch)
                {
                    HandlePinch(sampledGesture);
                }
            }
        }

        private void HandlePinch(GestureSample sampledGesture)
        {
            Vect from = MouseToSpace(sampledGesture.Position);
            Vect to = MouseToSpace(sampledGesture.Position2);

            demo[currentDemo].OnPinch(from, to);
        }

        private void HandlePinchComplete(GestureSample sampledGesture)
        {
            demo[currentDemo].OnPinchComplete();
        }

        private void HandleDragComplete(GestureSample sampledGesture)
        {
            touchDragging = false;

            MouseLeftButtonUp();
        }

        private void HandleDrag(GestureSample sampledGesture)
        {
            if (touchDragging)
            {
                HandleDragMove(sampledGesture);
                return;
            }

            HandleDragBegin(sampledGesture);
        }

        private void HandleDragBegin(GestureSample sampledGesture)
        {
            touchDragging = true;

            MouseLeftButtonDown(sampledGesture.Position);
        }

        private void HandleDragMove(GestureSample sampledGesture)
        {
            MouseMove(sampledGesture.Position);
        }

        private void HandleDoubleTap(GestureSample sampledGesture)
        {
            RightKeyUp();
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
            if (cursorJoint == null || cursorJoint.Space == null)
                return;

            space.RemoveConstraint(cursorJoint);
            cursorJoint.Dispose();
            cursorJoint = null;

            demo[currentDemo].OnMouseLeftButtonUp(chipmunkDemoMouse);
        }

        private void MouseMove(Vector2 position)
        {
            chipmunkDemoMouse = MouseToSpace(position);

            demo[currentDemo].OnMouseMove(chipmunkDemoMouse);

            if (cursorJoint != null)
                return;

            cursorBody.Position = chipmunkDemoMouse;
        }

        private void MouseLeftButtonDown(Vector2 position)
        {
            Vect mousePosition = MouseToSpace(position);

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
            cursorJoint.MaxForce = 5000.0;
            cursorJoint.ErrorBias = Math.Pow(1.0 - 0.15, 60.0);

            space.AddConstraint(cursorJoint);

            demo[currentDemo].OnMouseLeftButtonDown(chipmunkDemoMouse);
        }

        private Vect MouseToSpace(Vector2 position)
        {
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
                if (c == cursorJoint)
                    continue;

                space.RemoveConstraint(c);
                c.Dispose();
            }

            space.Free();
            space = null;
        }
    }
}
