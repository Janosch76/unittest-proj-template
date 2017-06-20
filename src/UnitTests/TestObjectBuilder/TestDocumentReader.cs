namespace TestProject.Sample
{
    using System.IO;

    /// <summary>
    /// Represents a test document
    /// </summary>
    public class TestDocument
    {
        private const string TestDocumentsFolder = "..\\..\\TestDocuments";
        private readonly string path;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestDocument"/> class.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public TestDocument(string filename)
        {
            this.path = Path.Combine(TestDocumentsFolder, filename);
        }

        /// <summary>
        /// Gets the full filename.
        /// </summary>
        public string FullFilename
        {
            get { return this.path; }
        }

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <returns>Returns a file stream.</returns>
        public FileStream Open()
        {
            return new FileStream(this.path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
    }
}
