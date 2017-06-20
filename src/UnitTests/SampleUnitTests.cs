namespace TestProject.Sample
{
    using System;
    using System.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestProject.Sample.TestObjectBuilder;

    [TestClass]
    public class SampleUnitTests : TestBase
    {
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
