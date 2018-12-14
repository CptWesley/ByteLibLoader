using System.IO;
using ExtensionNet.Streams;

namespace ByteLibLoader.PlatformLoaders.PE
{
    /// <summary>
    /// Represents a parsed section header in a PE file.
    /// </summary>
    public class SectionHeader
    {
        /// <summary>
        /// Gets or sets the name of the section.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the virtual size of the section.
        /// </summary>
        public uint VirtualSize { get; set; }

        /// <summary>
        /// Gets or sets the virtual address of the section.
        /// </summary>
        public uint VirtualAddress { get; set; }

        /// <summary>
        /// Gets or sets the size of the raw data of the section.
        /// </summary>
        public uint SizeOfRawData { get; set; }

        /// <summary>
        /// Gets or sets the pointer to the raw data of the section.
        /// </summary>
        public uint PointerToRawData { get; set; }

        /// <summary>
        /// Gets or sets the pointer to the relocations table of the section.
        /// </summary>
        public uint PointerToRelocations { get; set; }

        /// <summary>
        /// Gets or sets the pointer to the line number table of the section.
        /// </summary>
        public uint PointerToLinenumbers { get; set; }

        /// <summary>
        /// Gets or sets the size of the relocation table of the section.
        /// </summary>
        public ushort NumberOfRelocations { get; set; }

        /// <summary>
        /// Gets or sets the size of the line number table of the section.
        /// </summary>
        public ushort NumberOfLinenumbers { get; set; }

        /// <summary>
        /// Gets or sets the characteristics of the section.
        /// </summary>
        public SectionHeaderCharacteristics Characteristics { get; set; }

        /// <summary>
        /// Reads a section header from the stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>The section header read from stream.</returns>
        public static SectionHeader FromStream(Stream stream)
            => new SectionHeader()
            {
                Name = stream.ReadString(8),
                VirtualSize = stream.ReadUInt32(),
                VirtualAddress = stream.ReadUInt32(),
                SizeOfRawData = stream.ReadUInt32(),
                PointerToRawData = stream.ReadUInt32(),
                PointerToRelocations = stream.ReadUInt32(),
                PointerToLinenumbers = stream.ReadUInt32(),
                NumberOfRelocations = stream.ReadUInt16(),
                NumberOfLinenumbers = stream.ReadUInt16(),
                Characteristics = (SectionHeaderCharacteristics)stream.ReadUInt32()
            };
    }
}
