using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using ByteLibLoader.PlatformLoaders.PE;

namespace ByteLibLoader.PlatformLoaders
{
    /// <summary>
    /// Class which is able to load libraries from memory on Windows systems.
    /// </summary>
    /// <seealso cref="ILibraryLoader" />
    [SuppressMessage("Globalization", "CA2101:Specify marshaling for P/Invoke string arguments", Justification = "Type if specified.")]
    public class WindowsLibraryLoader : ILibraryLoader
    {
        /// <inheritdoc/>
        public IntPtr Load(byte[] library)
        {
            using (MemoryStream ms = new MemoryStream(library))
            {
                DosHeader dosHeader = DosHeader.FromStream(ms);
                if (dosHeader.Magic != DosHeader.RequiredMagicNumber)
                {
                    return IntPtr.Zero;
                }

                ms.Position = dosHeader.Lfanew;
                PeHeader peHeader = PeHeader.FromStream(ms);
                if (peHeader.Signature != PeHeader.RequiredMagicNumber)
                {
                    return IntPtr.Zero;
                }

                OptionalHeader optionalHeader = OptionalHeader.FromStream(ms);
                if (!OptionalHeader.RequiredMagicNumbers.Contains(optionalHeader.Magic))
                {
                    return IntPtr.Zero;
                }
            }

            return IntPtr.Zero;
        }

        /// <inheritdoc/>
        public bool Unload(IntPtr library)
            => NativeMethods.FreeLibrary(library);

        /// <inheritdoc/>
        public IntPtr GetSymbol(IntPtr library, string symbol)
            => NativeMethods.GetProcAddress(library, symbol);

        private static class NativeMethods
        {
            [DllImport("kernel32")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool FreeLibrary(IntPtr hModule);

            [DllImport("kernel32")]
            internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string lpFileName);

            [DllImport("kernel32")]
            internal static extern IntPtr GetProcAddress(IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)]string procName);
        }
    }
}
