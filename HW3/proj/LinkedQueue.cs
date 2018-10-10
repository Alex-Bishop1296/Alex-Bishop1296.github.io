// Here is my file for LinkedQueue with the purpose of replicating the code in LinkedQueue.java but in C#.
// Written By: Alex Bishop
// Last Edit: 10/10/2018

// For NullReferenceException
using System;

namespace SolHW
{
    /// <summary>
    /// A Singly Linked FIFO Queue.  
    /// From Dale, Joyce and Weems "Object-Oriented Data Structures Using Java"
    /// Converted to the C# language
    /// </summary>
    /// <typeparam name="T">Type Parameter for stored Node data</typeparam>
    class LinkedQueue<T> : IQueueInterface<T>
    {
        private Node<T> front;
        private Node<T> rear;

        /// <summary>
        /// Default constructor for Linked Queue
        /// </summary>
        public LinkedQueue()
        {
            front = null;
            rear = null;
        }

        public T Push(T element)
        {
            if (element == null)
            {
                throw new NullReferenceException();
            }

            if (IsEmpty())
            {
                Node<T> tmp = new Node<T>(element, null);
                rear = front = tmp;
            }
            else
            {
                // General case
                Node<T> tmp = new Node<T>(element, null);
                rear.Next = tmp;
                rear = tmp;
            }
            return element;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            // Needs to be default(T) as T could be a non-nullable type
            T tmp = default(T);
            if (IsEmpty())
            {
                throw new QueueUnderflowException("The queue was empty when pop was invoked.");
            }
            else if (front == rear)
            {   // one item in queue
                tmp = front.Data;
                front = null;
                rear = null;
            }
            else
            {
                // General case
                tmp = front.Data;
                front = front.Next;
            }

            return tmp;
        }

        /// <summary>
        /// Checks if the queue is empty
        /// </summary>
        /// <returns>True if empty, false if the queue contains data</returns>
        public bool IsEmpty()
        {
            // If both head and tail contain no data
            if (front == null && rear == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
