using System;

namespace ByteLibLoader.PlatformLoaders.PE
{
    /// <summary>
    /// Exception thrown when PE parsing fails.
    /// </summary>
    /// <seealso cref="Exception" />
    public class PeParsingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PeParsingException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public PeParsingException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PeParsingException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public PeParsingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PeParsingException"/> class.
        /// </summary>
        public PeParsingException()
        {
        }
    }
}
