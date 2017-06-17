namespace UnitTests.TestUtils
{
    using System;
    using System.IO;

    /// <summary>
    /// Creates a temporary folder
    /// </summary>
    public class TempFolder : IDisposable
    {
        private readonly string path;

        /// <summary>
        /// Initializes a new instance of the <see cref="TempFolder"/> class.
        /// </summary>
        public TempFolder()
        {
            var foldername = Guid.NewGuid().ToString().Replace("-", string.Empty);
            this.path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), foldername);
            Directory.CreateDirectory(this.path);
        }

        /// <summary>
        /// Gets the path of the temporary folder.
        /// </summary>
        public string Path
        {
            get { return this.path; }
        }

        /// <summary>
        /// Deletes the temporary folder.
        /// </summary>
        public void Dispose()
        {
            try
            {
                Directory.Delete(this.path);
            }
            catch
            {
            }
        }
    }
}
