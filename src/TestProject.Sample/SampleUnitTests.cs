namespace TestProject.Sample
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
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
            var numbers = new int[] { 1, 2, 3 };

            AssertThrows<IndexOutOfRangeException>(() =>
            {
                var result = numbers[3];
            });
        }

        /// <summary>
        /// A unit test, testing asynchronous code.
        /// </summary>
        [UnitTest]
        [TestMethod]
        public async Task TestMethod4()
        {
            var result = await ReturnAsync(1);

            Assert.AreEqual(1, result);
        }

        /// <summary>
        /// A unit test, asserting that the tested asynchronous statement throws a specified assertion.
        /// </summary>
        [UnitTest]
        [TestMethod]
        public async Task TestMethod5()
        {
            var numbers = new int[] { 1, 2, 3 };

            await AssertThrowsAsync<IndexOutOfRangeException>(async () =>
            {
                var i = await ReturnAsync(3);
                var result = numbers[i];
            });
        }

        /// <summary>
        /// A unit test, asserting that the tested statement raises an event.
        /// </summary>
        [UnitTest]
        [TestMethod]
        public void TestMethod6()
        {
            var customer = a.Customer
                .WithFirstname("John")
                .WithLastname("Doe")
                .Create();

            AssertRaisesPropertyChangedEvents(
                customer,
                new string[] { nameof(customer.FirstName), nameof(customer.LastName) },
                () =>
                {
                    customer.FirstName = "Michael";
                    customer.LastName = "Dee";
                });
        }

        private async Task<int> ReturnAsync(int value)
        {
            await Task.Delay(10);
            return value;
        }
    }
}
