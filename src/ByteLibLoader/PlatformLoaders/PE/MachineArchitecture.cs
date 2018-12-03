namespace ByteLibLoader.PlatformLoaders.PE
{
    /// <summary>
    /// Enumaration of possible architecture types.
    /// </summary>
    public enum MachineArchitecture
    {
        /// <summary>
        /// Standard x86 architecture.
        /// </summary>
        I386 = 0x014c,

        /// <summary>
        /// Intel Itanium architecture.
        /// </summary>
        IA64 = 0x0200,

        /// <summary>
        /// Standard x64 architecture.
        /// </summary>
        AMD64 = 0x8664
    }
}
