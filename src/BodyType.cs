using System;
using System.Collections.Generic;
using System.Text;

namespace ChipmunkBinding
{
    /// <summary>
    /// Chipmunk supports three different types of bodies with unique behavioral and performance characteristics.
    /// </summary>
    public enum BodyType
    {
        /// <summary>
        /// Dynamic bodies are the default body type. They react to collisions, are affected by forces and gravity, and have a finite amount of mass. These are the type of bodies that you want the physics engine to simulate for you. Dynamic bodies interact with all types of bodies and can generate collision callbacks.
        /// </summary>
        Dinamic,
        /// <summary>
        /// Kinematic bodies are bodies that are controlled from your code instead of inside the physics engine. They arent affected by gravity and they have an infinite amount of mass so they don’t react to collisions or forces with other bodies. Kinematic bodies are controlled by setting their velocity, which will cause them to move. Good examples of kinematic bodies might include things like moving platforms. Objects that are touching or jointed to a kinematic body are never allowed to fall asleep.
        /// </summary>
        Kinematic,
        /// <summary>
        /// Static bodies are bodies that never (or rarely) move. Using static bodies for things like terrain offers a big performance boost over other body types- because Chipmunk doesn’t need to check for collisions between static objects and it never needs to update their collision information. Additionally, because static bodies don’t move, Chipmunk knows it’s safe to let objects that are touching or jointed to them fall asleep. Generally all of your level geometry will be attached to a static body except for things like moving platforms or doors. Every space provide a built-in static body for your convenience. Static bodies can be moved, but there is a performance penalty as the collision information is recalculated. There is no penalty for having multiple static bodies, and it can be useful for simplifying your code by allowing different parts of your static geometry to be initialized or moved separately.
        /// </summary>
        Static
    }
}
