using System;

namespace ByteLibLoader.PlatformLoaders.PE
{
    /// <summary>
    /// Flags for PE header characteristics.
    /// </summary>
    [Flags]
    public enum Characteristics
    {
        /// <summary>
        /// Indicates that the relocation information was stripped from the file.
        /// </summary>
        RelocsStripped = 0x0001,

        /// <summary>
        /// The file is executable.
        /// </summary>
        ExecutableImage = 0x0002,

        /// <summary>
        /// PE line numbers were stripped from the file.
        /// </summary>
        LineNumsStripped = 0x0004,

        /// <summary>
        /// PE symbol table was stripped from the file.
        /// </summary>
        LocalSymsStripped = 0x0008,

        /// <summary>
        /// Obsolete.
        /// </summary>
        AggresiveWsTrim = 0x0010,

        /// <summary>
        /// The application can handle files larger than 2 GB.
        /// </summary>
        LargeAddressAware = 0x0020,

        /// <summary>
        /// Obsolete.
        /// </summary>
        BytesReversedLo = 0x0080,

        /// <summary>
        /// The computer supports 32 bit words.
        /// </summary>
        Bit32Machine = 0x0100,

        /// <summary>
        /// The debug information was stripped from the file.
        /// </summary>
        DebugStripped = 0x0200,

        /// <summary>
        /// If the image is on removeable media, copy it to run it from the swap file.
        /// </summary>
        RemovableFromSwap = 0x0400,

        /// <summary>
        /// If the image is on the network, copy it to run it from the swap file.
        /// </summary>
        NetRunFromSwap = 0x0800,

        /// <summary>
        /// The file is a system file.
        /// </summary>
        System = 0x1000,

        /// <summary>
        /// The file is a DLL file.
        /// </summary>
        Dll = 0x2000,

        /// <summary>
        /// Can only be run on uniprocessor computer.
        /// </summary>
        SystemOnly = 0x4000,

        /// <summary>
        /// Obsolete.
        /// </summary>
        BytesReversedHi = 0x8000
    }
}
