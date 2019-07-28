using System;
using ChipmunkBinding;
using Microsoft.Xna.Framework;

namespace ChipmunkDemo
{
    public abstract class DemoBase
    {
        protected Space space;

        protected DemoBase()
        {
        }

        public abstract Space LoadContent();

        public virtual void Update(double dt)
        {
            space.Step(dt);
        }

        public virtual void OnMouseRightButtonUp(Vect chipmunkDemoMouse)
        {
        }

        public virtual void OnMouseRightButtonDown(Vect chipmunkDemoMouse)
        {
        }

        public virtual void OnMouseLeftButtonUp(Vect chipmunkDemoMouse)
        {
        }

        public virtual void OnMouseMove(Vect chipmunkDemoMouse)
        {
        }

        public virtual void OnMouseLeftButtonDown(Vect chipmunkDemoMouse)
        {
        }

        public virtual void Draw(GameTime gameTime, IDebugDraw debugDraw)
        {
            space.DebugDraw(debugDraw);

            var colorMagenta = new DebugColor(1, 0, 1, 1);
            debugDraw.DrawCircle(ChipmunkDemoGame.ChipmunkDemoMouse, 0.0, 5, colorMagenta, colorMagenta);
        }
    }
}
