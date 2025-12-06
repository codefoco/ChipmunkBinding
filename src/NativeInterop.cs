// .      ______          __     ____               
//       / ____/___  ____/ /__  / __/___  _________ 
//      / /   / __ \/ __  / _ \/ /_/ __ \/ ___/ __ \
//     / /___/ /_/ / /_/ /  __/ __/ /_/ / /__/ /_/ /
//     \____/\____/\__, _/\___/_/  \____/\___/\____/ 
//     
//     Copyright (c) 2026 Codefoco LTDA - The above copyright notice and this permission notice shall be
//     included in all copies or substantial portions of the Software.
//
//     Redistribution and use in source and binary forms, with or without
//     modification, are permitted only if explicitly approved by the authors.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, 
//     EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//     OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//     NONINFRINGEMENT.IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//     HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, 
//     WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//     FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//     OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ChipmunkBinding
{
    internal static class NativeInterop
    {
        public static IntPtr RegisterHandle(object obj)
        {
            var gcHandle = GCHandle.Alloc(obj);
            return GCHandle.ToIntPtr(gcHandle);
        }

        public static T FromIntPtr<T>(IntPtr pointer)
        {
            Debug.Assert(pointer != IntPtr.Zero, "IntPtr parameter should never be Zero");

            var handle = GCHandle.FromIntPtr(pointer);

            Debug.Assert(handle.IsAllocated, "GCHandle not allocated.");
            Debug.Assert(handle.Target is T, "Target is not of type T.");

            return (T)handle.Target;
        }

        public static void ReleaseHandle(IntPtr pointer)
        {
            Debug.Assert(pointer != IntPtr.Zero, "IntPtr parameter should never be Zero");

            var handle = GCHandle.FromIntPtr(pointer);

            Debug.Assert(handle.IsAllocated, "GCHandle not allocated.");

            handle.Free();
        }

        public static T FromIntPtrAndFree<T>(IntPtr pointer)
        {
            Debug.Assert(pointer != IntPtr.Zero, "IntPtr parameter should never be Zero");

            var handle = GCHandle.FromIntPtr(pointer);

            Debug.Assert(handle.IsAllocated, "GCHandle not allocated.");
            Debug.Assert(handle.Target is T, "Target is not of type T.");
            T obj = (T)handle.Target;

            handle.Free();

            return obj;
        }

        public static int SizeOf<T>()
        {
#if NET_4_0
            return Marshal.SizeOf(typeof(T));
#else
            return Marshal.SizeOf<T>();
#endif
        }

        public static IntPtr AllocStructure<T>()
        {
            int size = SizeOf<T>();
            return Marshal.AllocHGlobal(size);
        }

        public static void FreeStructure(IntPtr ptr)
        {
            Marshal.FreeHGlobal(ptr);
        }

        public static T PtrToStructure<T>(IntPtr intPtr)
        {
#if NET_4_0
            return (T)Marshal.PtrToStructure(intPtr, typeof(T));
#else
#pragma warning disable IL2091
            return Marshal.PtrToStructure<T>(intPtr);
#pragma warning restore IL2091
#endif
        }

        public static T[] PtrToStructureArray<T>(IntPtr intPtr, int count)
        {
            var items = new T[count];
            int size = SizeOf<T>();

            for (int i = 0; i < count; i++)
            {
                IntPtr newPtr = new IntPtr(intPtr.ToInt64() + (i * size));
                items[i] = PtrToStructure<T>(newPtr);
            }

            return items;
        }

        internal static IntPtr StructureArrayToPtr<T>(T[] items)
        {
            int size = SizeOf<T>();
            int count = items.Length;
            IntPtr memory = Marshal.AllocHGlobal(size * count);

            for (int i = 0; i < count; i++)
            {
                IntPtr ptr = new IntPtr(memory.ToInt64() + (i * size));
                Marshal.StructureToPtr(items[i], ptr, true);
            }

            return memory;
        }
    }
}