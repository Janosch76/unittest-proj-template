namespace TestProject.Sample.TestObjectBuilder
{
    using System;

    /// <summary>
    /// Test object builder
    /// </summary>
    public static class a
    {
        /// <summary>
        /// Gets a test document.
        /// </summary>
        public static TestDocumentBuilder TestDocument
        {
            get { return new TestDocumentBuilder(); }
        }

        /// <summary>
        /// Gets a new temporary folder.
        /// </summary>
        public static TempFolder TempFolder
        {
            get { return new TempFolder(); }
        }

        /// <summary>
        /// Gets a customer instance.
        /// </summary>
        public static CustomerBuilder Customer
        {
            get { return new CustomerBuilder(); }
        }
    }
}
