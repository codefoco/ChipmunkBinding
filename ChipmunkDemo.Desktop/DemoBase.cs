using System;
using ChipmunkBinding;

namespace ChipmunkDemo
{
    public abstract class DemoBase
    {
        protected Space space;

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
    }
}
