namespace DeckImplementation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A simple interface for implementing a deque data structure.
    /// </summary>
    /// <typeparam name="T">Sets the element type of the deque.</typeparam>
    public interface IDeque<T>
    {
        /// <summary>
        /// Gets the count of elements in the deque.
        /// </summary>
        /// <value>
        /// Gets the count of the elements in the deque;
        /// </value>
        int Count
        {
            get;
        }

        /// <summary>
        /// Pushes the first element of the deque.
        /// </summary>
        /// <param name="element">The element to push.</param>
        void PushFirst(T element);

        /// <summary>
        /// Pushes the last element of the deque.
        /// </summary>
        /// <param name="element">The element to push.</param>
        void PushLast(T element);

        /// <summary>
        /// Pops the first element of the deque.
        /// </summary>
        /// <returns>Returns the poped element.</returns>
        T PopFirst();

        /// <summary>
        /// Pops the last element of the deque.
        /// </summary>
        /// <returns>Returns the poped element.</returns>
        T PopLast();

        /// <summary>
        /// Peeks the first element of the deque.
        /// </summary>
        /// <returns>Returns the first element of the deque without removing it.</returns>
        T PeekFirst();

        /// <summary>
        /// Peeks the last element of the deque.
        /// </summary>
        /// <returns>Returns the last element of the deque without removing it.</returns>
        T PeekLast();

        /// <summary>
        /// Clears this deque.
        /// </summary>
        void Clear();

        /// <summary>
        /// Determines whether [contains] [the specified element].
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified element]; otherwise, <c>false</c>.
        /// </returns>
        bool Contains(T element);
    }

    /// <summary>
    /// A simple class implementing the <see cref="IDeque"/> interface.
    /// </summary>
    /// <typeparam name="T">Sets the element type of the deque.</typeparam>
    public class Deque<T> : IDeque<T>
    {
        /// <summary>
        /// The inner linked list for the deque implementation.
        /// </summary>
        private LinkedList<T> innerLinkedList;

        /// <summary>
        /// Initializes a new instance of the <see cref="Deque{T}"/> class.
        /// </summary>
        public Deque()
        {
            this.innerLinkedList = new LinkedList<T>();
        }

        /// <summary>
        /// Gets the count of elements in the deque.
        /// </summary>
        /// <value>
        /// Gets the count of the elements in the deque;
        /// </value>
        public int Count
        {
            get
            {
                return this.innerLinkedList.Count;
            }
        }

        /// <summary>
        /// Pushes the first element of the deque.
        /// </summary>
        /// <param name="element">The element to push.</param>
        public void PushFirst(T element)
        {
            this.innerLinkedList.AddFirst(element);
        }

        /// <summary>
        /// Pushes the last element of the deque.
        /// </summary>
        /// <param name="element">The element to push.</param>
        public void PushLast(T element)
        {
            this.innerLinkedList.AddLast(element);
        }

        /// <summary>
        /// Pops the first element of the deque.
        /// </summary>
        /// <returns>Returns the poped element.</returns>
        public T PopFirst()
        {
            if (this.innerLinkedList.Count == 0)
            {
                throw new InvalidOperationException("Deque is empty. No elements to pop.");
            }

            var element = this.innerLinkedList.First();
            this.innerLinkedList.RemoveFirst();
            return element;
        }

        /// <summary>
        /// Pops the last element of the deque.
        /// </summary>
        /// <returns>Returns the poped element.</returns>
        public T PopLast()
        {
            if (this.innerLinkedList.Count == 0)
            {
                throw new InvalidOperationException("Deque is empty. No elements to pop.");
            }

            var element = this.innerLinkedList.Last();
            this.innerLinkedList.RemoveLast();
            return element;
        }

        /// <summary>
        /// Peeks the first element of the deque.
        /// </summary>
        /// <returns>Returns the first element of the deque without removing it.</returns>
        public T PeekFirst()
        {
            if (this.innerLinkedList.Count == 0)
            {
                throw new InvalidOperationException("Deque is empty. No elements to peek.");
            }

            var element = this.innerLinkedList.First();
            return element;
        }

        /// <summary>
        /// Peeks the last element of the deque.
        /// </summary>
        /// <returns>Returns the last element of the deque without removing it.</returns>
        public T PeekLast()
        {
            if (this.innerLinkedList.Count == 0)
            {
                throw new InvalidOperationException("Deque is empty. No elements to peek.");
            }

            var element = this.innerLinkedList.Last();
            return element;
        }

        /// <summary>
        /// Clears this deque.
        /// </summary>
        public void Clear()
        {
            this.innerLinkedList.Clear();
        }

        /// <summary>
        /// Determines whether [contains] [the specified element].
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified element]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(T element)
        {
            return this.innerLinkedList.Contains(element);
        }
    }

    /// <summary>
    /// A test class for our deque
    /// </summary>
    public class Program
    {
        internal static void Main()
        {
            // This is how we initialise a new instace of our deque structure
            Deque<int> simpleDeque = new Deque<int>();

            // This is how we add a element to the front of the deque.
            simpleDeque.PushFirst(20);

            // This is how we add a element to the back of the deque.
            simpleDeque.PushFirst(30);

            // This is how we Peek at our last and first element.
            Console.WriteLine(simpleDeque.PeekLast());
            Console.WriteLine(simpleDeque.PeekFirst());

            // This is how we clear our deque.
            //simpleDeque.Clear();

            // This is how we get the count of our deque.
            Console.WriteLine(simpleDeque.Count);

            // This is how we check if a element is contained in the deque.
            Console.WriteLine(simpleDeque.Contains(22));

            // This is how we pop our elements from the front and the back of the deque
            Console.WriteLine(simpleDeque.PopLast());
            Console.WriteLine(simpleDeque.PopLast());
        }
    }
}
