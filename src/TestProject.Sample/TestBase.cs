namespace TestProject.Sample
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Base class for test classes, providing some assertions and other utility methods.
    /// </summary>
    [TestClass]
    public class TestBase
    {
        /// <summary>
        /// Asserts that a specified code block throws an exception.
        /// </summary>
        /// <typeparam name="T">The exception type.</typeparam>
        /// <param name="action">The code block.</param>
        /// <returns>The exception that was thrown.</returns>
        public static T AssertThrows<T>(Action action) where T : Exception
        {
            try
            {
                action();
            }
            catch (T e)
            {
                return e;
            }
            catch (Exception e)
            {
                Assert.Fail(
                    "Expected exception of type {0}, but an exception of type {1} was thrown.",
                    typeof(T).Name,
                    e.GetType().Name);
                return null;
            }

            Assert.Fail(
                "Expected exception of type {0}, but no exception was thrown.",
                typeof(T).Name);
            return null;
        }

        /// <summary>
        /// Asserts that a given collection is empty.
        /// </summary>
        /// <typeparam name="T">The element type</typeparam>
        /// <param name="items">The collection to check.</param>
        public static void AssertIsEmpty<T>(IEnumerable<T> items)
        {
            AssertIsEmpty(items, $"Expected empty collection, but there are {items.Count()} items");
        }

        /// <summary>
        /// Asserts that a given collection is empty.
        /// </summary>
        /// <typeparam name="T">The element type</typeparam>
        /// <param name="items">The collection to check.</param>
        /// <param name="message">The description given in case the assertion fails.</param>
        public static void AssertIsEmpty<T>(IEnumerable<T> items, string message)
        {
            if (!items.Any())
            {
                Assert.Fail(message);
            }
        }
    }
}
