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

                IntPtr memory = AllocateImage(optionalHeader.ImageBase, optionalHeader.SizeOfImage);
                if (memory == IntPtr.Zero)
                {
                    return IntPtr.Zero;
                }

                SectionHeader[] sectionHeaders = new SectionHeader[peHeader.NumberOfSections];
                for (int i = 0; i < peHeader.NumberOfSections; i++)
                {
                    SectionHeader sectionHeader = SectionHeader.FromStream(ms);
                    sectionHeaders[i] = sectionHeader;
                    uint size = sectionHeader.SizeOfRawData;
                    if (size == 0)
                    {
                        if (sectionHeader.Characteristics.HasFlag(SectionHeaderCharacteristics.ContainsInitializedData))
                        {
                            size = optionalHeader.SizeOfInitializedData;
                        }
                        else if (sectionHeader.Characteristics.HasFlag(SectionHeaderCharacteristics.ContainsUninitializedData))
                        {
                            size = optionalHeader.SizeOfUninitializedData;
                        }
                    }

                    IntPtr section = AllocateSection(memory, sectionHeader.VirtualAddress, sectionHeader.SizeOfRawData);
                    if (section == IntPtr.Zero)
                    {
                        return IntPtr.Zero;
                    }

                    Marshal.Copy(library, (int)sectionHeader.PointerToRawData, section, (int)sectionHeader.SizeOfRawData);
                }

                return memory;
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

        private IntPtr AllocateSection(IntPtr basePointer, ulong virtualAddress, uint size)
            => NativeMethods.VirtualAlloc(new IntPtr(basePointer.ToInt64() + (long)virtualAddress), new UIntPtr(size), NativeMethods.AllocationTypeCommit, NativeMethods.MemoryProtectionReadWrite);

        private static class NativeMethods
        {
            internal const uint AllocationTypeReserve = 0x2000;
            internal const uint AllocationTypeCommit = 0x1000;
            internal const uint MemoryProtectionReadWrite = 0x04;

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
