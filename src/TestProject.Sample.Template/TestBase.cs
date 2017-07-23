namespace $safeprojectname$
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        /// <summary>
        /// Asserts that two given date times are equal.
        /// </summary>
        /// <param name="expected">The expected date time.</param>
        /// <param name="actual">The actual date time.</param>
        public static void AssertDateTimesAreEqual(DateTime expected, DateTime actual)
        {
            string message = $"Expected timestamp {expected: yyyy-MM-dd HH:mm:ss.fff}, but actual timestamp is {actual: yyyy-MM-dd HH:mm:ss.fff}";
            AssertDateTimesAreEqual(expected, actual, TimeSpan.FromSeconds(0), message);
        }

        /// <summary>
        /// Asserts that two given date times are equal.
        /// </summary>
        /// <param name="expected">The expected date time.</param>
        /// <param name="actual">The actual date time.</param>
        /// <param name="message">The message to show in case the assertion does not hold.</param>
        public static void AssertDateTimesAreEqual(DateTime expected, DateTime actual, string message)
        {
            AssertDateTimesAreEqual(expected, actual, TimeSpan.FromSeconds(0), message);
        }

        /// <summary>
        /// Asserts that two given date times are equal.
        /// </summary>
        /// <param name="expected">The expected date time.</param>
        /// <param name="actual">The actual date time.</param>
        /// <param name="delta">The allowed difference between the two date ties.</param>
        public static void AssertDateTimesAreEqual(DateTime expected, DateTime actual, TimeSpan delta)
        {
            string message = $"Expected equal timestamps, but "
                + $"actual timestamp {actual.ToString("yyyy-MM-dd HH:mm:ss.fff")} differs "
                + $"by more than {delta.Milliseconds} milliseconds from "
                + $"expected timestamp {expected.ToString("yyyy-MM-dd HH:mm:ss.fff")}";
            AssertDateTimesAreEqual(expected, actual, delta, message);
        }

        /// <summary>
        /// Asserts that two given date times are equal.
        /// </summary>
        /// <param name="expected">The expected date time.</param>
        /// <param name="actual">The actual date time.</param>
        /// <param name="delta">The allowed difference between the two date ties.</param>
        /// <param name="message">The message to show in case the assertion does not hold.</param>
        public static void AssertDateTimesAreEqual(DateTime expected, DateTime actual, TimeSpan delta, string message)
        {
            if (Math.Abs((expected - actual).Ticks) > delta.Ticks)
            {
                Assert.Fail(message);
            }
        }
    }
}