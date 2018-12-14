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
        /// Gets or sets the address of the export table.
        /// </summary>
        public uint ExportTable { get; set; }

        /// <summary>
        /// Gets or sets the size of the export table.
        /// </summary>
        public uint SizeOfExportTable { get; set; }

        /// <summary>
        /// Gets or sets the address of the import table.
        /// </summary>
        public uint ImportTable { get; set; }

        /// <summary>
        /// Gets or sets the size of the import table.
        /// </summary>
        public uint SizeOfImportTable { get; set; }

        /// <summary>
        /// Gets or sets the address of the resource table.
        /// </summary>
        public uint ResourceTable { get; set; }

        /// <summary>
        /// Gets or sets the size of the resource table.
        /// </summary>
        public uint SizeOfResourceTable { get; set; }

        /// <summary>
        /// Gets or sets the address of the exception table.
        /// </summary>
        public uint ExceptionTable { get; set; }

        /// <summary>
        /// Gets or sets the size of the exception table.
        /// </summary>
        public uint SizeOfExceptionTable { get; set; }

        /// <summary>
        /// Gets or sets the address of the certificate table.
        /// </summary>
        public uint CertificateTable { get; set; }

        /// <summary>
        /// Gets or sets the size of the certificate table.
        /// </summary>
        public uint SizeOfCertificateTable { get; set; }

        /// <summary>
        /// Gets or sets the address of the base relocation table.
        /// </summary>
        public uint BaseRelocationTable { get; set; }

        /// <summary>
        /// Gets or sets the size of the base relocation table.
        /// </summary>
        public uint SizeOfBaseRelocationTable { get; set; }

        /// <summary>
        /// Gets or sets the address of the debug information.
        /// </summary>
        public uint Debug { get; set; }

        /// <summary>
        /// Gets or sets the size of the debug information.
        /// </summary>
        public uint SizeOfDebug { get; set; }

        /// <summary>
        /// Gets or sets the address of the architecture data.
        /// </summary>
        public uint ArchitectureData { get; set; }

        /// <summary>
        /// Gets or sets the size of the architecture data.
        /// </summary>
        public uint SizeOfArchitectureData { get; set; }

        /// <summary>
        /// Gets or sets the address of the global pointer (has no size).
        /// </summary>
        public uint GlobalPtr { get; set; }

        /// <summary>
        /// Gets or sets the address of the TLS table.
        /// </summary>
        public uint TlsTable { get; set; }

        /// <summary>
        /// Gets or sets the size of the TLS table.
        /// </summary>
        public uint SizeOfTlsTable { get; set; }

        /// <summary>
        /// Gets or sets the address of the load config table.
        /// </summary>
        public uint LoadConfigTable { get; set; }

        /// <summary>
        /// Gets or sets the size of the load config table.
        /// </summary>
        public uint SizeOfLoadConfigTable { get; set; }

        /// <summary>
        /// Gets or sets the address of the bound import.
        /// </summary>
        public uint BoundImport { get; set; }

        /// <summary>
        /// Gets or sets the size of the bound import.
        /// </summary>
        public uint SizeOfBoundImport { get; set; }

        /// <summary>
        /// Gets or sets the address of the import address table.
        /// </summary>
        public uint ImportAddressTable { get; set; }

        /// <summary>
        /// Gets or sets the size of the import address table.
        /// </summary>
        public uint SizeOfImportAddressTable { get; set; }

        /// <summary>
        /// Gets or sets the address of the delay import descriptor.
        /// </summary>
        public uint DelayImportDescriptor { get; set; }

        /// <summary>
        /// Gets or sets the size of the delay import descriptor.
        /// </summary>
        public uint SizeOfDelayImportDescriptor { get; set; }

        /// <summary>
        /// Gets or sets the address of the CLR runtime header.
        /// </summary>
        public uint ClrRuntimeHJeader { get; set; }

        /// <summary>
        /// Gets or sets the size of the CLR runtime header.
        /// </summary>
        public uint SizeOfClrRuntimeHJeaderr { get; set; }

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
            header.ExportTable = stream.ReadUInt32();
            header.SizeOfExportTable = stream.ReadUInt32();
            header.ImportTable = stream.ReadUInt32();
            header.SizeOfImportTable = stream.ReadUInt32();
            header.ResourceTable = stream.ReadUInt32();
            header.SizeOfResourceTable = stream.ReadUInt32();
            header.ExceptionTable = stream.ReadUInt32();
            header.SizeOfExceptionTable = stream.ReadUInt32();
            header.CertificateTable = stream.ReadUInt32();
            header.SizeOfCertificateTable = stream.ReadUInt32();
            header.BaseRelocationTable = stream.ReadUInt32();
            header.SizeOfBaseRelocationTable = stream.ReadUInt32();
            header.Debug = stream.ReadUInt32();
            header.SizeOfDebug = stream.ReadUInt32();
            header.ArchitectureData = stream.ReadUInt32();
            header.SizeOfArchitectureData = stream.ReadUInt32();
            header.GlobalPtr = stream.ReadUInt32();
            stream.ReadUInt32(); // Zeroes filled.
            header.TlsTable = stream.ReadUInt32();
            header.SizeOfTlsTable = stream.ReadUInt32();
            header.LoadConfigTable = stream.ReadUInt32();
            header.SizeOfLoadConfigTable = stream.ReadUInt32();
            header.BoundImport = stream.ReadUInt32();
            header.SizeOfBoundImport = stream.ReadUInt32();
            header.ImportAddressTable = stream.ReadUInt32();
            header.SizeOfImportAddressTable = stream.ReadUInt32();
            header.DelayImportDescriptor = stream.ReadUInt32();
            header.SizeOfDelayImportDescriptor = stream.ReadUInt32();
            header.ClrRuntimeHJeader = stream.ReadUInt32();
            header.SizeOfClrRuntimeHJeaderr = stream.ReadUInt32();

            return header;
        }
    }
}
