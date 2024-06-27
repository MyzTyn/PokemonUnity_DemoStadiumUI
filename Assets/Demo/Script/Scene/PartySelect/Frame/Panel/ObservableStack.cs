using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PokemonUnity.Stadium
{
    // Custom EventArgs for stack change notifications
    public class StackChangedEventArgs<T> : EventArgs
    {
        // Enum for stack operations
        public enum OperationType
        {
            Push,
            Pop,
            Update
        }

        // Properties
        public OperationType Operation { get; }
        public Stack<T> Stack { get; }

        // Constructor
        public StackChangedEventArgs(OperationType operation, Stack<T> stack)
        {
            Operation = operation;
            Stack = stack;
        }
    }

    // Observable stack class
    public class ObservableStack<T> : Stack<T>, IObservable<StackChangedEventArgs<T>> where T : class
    {
        private readonly List<IObserver<StackChangedEventArgs<T>>> observers = new List<IObserver<StackChangedEventArgs<T>>>();

        // Subscribe method
        public IDisposable Subscribe(IObserver<StackChangedEventArgs<T>> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
                observer.OnNext(new StackChangedEventArgs<T>(StackChangedEventArgs<T>.OperationType.Update, this));
            }

            return new Unsubscriber(observers, observer);
        }

        public T this[int index]
        {
            get
            {
                return this.ElementAt(index);
            }
        }

        // Push method
        public new T Push(T obj)
        {
            base.Push(obj);
            NotifyObservers(StackChangedEventArgs<T>.OperationType.Push);
            return obj;
        }

        // Pop method
        public new T Pop()
        {
            var obj = base.Pop();
            NotifyObservers(StackChangedEventArgs<T>.OperationType.Pop);
            return obj;
        }

        // Notify observers
        private void NotifyObservers(StackChangedEventArgs<T>.OperationType operationType)
        {
            var eventArgs = new StackChangedEventArgs<T>(operationType, this);

            foreach (var observer in observers.ToArray()) // ToArray to avoid modification during enumeration
            {
                observer.OnNext(eventArgs);
            }
        }

        // Unsubscriber class
        private class Unsubscriber : IDisposable
        {
            private readonly List<IObserver<StackChangedEventArgs<T>>> _observers;
            private readonly IObserver<StackChangedEventArgs<T>> _observer;

            public Unsubscriber(List<IObserver<StackChangedEventArgs<T>>> observers, IObserver<StackChangedEventArgs<T>> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }
            }
        }
    }
}
