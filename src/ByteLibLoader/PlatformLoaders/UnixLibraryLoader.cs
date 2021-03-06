﻿using System;
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

            IntPtr libPtr;
            try
            {
                libPtr = NativeMethods.dlopen(fileName, NativeMethods.RTLD_NOW);
            }
            catch (DllNotFoundException)
            {
                // Some distributions require "libc6-dev" for "libdl" to be able to be resolved, but they might know "libdl.so.2".
                libPtr = NativeMethods.dlopen2(fileName, NativeMethods.RTLD_NOW);
            }

            if (NativeMethods.close(fd) == -1)
            {
                return IntPtr.Zero;
            }

            return libPtr;
        }

        /// <inheritdoc/>
        public bool Unload(IntPtr library)
        {
            int result;
            try
            {
                result = NativeMethods.dlclose(library);
            }
            catch (DllNotFoundException)
            {
                // Some distributions require "libc6-dev" for "libdl" to be able to be resolved, but they might know "libdl.so.2".
                result = NativeMethods.dlclose2(library);
            }

            return result == 0;
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

        private static class NativeMethods
        {
            internal const long __NR_memfd_create = 319;
            internal const int O_RDWR = 2;
            internal const int O_CREAT = 64;
            internal const int S_IRWXU = 448;
            internal const int MFD_CLOEXEC = 1;
            internal const int RTLD_NOW = 2;

            [DllImport("librt")]
            internal static extern int shm_open([MarshalAs(UnmanagedType.LPStr)]string name, int flag, int mode);

            [DllImport("libc")]
            internal static extern int syscall(long id, [MarshalAs(UnmanagedType.LPStr)]string name, uint flags);

            [DllImport("libc")]
            internal static extern int write(int fileDescriptor, byte[] data, int count);

            [DllImport("libc")]
            internal static extern int close(int fileDescriptor);

            [DllImport("libc")]
            internal static extern int getpid();

            [DllImport("libdl")]
            internal static extern IntPtr dlopen([MarshalAs(UnmanagedType.LPStr)]string fileName, int flag);

            [DllImport("libdl")]
            internal static extern int dlclose(IntPtr handle);

            [DllImport("libdl")]
            internal static extern IntPtr dlsym(IntPtr handle, [MarshalAs(UnmanagedType.LPStr)]string symbol);

            [DllImport("libdl.so.2", EntryPoint = "dlopen")]
            internal static extern IntPtr dlopen2([MarshalAs(UnmanagedType.LPStr)]string fileName, int flag);

            [DllImport("libdl.so.2", EntryPoint = "dlclose")]
            internal static extern int dlclose2(IntPtr handle);

            [DllImport("libdl.so.2", EntryPoint = "dlsym")]
            internal static extern IntPtr dlsym2(IntPtr handle, [MarshalAs(UnmanagedType.LPStr)]string symbol);
        }
    }
}
