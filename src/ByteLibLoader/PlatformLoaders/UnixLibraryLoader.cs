using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;

namespace ByteLibLoader.PlatformLoaders
{
    /// <summary>
    /// Class which is able to load libraries from memory on Unix systems.
    /// </summary>
    /// <seealso cref="ILibraryLoader" />
    [SuppressMessage("Globalization", "CA2101:Specify marshaling for P/Invoke string arguments", Justification = "Type if specified.")]
    public class UnixLibraryLoader : ILibraryLoader
    {
        /// <inheritdoc/>
        public IntPtr Load(byte[] library)
        {
            string name = Guid.NewGuid().ToString("N", CultureInfo.InvariantCulture);

            // Not all distributions have a library wrapping "memfd_create" yet and not all kernels support the syscall yet.
            int fd = NativeMethods.syscall(NativeMethods.__NR_memfd_create, name, NativeMethods.MFD_CLOEXEC);
            bool memfd = fd != -1;

            if (!memfd)
            {
                // This fallback sadly still creates a temporary file, but it's better than crashing.
                fd = NativeMethods.shm_open(name, NativeMethods.O_RDWR | NativeMethods.O_CREAT, NativeMethods.S_IRWXU);
            }

            if (NativeMethods.write(fd, library, library.Length) == -1)
            {
                return IntPtr.Zero;
            }

            string fileName = memfd ? $"/proc/{NativeMethods.getpid()}/fd/{fd}" : $"/dev/shm/{name}";

            try
            {
                return NativeMethods.dlopen(fileName, NativeMethods.RTLD_NOW);
            }
            catch (DllNotFoundException)
            {
                // Some distributions require "libc6-dev" for "libdl" to be able to be resolved, but they might know "libdl.so.2".
                return NativeMethods.dlopen2(fileName, NativeMethods.RTLD_NOW);
            }
        }

        /// <inheritdoc/>
        public IntPtr GetSymbol(IntPtr library, string symbol)
        {
            try
            {
                return NativeMethods.dlsym(library, symbol);
            }
            catch (DllNotFoundException)
            {
                // Some distributions require "libc6-dev" for "libdl" to be able to be resolved, but they might know "libdl.so.2".
                return NativeMethods.dlsym2(library, symbol);
            }
        }

        private class NativeMethods
        {
            public const long __NR_memfd_create = 319;
            public const int O_RDWR = 2;
            public const int O_CREAT = 64;
            public const int S_IRWXU = 448;
            public const int MFD_CLOEXEC = 1;
            public const int RTLD_NOW = 2;

            [DllImport("librt")]
            public static extern int shm_open([MarshalAs(UnmanagedType.LPStr)]string name, int flag, int mode);

            [DllImport("libc")]
            public static extern int syscall(long id, [MarshalAs(UnmanagedType.LPStr)]string name, uint flags);

            [DllImport("libc")]
            public static extern int write(int fileDescriptor, byte[] data, int count);

            [DllImport("libc")]
            public static extern int getpid();

            [DllImport("libdl")]
            public static extern IntPtr dlsym(IntPtr handle, [MarshalAs(UnmanagedType.LPStr)]string symbol);

            [DllImport("libdl")]
            public static extern IntPtr dlopen([MarshalAs(UnmanagedType.LPStr)]string fileName, int flag);

            [DllImport("libdl.so.2", EntryPoint = "dlopen")]
            public static extern IntPtr dlopen2([MarshalAs(UnmanagedType.LPStr)]string fileName, int flag);

            [DllImport("libdl.so.2", EntryPoint = "dlsym")]
            public static extern IntPtr dlsym2(IntPtr handle, [MarshalAs(UnmanagedType.LPStr)]string symbol);
        }
    }
}
