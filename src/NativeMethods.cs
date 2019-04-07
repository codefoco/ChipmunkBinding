// ReSharper disable IdentifierTypo
using System.Runtime.InteropServices;
using System.Security;

using size_t = System.UIntPtr;
using cpFloat = System.Double;
using cpArbiter = System.IntPtr;
using cpSpace = System.IntPtr;

namespace ChipmunkBinding
{
    [SuppressUnmanagedCodeSecurity]
    internal static class NativeMethods
    {
#if __TVOS__ && __UNIFIED__
        private const string ChipmunkLibraryName = "@rpath/libchipmunk.framework/libchipmunk";
#elif __WATCHOS__ && __UNIFIED__
        private const string ChipmunkLibraryName = "@rpath/libchipmunk.framework/libchipmunk";
#elif __IOS__ && __UNIFIED__
        private const string ChipmunkLibraryName = "@rpath/libchipmunk.framework/libchipmunk";
#elif __ANDROID__
        private const string ChipmunkLibraryName = "libchipmunk.so";
#elif __MACOS__ 
        private const string ChipmunkLibraryName = "libchipmunk.dylib";
#elif WINDOWS_UWP
        private const string ChipmunkLibraryName = "chipmunk.dll";
#else
        private const string ChipmunkLibraryName = "chipmunk";
#endif

#pragma warning disable IDE1006 // Naming Styles

        [DllImport (ChipmunkLibraryName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int cpArbiterCallWildcardBeginA(cpArbiter arb, cpSpace space);

        [DllImport (ChipmunkLibraryName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int cpArbiterCallWildcardBeginB(cpArbiter arb, cpSpace space);

        [DllImport (ChipmunkLibraryName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int cpArbiterCallWildcardPostSolveA(cpArbiter arb, cpSpace space);

        [DllImport (ChipmunkLibraryName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int cpArbiterCallWildcardPostSolveB(cpArbiter arb, cpSpace space);

        [DllImport (ChipmunkLibraryName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int cpArbiterCallWildcardPreSolveA(cpArbiter arb, cpSpace space);

        [DllImport (ChipmunkLibraryName, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int cpArbiterCallWildcardPreSolveB(cpArbiter arb, cpSpace space);

#pragma warning restore IDE1006 // Naming Styles

    }
}
