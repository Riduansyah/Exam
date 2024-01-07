using System;

namespace MemoryLeakExample
{
    class ProgramLima
    {
        static void Main(string[] args)
        {
            var publisher = new EventPublisher();
            while (true)
            {
                var subscriber = new EventSubscriber(publisher);
                // do something with the publisher and subscriber objects
                subscriber.Dispose();//
            }
        }

        class EventPublisher
        {
            public event EventHandler MyEvent;
            public void RaiseEvent()
            {
                MyEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        class EventSubscriber : IDisposable
        {
            private readonly EventPublisher eventPublisher;
            private bool disposedValue = false;

            public EventSubscriber(EventPublisher publisher)
            {
                eventPublisher = publisher;
                publisher.MyEvent += OnMyEvent;
            }

            private void OnMyEvent(object sender, EventArgs e)
            {
                Console.WriteLine("MyEvent raised");
            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposedValue) return;

                if (disposing)
                {
                    eventPublisher.MyEvent -= OnMyEvent;
                }

                disposedValue = true;
            }

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }
        }
    }
}
