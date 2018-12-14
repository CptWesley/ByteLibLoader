using System;

namespace ByteLibLoader.PlatformLoaders.PE
{
    /// <summary>
    /// Flags for section header characteristics.
    /// </summary>
    [Flags]
    public enum SectionHeaderCharacteristics
    {
        /// <summary>
        /// Indicates that the section can be discarded.
        /// </summary>
        MemoryDiscardable = 0x02000000,

        /// <summary>
        /// Indicates that the section can not be cached.
        /// </summary>
        MemoryNotCached = 0x04000000,

        /// <summary>
        /// Indicates that the section can not be paged.
        /// </summary>
        MemoryNotPaged = 0x08000000,

        /// <summary>
        /// Indicates that the section can be shared.
        /// </summary>
        MemoryShared = 0x10000000,

        /// <summary>
        /// Indicates that the section can be executed as code.
        /// </summary>
        MemoryExecute = 0x20000000,

        /// <summary>
        /// Indicates that the section can be read.
        /// </summary>
        MemoryRead = 0x40000000,

        /// <summary>
        /// Indicates that the section can be written to.
        /// </summary>
        MemoryWrite = -2147483648,

        /// <summary>
        /// Indicates that the section contains executable code.
        /// </summary>
        ContainsCode = 0x00000020,

        /// <summary>
        /// Indicates that the section contains initiliazed data.
        /// </summary>
        ContainsInitializedData = 0x00000040,

        /// <summary>
        /// Indicates that the section contains uninitiliazed data.
        /// </summary>
        ContainsUninitializedData = 0x00000080
    }
}
