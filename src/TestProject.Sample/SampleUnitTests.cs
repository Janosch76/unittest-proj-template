namespace TestProject.Sample
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestProject.Sample.TestObjectBuilder;

    /// <summary>
    /// A collection of sample unit tests.
    /// </summary>
    [TestClass]
    public class SampleUnitTests : TestBase
    {
        /// <summary>
        /// A unit test using the classic assemble/act/assert pattern, with 
        /// a test object builder to create a domain object for the test.
        /// </summary>
        [UnitTest]
        [TestMethod]
        public void TestMethod1()
        {
            var customer = a.Customer
                .WithBirthdate(new DateTime(1990, 04, 01))
                .Create();

            var result = customer.IsAdult(new DateTime(2008, 03, 31));

            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// A unit test, showing how to use one of the test documents as input.
        /// </summary>
        [UnitTest]
        [TestMethod]
        public void TestMethod2()
        {
            using (var folder = a.TempFolder)
            {
                var txt = a.TestDocument.OfTypeTxt;

                File.Copy(txt.FullFilename, Path.Combine(folder.Path, "copy.txt"));

                Assert.IsTrue(File.Exists(Path.Combine(folder.Path, "copy.txt")));
            }
        }

        /// <summary>
        /// A unit test, asserting that the tested statement throws a specified assertion.
        /// </summary>
        [UnitTest]
        [TestMethod]
        public void TestMethod3()
        {
            AssertThrows<IndexOutOfRangeException>(() =>
            {
                var numbers = new int[] { 1, 2, 3 };
                var result = numbers[3];
            });
        }
    }
}
