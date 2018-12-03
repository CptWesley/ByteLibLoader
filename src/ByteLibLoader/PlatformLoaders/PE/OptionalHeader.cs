using System;
using System.Collections.ObjectModel;
using System.IO;
using ExtensionNet.Streams;

namespace ByteLibLoader.PlatformLoaders.PE
{
    /// <summary>
    /// Class representing a parsed PE optional header.
    /// </summary>
    public class OptionalHeader
    {
        /// <summary>
        /// The magic number if the file is an executable 64 bit image.
        /// </summary>
        public const ushort Hdr64Magic = 0x20B;

        /// <summary>
        /// The magic number if the file is an executable 32 bit image.
        /// </summary>
        public const ushort Hdr32Magic = 0x10B;

        /// <summary>
        /// The magic number if the file is a ROM image.
        /// </summary>
        public const ushort HdrMagic = 0x107;

        /// <summary>
        /// The required magic numbers.
        /// </summary>
        public static readonly ReadOnlyCollection<ushort> RequiredMagicNumbers = Array.AsReadOnly(new[] { Hdr64Magic, Hdr32Magic, HdrMagic });

        /// <summary>
        /// Gets or sets the found magic number.
        /// </summary>
        public ushort Magic { get; set; }

        /// <summary>
        /// Gets or sets the major linker version.
        /// </summary>
        public byte MajorLinkerVersion { get; set; }

        /// <summary>
        /// Gets or sets the minor linker version.
        /// </summary>
        public byte MinorLinkerVersion { get; set; }

        /// <summary>
        /// Gets or sets the the size of all sections.
        /// </summary>
        public uint SizeOfCode { get; set; }

        /// <summary>
        /// Gets or sets the size of initialized data.
        /// </summary>
        public uint SizeOfInitializedData { get; set; }

        /// <summary>
        /// Gets or sets the size of uninitialized data.
        /// </summary>
        public uint SizeOfUninitializedData { get; set; }

        /// <summary>
        /// Gets or sets the RVA address of the entry point.
        /// </summary>
        public uint AddressOfEntryPoint { get; set; }

        /// <summary>
        /// Gets or sets the RVA address of the base of the code.
        /// </summary>
        public uint BaseOfCode { get; set; }

        /// <summary>
        /// Gets or sets the RVA address of the base of the data.
        /// </summary>
        public uint BaseOfData { get; set; }

        /// <summary>
        /// Gets or sets the image base address.
        /// </summary>
        public uint ImageBase { get; set; }

        /// <summary>
        /// Gets or sets the section alignment.
        /// </summary>
        public uint SectionAlignment { get; set; }

        /// <summary>
        /// Gets or sets the file alignment.
        /// </summary>
        public uint FileAlignment { get; set; }

        /// <summary>
        /// Gets or sets the major operating system version.
        /// </summary>
        public byte MajorOperatingSystemVersion { get; set; }

        /// <summary>
        /// Gets or sets the minor operating system version.
        /// </summary>
        public byte MinorOperatingSystemVersion { get; set; }

        /// <summary>
        /// Gets or sets the major image version.
        /// </summary>
        public byte MajorImageVersion { get; set; }

        /// <summary>
        /// Gets or sets the minor image version.
        /// </summary>
        public byte MinorImageVersion { get; set; }

        /// <summary>
        /// Gets or sets the major subsystem version.
        /// </summary>
        public byte MajorSubsystemVersion { get; set; }

        /// <summary>
        /// Gets or sets the minor subsystem version.
        /// </summary>
        public byte MinorSubsystemVersion { get; set; }

        /// <summary>
        /// Gets or sets the win32 version value.
        /// </summary>
        public uint Win32VersionValue { get; set; }

        /// <summary>
        /// Gets or sets the size of image.
        /// </summary>
        public uint SizeOfImage { get; set; }

        /// <summary>
        /// Gets or sets the size of headers.
        /// </summary>
        public uint SizeOfHeaders { get; set; }

        /// <summary>
        /// Gets or sets the checksum.
        /// </summary>
        public uint Checksum { get; set; }

        /// <summary>
        /// Gets or sets the subsystem.
        /// </summary>
        public ushort Subsystem { get; set; }

        /// <summary>
        /// Gets or sets the DLL characteristics.
        /// </summary>
        public ushort DllCharacteristics { get; set; }

        /// <summary>
        /// Gets or sets the size of the reserve stack.
        /// </summary>
        public uint SizeOfStackReserve { get; set; }

        /// <summary>
        /// Gets or sets the size of stack commit.
        /// </summary>
        public uint SizeOfStackCommit { get; set; }

        /// <summary>
        /// Gets or sets the size of heap reserve.
        /// </summary>
        public uint SizeOfHeapReserve { get; set; }

        /// <summary>
        /// Gets or sets the size of stack heap commit.
        /// </summary>
        public uint SizeOfHeapCommit { get; set; }

        /// <summary>
        /// Gets or sets the loader flags.
        /// </summary>
        public uint LoaderFlags { get; set; }

        /// <summary>
        /// Gets or sets the number of rva and sizes.
        /// </summary>
        public uint NumberOfRvaAndSizes { get; set; }

        /// <summary>
        /// Creates a new <see cref="OptionalHeader"/> instance from a stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>A new <see cref="OptionalHeader"/> instance read from the stream.</returns>
        public static OptionalHeader FromStream(Stream stream)
            => new OptionalHeader()
            {
                Magic = stream.ReadUInt16(),
                MajorLinkerVersion = stream.ReadUInt8(),
                MinorLinkerVersion = stream.ReadUInt8(),
                SizeOfCode = stream.ReadUInt32(),
                SizeOfInitializedData = stream.ReadUInt32(),
                SizeOfUninitializedData = stream.ReadUInt32(),
                AddressOfEntryPoint = stream.ReadUInt32(),
                BaseOfCode = stream.ReadUInt32(),
                BaseOfData = stream.ReadUInt32(),
                ImageBase = stream.ReadUInt32(),
                SectionAlignment = stream.ReadUInt32(),
                FileAlignment = stream.ReadUInt32(),
                MajorOperatingSystemVersion = stream.ReadUInt8(),
                MinorOperatingSystemVersion = stream.ReadUInt8(),
                MajorImageVersion = stream.ReadUInt8(),
                MinorImageVersion = stream.ReadUInt8(),
                MajorSubsystemVersion = stream.ReadUInt8(),
                MinorSubsystemVersion = stream.ReadUInt8(),
                Win32VersionValue = stream.ReadUInt32(),
                SizeOfImage = stream.ReadUInt32(),
                SizeOfHeaders = stream.ReadUInt32(),
                Checksum = stream.ReadUInt32(),
                Subsystem = stream.ReadUInt16(),
                DllCharacteristics = stream.ReadUInt16(),
                SizeOfStackReserve = stream.ReadUInt32(),
                SizeOfStackCommit = stream.ReadUInt32(),
                SizeOfHeapReserve = stream.ReadUInt32(),
                SizeOfHeapCommit = stream.ReadUInt32(),
                LoaderFlags = stream.ReadUInt32(),
                NumberOfRvaAndSizes = stream.ReadUInt32()
            };
    }
}
