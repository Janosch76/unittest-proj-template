namespace $safeprojectname$
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Event monitor, recording the events raised by a publisher object
    /// </summary>
    /// <seealso cref="System.IDisposable" />
    public class EventMonitor : IDisposable
    {
        private readonly object publisher;
        private readonly List<EventData> events;
        private readonly List<Delegate> registeredHandlers;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventMonitor"/> class.
        /// </summary>
        /// <param name="publisher">The publisher that is monitored.</param>
        public EventMonitor(object publisher)
        {
            this.publisher = publisher;
            this.events = new List<EventData>();
            this.registeredHandlers = new List<Delegate>();

            RegisterHandlers();
        }

        /// <summary>
        /// Gets the events raised by the publisher.
        /// </summary>
        public IEnumerable<EventData> Events
        {
            get { return this.events; }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            UnregisterHandlers();
        }

        private void RegisterHandlers()
        {
            MethodInfo method = this.GetType()
                .GetMethod(nameof(AddEventDetails), BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var eventInfo in this.publisher.GetType().GetEvents())
            {
                Delegate handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, this, method);
                eventInfo.AddEventHandler(this.publisher, handler);
                this.registeredHandlers.Add(handler);
            }
        }

        private void UnregisterHandlers()
        {
            foreach (var eventInfo in this.publisher.GetType().GetEvents())
            {
                foreach (var handler in this.registeredHandlers)
                {
                    eventInfo.RemoveEventHandler(this.publisher, handler);
                }
            }
        }

        private void AddEventDetails(object sender, EventArgs e)
        {
            this.events.Add(new EventData(sender, e));
        }

        /// <summary>
        /// Holds the data about a raised event.
        /// </summary>
        public class EventData
        {
            private readonly object sender;
            private readonly EventArgs eventArgs;

            /// <summary>
            /// Initializes a new instance of the <see cref="EventData"/> class.
            /// </summary>
            /// <param name="sender">The sender.</param>
            /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
            public EventData(object sender, EventArgs e)
            {
                this.sender = sender;
                this.eventArgs = e;
            }

            /// <summary>
            /// Gets the event publisher.
            /// </summary>
            public object Sender
            {
                get { return this.sender; }
            }

            /// <summary>
            /// Gets the event arguments.
            /// </summary>
            public EventArgs EventArgs
            {
                get { return this.eventArgs; }
            }
        }
    }
}