// .      ______          __     ____               
//       / ____/___  ____/ /__  / __/___  _________ 
//      / /   / __ \/ __  / _ \/ /_/ __ \/ ___/ __ \
//     / /___/ /_/ / /_/ /  __/ __/ /_/ / /__/ /_/ /
//     \____/\____/\__, _/\___/_/  \____/\___/\____/ 
//     
//     Copyright (c) 2023 Codefoco LTDA - The above copyright notice and this permission notice shall be
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

using System.Reflection;

#if __MACOS__ || __TVOS__ || __WATCHOS__ || __IOS__ || __MACCATALYST__

using Foundation;

#if NET
[assembly: AssemblyMetadata ("IsTrimmable", "True")]
#else
[assembly: LinkerSafe]
#endif

#endif

// Information about this assembly is defined by the following attributes. 
// Change them to the values specific to your project.

#if NETFRAMEWORK
[assembly: AssemblyTitle("ChipmunkBinding (.NET Framework 4.6)")]
#elif WINDOWS_UWP
[assembly: AssemblyTitle("ChipmunkBinding (Windows Universal)")]
#elif __ANDROID__
[assembly: AssemblyTitle("ChipmunkBinding (Xamarin.Android)")]
#elif NETCOREAPP
[assembly: AssemblyTitle("ChipmunkBinding (.NET)")]
#elif NETSTANDARD
[assembly: AssemblyTitle("ChipmunkBinding (.NET Standard)")]
#elif __TVOS__
[assembly: AssemblyTitle ("ChipmunkBinding (tvOS)")]
#elif __WATCHOS__
[assembly: AssemblyTitle("ChipmunkBinding (watchOS)")]
#elif __IOS__
[assembly: AssemblyTitle("ChipmunkBinding (iOS)")]
#elif __MACCATALYST__
[assembly: AssemblyTitle("ChipmunkBinding (Mac Catalyst)")]
#elif __MACOS__
[assembly: AssemblyTitle("ChipmunkBinding (Mac)")]
#else
[assembly: AssemblyTitle("ChipmunkBinding (.NET)")]
#endif

[assembly: AssemblyDescription("Binding library for native Chipmunk2D")]
[assembly: AssemblyCompany("Codefoco")]
[assembly: AssemblyProduct("ChipmunkBinding")]
[assembly: AssemblyCopyright("Copyright © Codefoco 2022")]
[assembly: AssemblyCulture("")]

// The assembly version has the format "{Major}.{Minor}.{Build}.{Revision}".
// The form "{Major}.{Minor}.*" will automatically update the build and revision,
// and "{Major}.{Minor}.{Build}.*" will update just the revision.

[assembly: AssemblyVersion("0.1.0.0")]
[assembly: AssemblyInformationalVersion("0.1.0+155.Branch.main.Sha.2bffe65059140b063c0ca22a830593629f64b2cb")]
[assembly: AssemblyFileVersion("0.1.0.0")]