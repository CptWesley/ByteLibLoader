using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ByteLibLoader.PlatformLoaders.PE
{
    /// <summary>
    /// Class representing a parsed PE file.
    /// </summary>
    [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "More alligned with specifications.")]
    public class PeFile
    {
        /// <summary>
        /// Gets or sets the dos header.
        /// </summary>
        public DosHeader DosHeader { get; set; }

        /// <summary>
        /// Gets or sets the pe header.
        /// </summary>
        public PeHeader PeHeader { get; set; }

        /// <summary>
        /// Gets or sets the optional header.
        /// </summary>
        public OptionalHeader OptionalHeader { get; set; }

        /// <summary>
        /// Gets or sets the section headers.
        /// </summary>
        public SectionHeader[] SectionHeaders { get; set; }

        /// <summary>
        /// Creates a <see cref="PeFile"/> instance from the given bytes.
        /// </summary>
        /// <param name="bytes">The bytes to parse.</param>
        /// <returns>The PE file contained in the bytes.</returns>
        /// <exception cref="PeParsingException">
        /// Thrown when parsing fails.
        /// </exception>
        public static PeFile Parse(byte[] bytes)
        {
            PeFile result = new PeFile();
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                result.DosHeader = DosHeader.FromStream(ms);
                if (result.DosHeader.Magic != DosHeader.RequiredMagicNumber)
                {
                    throw new PeParsingException("Invalid DOS header signature.");
                }

                ms.Position = result.DosHeader.Lfanew;
                result.PeHeader = PeHeader.FromStream(ms);
                if (result.PeHeader.Signature != PeHeader.RequiredMagicNumber)
                {
                    throw new PeParsingException("Invalid PE header signature.");
                }

                result.OptionalHeader = OptionalHeader.FromStream(ms);
                if (!OptionalHeader.RequiredMagicNumbers.Contains(result.OptionalHeader.Magic))
                {
                    throw new PeParsingException("Invalid PE optional header signature.");
                }

                result.SectionHeaders = new SectionHeader[result.PeHeader.NumberOfSections];
                for (int i = 0; i < result.PeHeader.NumberOfSections; i++)
                {
                    result.SectionHeaders[i] = SectionHeader.FromStream(ms);
                }

                return result;
            }
        }
    }
}
