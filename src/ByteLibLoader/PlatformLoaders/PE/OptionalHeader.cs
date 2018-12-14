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

        private DataDirectory[] dataDirectories;

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
        public ulong ImageBase { get; set; }

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
        public ushort MajorOperatingSystemVersion { get; set; }

        /// <summary>
        /// Gets or sets the minor operating system version.
        /// </summary>
        public ushort MinorOperatingSystemVersion { get; set; }

        /// <summary>
        /// Gets or sets the major image version.
        /// </summary>
        public ushort MajorImageVersion { get; set; }

        /// <summary>
        /// Gets or sets the minor image version.
        /// </summary>
        public ushort MinorImageVersion { get; set; }

        /// <summary>
        /// Gets or sets the major subsystem version.
        /// </summary>
        public ushort MajorSubsystemVersion { get; set; }

        /// <summary>
        /// Gets or sets the minor subsystem version.
        /// </summary>
        public ushort MinorSubsystemVersion { get; set; }

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
        public ulong SizeOfStackReserve { get; set; }

        /// <summary>
        /// Gets or sets the size of stack commit.
        /// </summary>
        public ulong SizeOfStackCommit { get; set; }

        /// <summary>
        /// Gets or sets the size of heap reserve.
        /// </summary>
        public ulong SizeOfHeapReserve { get; set; }

        /// <summary>
        /// Gets or sets the size of stack heap commit.
        /// </summary>
        public ulong SizeOfHeapCommit { get; set; }

        /// <summary>
        /// Gets or sets the loader flags.
        /// </summary>
        public uint LoaderFlags { get; set; }

        /// <summary>
        /// Gets or sets the number of rva and sizes.
        /// </summary>
        public uint NumberOfRvaAndSizes { get; set; }

        /// <summary>
        /// Gets the address of the export table.
        /// </summary>
        public uint ExportTable => dataDirectories[0].Address;

        /// <summary>
        /// Gets the size of the export table.
        /// </summary>
        public uint SizeOfExportTable => dataDirectories[0].Size;

        /// <summary>
        /// Gets the address of the import table.
        /// </summary>
        public uint ImportTable => dataDirectories[1].Address;

        /// <summary>
        /// Gets the size of the import table.
        /// </summary>
        public uint SizeOfImportTable => dataDirectories[1].Size;

        /// <summary>
        /// Gets the address of the resource table.
        /// </summary>
        public uint ResourceTable => dataDirectories[2].Address;

        /// <summary>
        /// Gets the size of the resource table.
        /// </summary>
        public uint SizeOfResourceTable => dataDirectories[2].Size;

        /// <summary>
        /// Gets the address of the exception table.
        /// </summary>
        public uint ExceptionTable => dataDirectories[3].Address;

        /// <summary>
        /// Gets the size of the exception table.
        /// </summary>
        public uint SizeOfExceptionTable => dataDirectories[3].Size;

        /// <summary>
        /// Gets the address of the certificate table.
        /// </summary>
        public uint CertificateTable => dataDirectories[4].Address;

        /// <summary>
        /// Gets the size of the certificate table.
        /// </summary>
        public uint SizeOfCertificateTable => dataDirectories[4].Size;

        /// <summary>
        /// Gets the address of the base relocation table.
        /// </summary>
        public uint BaseRelocationTable => dataDirectories[5].Address;

        /// <summary>
        /// Gets the size of the base relocation table.
        /// </summary>
        public uint SizeOfBaseRelocationTable => dataDirectories[5].Size;

        /// <summary>
        /// Gets the address of the debug information.
        /// </summary>
        public uint Debug => dataDirectories[6].Address;

        /// <summary>
        /// Gets the size of the debug information.
        /// </summary>
        public uint SizeOfDebug => dataDirectories[6].Size;

        /// <summary>
        /// Gets the address of the architecture data.
        /// </summary>
        public uint ArchitectureData => dataDirectories[7].Address;

        /// <summary>
        /// Gets the size of the architecture data.
        /// </summary>
        public uint SizeOfArchitectureData => dataDirectories[7].Size;

        /// <summary>
        /// Gets the address of the global pointer (has no size).
        /// </summary>
        public uint GlobalPtr => dataDirectories[8].Address;

        /// <summary>
        /// Gets the address of the TLS table.
        /// </summary>
        public uint TlsTable => dataDirectories[9].Address;

        /// <summary>
        /// Gets the size of the TLS table.
        /// </summary>
        public uint SizeOfTlsTable => dataDirectories[9].Size;

        /// <summary>
        /// Gets the address of the load config table.
        /// </summary>
        public uint LoadConfigTable => dataDirectories[10].Address;

        /// <summary>
        /// Gets the size of the load config table.
        /// </summary>
        public uint SizeOfLoadConfigTable => dataDirectories[10].Size;

        /// <summary>
        /// Gets the address of the bound import.
        /// </summary>
        public uint BoundImport => dataDirectories[11].Address;

        /// <summary>
        /// Gets the size of the bound import.
        /// </summary>
        public uint SizeOfBoundImport => dataDirectories[11].Size;

        /// <summary>
        /// Gets the address of the import address table.
        /// </summary>
        public uint ImportAddressTable => dataDirectories[12].Address;

        /// <summary>
        /// Gets the size of the import address table.
        /// </summary>
        public uint SizeOfImportAddressTable => dataDirectories[12].Size;

        /// <summary>
        /// Gets the address of the delay import descriptor.
        /// </summary>
        public uint DelayImportDescriptor => dataDirectories[13].Address;

        /// <summary>
        /// Gets the size of the delay import descriptor.
        /// </summary>
        public uint SizeOfDelayImportDescriptor => dataDirectories[13].Size;

        /// <summary>
        /// Gets the address of the CLR runtime header.
        /// </summary>
        public uint ClrRuntimeHeader => dataDirectories[14].Address;

        /// <summary>
        /// Gets the size of the CLR runtime header.
        /// </summary>
        public uint SizeOfClrRuntimeHeader => dataDirectories[14].Size;

        /// <summary>
        /// Creates a new <see cref="OptionalHeader"/> instance from a stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>A new <see cref="OptionalHeader"/> instance read from the stream.</returns>
        public static OptionalHeader FromStream(Stream stream)
        {
            OptionalHeader header = new OptionalHeader();

            header.Magic = stream.ReadUInt16();
            header.MajorLinkerVersion = stream.ReadUInt8();
            header.MinorLinkerVersion = stream.ReadUInt8();
            header.SizeOfCode = stream.ReadUInt32();
            header.SizeOfInitializedData = stream.ReadUInt32();
            header.SizeOfUninitializedData = stream.ReadUInt32();
            header.AddressOfEntryPoint = stream.ReadUInt32();
            header.BaseOfCode = stream.ReadUInt32();
            header.BaseOfData = (header.Magic == Hdr64Magic) ? 0 : stream.ReadUInt32();
            header.ImageBase = (header.Magic == Hdr64Magic) ? stream.ReadUInt64() : stream.ReadUInt32();
            header.SectionAlignment = stream.ReadUInt32();
            header.FileAlignment = stream.ReadUInt32();
            header.MajorOperatingSystemVersion = stream.ReadUInt16();
            header.MinorOperatingSystemVersion = stream.ReadUInt16();
            header.MajorImageVersion = stream.ReadUInt16();
            header.MinorImageVersion = stream.ReadUInt16();
            header.MajorSubsystemVersion = stream.ReadUInt16();
            header.MinorSubsystemVersion = stream.ReadUInt16();
            header.Win32VersionValue = stream.ReadUInt32();
            header.SizeOfImage = stream.ReadUInt32();
            header.SizeOfHeaders = stream.ReadUInt32();
            header.Checksum = stream.ReadUInt32();
            header.Subsystem = stream.ReadUInt16();
            header.DllCharacteristics = stream.ReadUInt16();
            header.SizeOfStackReserve = (header.Magic == Hdr64Magic) ? stream.ReadUInt64() : stream.ReadUInt32();
            header.SizeOfStackCommit = (header.Magic == Hdr64Magic) ? stream.ReadUInt64() : stream.ReadUInt32();
            header.SizeOfHeapReserve = (header.Magic == Hdr64Magic) ? stream.ReadUInt64() : stream.ReadUInt32();
            header.SizeOfHeapCommit = (header.Magic == Hdr64Magic) ? stream.ReadUInt64() : stream.ReadUInt32();
            header.LoaderFlags = stream.ReadUInt32();
            header.NumberOfRvaAndSizes = stream.ReadUInt32();

            header.dataDirectories = new DataDirectory[header.NumberOfRvaAndSizes];
            for (int i = 0; i < header.NumberOfRvaAndSizes; i++)
            {
                header.dataDirectories[i] = DataDirectory.FromStream(stream);
            }

            return header;
        }
    }
}
