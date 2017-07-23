namespace $safeprojectname$.Extensions
{
    using System.IO;

    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Generates a memory stream from a string.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <returns>A memory stream containing the given string</returns>
        public static Stream AsStream(this string s)
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(memoryStream);
            writer.Write(s);
            writer.Flush();

            // reset stream to start
            memoryStream.Position = 0;
            return memoryStream;
        }

        /// <summary>
        /// Returns the initial part of the current string instance.
        /// </summary>
        /// <param name="s">The string.</param>
        /// <param name="length">The length of the initial part.</param>
        /// <returns>The initial part of the current string instance.</returns>
        public static string Left(this string s, int length)
        {
            if (s.Length < length)
            {
                return s;
            }
            else
            {
                return s.Substring(0, length);
            }
        }
    }
}