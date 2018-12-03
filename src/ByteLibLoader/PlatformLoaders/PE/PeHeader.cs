using System.IO;
using ExtensionNet.Streams;

namespace ByteLibLoader.PlatformLoaders.PE
{
    /// <summary>
    /// Class representing a PE header.
    /// </summary>
    public class PeHeader
    {
        /// <summary>
        /// The magic number which is expected (PE in ASCII).
        /// </summary>
        public const ushort RequiredMagicNumber = 0x4550;

        /// <summary>
        /// Gets or sets the found magic number.
        /// </summary>
        public uint Signature { get; set; }

        /// <summary>
        /// Gets or sets the machine architecture.
        /// </summary>
        public MachineArchitecture Machine { get; set; }

        /// <summary>
        /// Gets or sets the number of sections.
        /// </summary>
        public ushort NumberOfSections { get; set; }

        /// <summary>
        /// Gets or sets the time and date stamp in epoch time.
        /// </summary>
        public uint TimeDateStamp { get; set; }

        /// <summary>
        /// Gets or sets the address of the symbol table.
        /// </summary>
        public uint PointerToSymbolTable { get; set; }

        /// <summary>
        /// Gets or sets the size of the symbol table.
        /// </summary>
        public uint NumberOfSymbolTable { get; set; }

        /// <summary>
        /// Gets or sets the time and date stamp in epoch time.
        /// </summary>
        public ushort SizeOfOptionalHeader { get; set; }

        /// <summary>
        /// Gets or sets the characteristics.
        /// </summary>
        public Characteristics Characteristics { get; set; }

        /// <summary>
        /// Creates a new <see cref="PeHeader"/> instance from a stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>A new <see cref="PeHeader"/> instance read from the stream.</returns>
        public static PeHeader FromStream(Stream stream)
            => new PeHeader()
            {
                Signature = stream.ReadUInt32(),
                Machine = (MachineArchitecture)stream.ReadUInt16(),
                NumberOfSections = stream.ReadUInt16(),
                TimeDateStamp = stream.ReadUInt32(),
                PointerToSymbolTable = stream.ReadUInt32(),
                NumberOfSymbolTable = stream.ReadUInt32(),
                SizeOfOptionalHeader = stream.ReadUInt16(),
                Characteristics = (Characteristics)stream.ReadUInt16(),
            };
    }
}
