using System.IO;
using ExtensionNet.Streams;

namespace ByteLibLoader.PlatformLoaders.PE
{
    /// <summary>
    /// Class representing a data directory
    /// </summary>
    public class DataDirectory
    {
        /// <summary>
        /// Gets or sets the RVA address of the data directory.
        /// </summary>
        public uint Address { get; set; }

        /// <summary>
        /// Gets or sets the size of the data directory.
        /// </summary>
        public uint Size { get; set; }

        /// <summary>
        /// Reads a data directory from the stream.
        /// </summary>
        /// <param name="stream">The stream to read from.</param>
        /// <returns>The data directory read from the stream</returns>
        public static DataDirectory FromStream(Stream stream)
            => new DataDirectory()
            {
                Address = stream.ReadUInt32(),
                Size = stream.ReadUInt32()
            };
    }
}
