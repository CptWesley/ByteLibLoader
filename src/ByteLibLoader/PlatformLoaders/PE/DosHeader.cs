using System.Diagnostics.CodeAnalysis;
using System.IO;
using ExtensionNet.Streams;

namespace ByteLibLoader.PlatformLoaders.PE
{
    /// <summary>
    /// Represents a DOS header.
    /// </summary>
    [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "More alligned with specifications.")]
    public class DosHeader
    {
        /// <summary>
        /// The magic number which is expected (MZ in ASCII).
        /// </summary>
        public const ushort RequiredMagicNumber = 0x5A4D;

        /// <summary>
        /// Gets or sets the found magic number.
        /// </summary>
        public ushort Magic { get; set; }

        /// <summary>
        /// Gets or sets the number of bytes on the last page of the file.
        /// </summary>
        public ushort Cblp { get; set; }

        /// <summary>
        /// Gets or sets the number of pages in the file.
        /// </summary>
        public ushort Cp { get; set; }

        /// <summary>
        /// Gets or sets the relocations.
        /// </summary>
        public ushort Crlc { get; set; }

        /// <summary>
        /// Gets or sets the paragraph header size.
        /// </summary>
        public ushort Cparhdr { get; set; }

        /// <summary>
        /// Gets or sets the amount of minimum extra paragraphs needed.
        /// </summary>
        public ushort MinAlloc { get; set; }

        /// <summary>
        /// Gets or sets the amount of maximum extra paragraphs needed.
        /// </summary>
        public ushort MaxAlloc { get; set; }

        /// <summary>
        /// Gets or sets the initial SS value.
        /// </summary>
        public ushort Ss { get; set; }

        /// <summary>
        /// Gets or sets the initial SP value.
        /// </summary>
        public ushort Sp { get; set; }

        /// <summary>
        /// Gets or sets the checksum.
        /// </summary>
        public ushort Csum { get; set; }

        /// <summary>
        /// Gets or sets the initial IP value.
        /// </summary>
        public ushort Ip { get; set; }

        /// <summary>
        /// Gets or sets the initial CS value.
        /// </summary>
        public ushort Cs { get; set; }

        /// <summary>
        /// Gets or sets the file adress of the relocation table.
        /// </summary>
        public ushort Lfarlc { get; set; }

        /// <summary>
        /// Gets or sets the overlay number.
        /// </summary>
        public ushort Ovno { get; set; }

        /// <summary>
        /// Gets or sets the first 4 reserved words.
        /// </summary>
        public ushort[] Res { get; set; }

        /// <summary>
        /// Gets or sets the OEM identifier.
        /// </summary>
        public ushort OemId { get; set; }

        /// <summary>
        /// Gets or sets the OEM information.
        /// </summary>
        public ushort OemInfo { get; set; }

        /// <summary>
        /// Gets or sets the second 10 reserved words.
        /// </summary>
        public ushort[] Res2 { get; set; }

        /// <summary>
        /// Gets or sets the file adress of the new exe header.
        /// </summary>
        public uint Lfanew { get; set; }

        /// <summary>
        /// Creates a new <see cref="DosHeader"/> instance from a stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>A new <see cref="DosHeader"/> instance read from the stream.</returns>
        public static DosHeader FromStream(Stream stream)
            => new DosHeader()
            {
                Magic = stream.ReadUInt16(),
                Cblp = stream.ReadUInt16(),
                Cp = stream.ReadUInt16(),
                Crlc = stream.ReadUInt16(),
                Cparhdr = stream.ReadUInt16(),
                MinAlloc = stream.ReadUInt16(),
                MaxAlloc = stream.ReadUInt16(),
                Ss = stream.ReadUInt16(),
                Sp = stream.ReadUInt16(),
                Csum = stream.ReadUInt16(),
                Ip = stream.ReadUInt16(),
                Cs = stream.ReadUInt16(),
                Lfarlc = stream.ReadUInt16(),
                Ovno = stream.ReadUInt16(),
                Res = stream.ReadUInt16(4),
                OemId = stream.ReadUInt16(),
                OemInfo = stream.ReadUInt16(),
                Res2 = stream.ReadUInt16(10),
                Lfanew = stream.ReadUInt32()
            };
    }
}
