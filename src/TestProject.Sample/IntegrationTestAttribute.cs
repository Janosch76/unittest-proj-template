namespace TestProject.Sample
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Attribute to mark a <see cref="Microsoft.VisualStudio.TestTools.UnitTesting"/> as integration test
    /// </summary>
    /// <seealso cref="Microsoft.VisualStudio.TestTools.UnitTesting.TestCategoryBaseAttribute" />
    public class IntegrationTestAttribute : TestCategoryBaseAttribute
    {
        /// <summary>
        /// Gets the test category name.
        /// </summary>
        public override IList<string> TestCategories
        {
            get
            {
                return new List<string>() { "IntegrationTest" };
            }
        }
    }
}
