namespace $safeprojectname$
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
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
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="action">The code block.</param>
        /// <param name="allowDerivedExceptionTypes">A flag indicating whether derived exception types are allowed or not.</param>
        /// <returns>The exception that was thrown.</returns>
        public static TException AssertThrows<TException>(Action action, bool allowDerivedExceptionTypes = true) where TException : Exception
        {
            try
            {
                action();
                Assert.Fail(
                    "Expected exception of type {0}, but no exception was thrown.",
                    typeof(TException).Name);
                return null;
            }
            catch (Exception e)
            {
                if (allowDerivedExceptionTypes && !(e is TException))
                {
                    Assert.Fail(
                        "Expected exception of type {0} or derived type, but an exception of type {1} was thrown.",
                        typeof(TException).Name,
                        e.GetType().Name);
                    return null;
                }
                else if (!allowDerivedExceptionTypes && e.GetType() != typeof(TException))
                {
                    Assert.Fail(
                        "Expected exception of type {0}, but an exception of type {1} was thrown.",
                        typeof(TException).Name,
                        e.GetType().Name);
                    return null;
                }
                else
                {
                    return e as TException;
                }
            }
        }

        /// <summary>
        /// Asserts that a specified asynchronous code block throws an exception.
        /// </summary>
        /// <typeparam name="TException">The exception type.</typeparam>
        /// <param name="action">The code block.</param>
        /// <param name="allowDerivedExceptionTypes">A flag indicating whether derived exception types are allowed or not.</param>
        /// <returns>The exception that was thrown.</returns>
        public static async Task<TException> AssertThrowsAsync<TException>(Func<Task> action, bool allowDerivedExceptionTypes = true) where TException : Exception
        {
            try
            {
                await action();
                Assert.Fail(
                    "Expected exception of type {0}, but no exception was thrown.",
                    typeof(TException).Name);
                return null;
            }
            catch (Exception e)
            {
                if (allowDerivedExceptionTypes && !(e is TException))
                {
                    Assert.Fail(
                        "Expected exception of type {0} or derived type, but an exception of type {1} was thrown.",
                        typeof(TException).Name,
                        e.GetType().Name);
                    return null;
                }
                else if (!allowDerivedExceptionTypes && e.GetType() != typeof(TException))
                {
                    Assert.Fail(
                        "Expected exception of type {0}, but an exception of type {1} was thrown.",
                        typeof(TException).Name,
                        e.GetType().Name);
                    return null;
                }
                else
                {
                    return e as TException;
                }
            }
        }

        /// <summary>
        /// Asserts that a given object raises events when executing the given code fragment.
        /// </summary>
        /// <param name="objectUnderTest">The object under test.</param>
        /// <param name="action">The action to execute.</param>
        public static void AssertRaisesEvents(object objectUnderTest, Action action)
        {
            AssertRaisesEvents(objectUnderTest)
                .Where(events => events.Any(), "Expected object under test to raise events, but none were raised.")
                .WhenExecuting(action);
        }

        /// <summary>
        /// Asserts that a given object raises no events when executing the given code fragment.
        /// </summary>
        /// <param name="objectUnderTest">The object under test.</param>
        /// <param name="action">The action to execute.</param>
        public static void AssertRaisesNoEvents(object objectUnderTest, Action action)
        {
            AssertRaisesEvents(objectUnderTest)
                .Where(events => !events.Any(), "Expected object under test to raise no events, but some were raised.")
                .WhenExecuting(action);
        }

        /// <summary>
        /// Asserts that a given object raises PropertyChanged events when executing the given code fragment.
        /// </summary>
        /// <param name="objectUnderTest">The object under test.</param>
        /// <param name="propertyNames">The expected property names.</param>
        /// <param name="action">The action to execute.</param>
        public static void AssertRaisesPropertyChangedEvents(object objectUnderTest, IEnumerable<string> propertyNames, Action action)
        {
            AssertRaisesEvents(objectUnderTest)
                .Where(events => events
                    .Where(e => e.EventArgs is System.ComponentModel.PropertyChangedEventArgs)
                    .Select(e => (e.EventArgs as System.ComponentModel.PropertyChangedEventArgs).PropertyName)
                    .SequenceEqual(propertyNames), 
                    "Expected object under test to raise PropertyChanged events for " + string.Join(", ", propertyNames))
                .WhenExecuting(action);
        }

        /// <summary>
        /// Creates a new <see cref="EventAssertion"/> for a given object under test.
        /// </summary>
        /// <param name="objectUnderTest">The object under test.</param>
        /// <returns>A new event assertion.</returns>
        public static EventAssertion AssertRaisesEvents(object objectUnderTest)
        {
            return new EventAssertion(objectUnderTest);
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

        /// <summary>
        /// Event assertion builder.
        /// </summary>
        public class EventAssertion
        {
            private readonly object objectUnderTest;
            private readonly Dictionary<Predicate<IEnumerable<EventMonitor.EventData>>, string> eventProperties;

            /// <summary>
            /// Initializes a new instance of the <see cref="EventAssertion" /> class.
            /// </summary>
            /// <param name="objectUnderTest">The object under test.</param>
            internal EventAssertion(object objectUnderTest)
            {
                this.objectUnderTest = objectUnderTest;
                this.eventProperties = new Dictionary<Predicate<IEnumerable<EventMonitor.EventData>>, string>();
            }

            /// <summary>
            /// Adds a new property that is required for the observed events.
            /// </summary>
            /// <param name="eventProperty">The event property.</param>
            /// <param name="message">The error message in case the property does not hold.</param>
            /// <returns>The event assertion builder</returns>
            public EventAssertion Where(Predicate<EventMonitor.EventData> eventProperty, string message = null)
            {
                return Where((IEnumerable<EventMonitor.EventData> events) => events.All(e => eventProperty(e)), message);
            }

            /// <summary>
            /// Adds a new property that is required for the observed events.
            /// </summary>
            /// <param name="eventProperty">The event property.</param>
            /// <param name="message">The error message in case the property does not hold.</param>
            /// <returns>The event assertion builder</returns>
            public EventAssertion Where(Predicate<IEnumerable<EventMonitor.EventData>> eventProperty, string message = null)
            {
                if (string.IsNullOrEmpty(message))
                {
                    message = "Expected property does not hold for raised events.";
                }

                this.eventProperties.Add(eventProperty, message);
                return this;
            }

            /// <summary>
            /// Executes a given code fragment and checks that the registered
            /// properties hold for the observed events from the object under test.
            /// </summary>
            /// <param name="action">The action to execute.</param>
            public void WhenExecuting(Action action)
            {
                using (var eventMonitor = new EventMonitor(this.objectUnderTest))
                {
                    action();

                    foreach (var kv in this.eventProperties)
                    {
                        var eventProperty = kv.Key;
                        var message = kv.Value;
                        if (!eventProperty(eventMonitor.Events))
                        {
                            Assert.Fail(message);
                        }
                    }
                }
            }
        }
    }
}
