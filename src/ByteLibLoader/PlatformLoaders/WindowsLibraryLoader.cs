using System;
using System.Diagnostics.CodeAnalysis;
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
            try
            {
                PeFile pe = PeFile.Parse(library);
                IntPtr image = AllocateImage(pe.OptionalHeader.ImageBase, pe.OptionalHeader.SizeOfImage);
                CopyHeaders(image, pe, library);
                CopySections(image, pe, library);

                return image;
            }
            catch (PeParsingException)
            {
                return IntPtr.Zero;
            }
        }

        /// <inheritdoc/>
        public bool Unload(IntPtr library)
            => NativeMethods.FreeLibrary(library);

        /// <inheritdoc/>
        public IntPtr GetSymbol(IntPtr library, string symbol)
            => NativeMethods.GetProcAddress(library, symbol);

        private IntPtr AllocateImage(ulong imageBase, uint imageSize)
            => NativeMethods.VirtualAlloc(new IntPtr((long)imageBase), new UIntPtr(imageSize), NativeMethods.AllocationTypeReserve, NativeMethods.MemoryProtectionReadWrite);

        private void CopyHeaders(IntPtr basePointer, PeFile pe, byte[] library)
        {
            IntPtr headers = NativeMethods.VirtualAlloc(basePointer, new UIntPtr(pe.OptionalHeader.SizeOfHeaders), NativeMethods.AllocationTypeCommit, NativeMethods.MemoryProtectionReadWrite);
            if (headers != basePointer)
            {
                throw new PeParsingException("Failed to copy headers to target.");
            }

            Marshal.Copy(library, 0, headers, (int)pe.OptionalHeader.SizeOfHeaders);
        }

        private void CopySections(IntPtr basePointer, PeFile pe, byte[] library)
        {
            for (int i = 0; i < pe.SectionHeaders.Length; i++)
            {
                SectionHeader sectionHeader = pe.SectionHeaders[i];
                uint size = sectionHeader.SizeOfRawData;
                if (size == 0)
                {
                    if (sectionHeader.Characteristics.HasFlag(SectionHeaderCharacteristics.ContainsInitializedData))
                    {
                        size = pe.OptionalHeader.SizeOfInitializedData;
                    }
                    else if (sectionHeader.Characteristics.HasFlag(SectionHeaderCharacteristics.ContainsUninitializedData))
                    {
                        size = pe.OptionalHeader.SizeOfUninitializedData;
                    }
                }

                IntPtr sectionTarget = new IntPtr(basePointer.ToInt64() + sectionHeader.VirtualAddress);
                IntPtr section = NativeMethods.VirtualAlloc(sectionTarget, new UIntPtr(size), NativeMethods.AllocationTypeCommit, NativeMethods.MemoryProtectionExecuteReadWrite);
                if (section == IntPtr.Zero || section != sectionTarget)
                {
                    throw new PeParsingException("Failed to allocate memory for the sections.");
                }

                Marshal.Copy(library, (int)sectionHeader.PointerToRawData, section, (int)sectionHeader.SizeOfRawData);
            }
        }

        private static class NativeMethods
        {
            internal const uint AllocationTypeReserve = 0x2000;
            internal const uint AllocationTypeCommit = 0x1000;
            internal const uint MemoryProtectionReadWrite = 0x04;
            internal const uint MemoryProtectionExecuteReadWrite = 0x40;

            [DllImport("kernel32")]
            [return: MarshalAs(UnmanagedType.Bool)]
            internal static extern bool FreeLibrary(IntPtr hModule);

            [DllImport("kernel32")]
            internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string lpFileName);

            [DllImport("kernel32")]
            internal static extern IntPtr GetProcAddress(IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)]string procName);

            [DllImport("kernel32")]
            internal static extern IntPtr VirtualAlloc(IntPtr lpAddress, UIntPtr dwSize, uint lAllocationType, uint flProtect);
        }
    }
}
