﻿using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    [StructLayout(LayoutKind.Sequential)]
    public struct cpTransform
    {
        public float a;
        public float b;
        public float c;
        public float d;
        public float tx;
        public float ty;
    }
}