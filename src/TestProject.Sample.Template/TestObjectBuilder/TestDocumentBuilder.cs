namespace $safeprojectname$.TestObjectBuilder
{
    /// <summary>
    /// Gets a test document
    /// </summary>
    public class TestDocumentBuilder
    {
        /// <summary>
        /// Gets a TXT document with 2 lines of text.
        /// </summary>
        public TestDocument OfTypeTxt
        {
            get { return new TestDocument("document1.txt"); }
        }

        /// <summary>
        /// Gets a CSV document with 4 rows of 3 columns each.
        /// </summary>
        public TestDocument OfTypeCsv
        {
            get { return new TestDocument("document2.csv"); }
        }
    }
}
