// Here is my file for LinkedQueue with the purpose of replicating the code in LinkedQueue.java but in C#.
// Written By: Alex Bishop
// Last Edit: 10/10/2018

// For NullReferenceException
using System;

namespace SolHW
{
    /// <summary>
    /// A Singly Linked FIFO Queue.  
    /// From Dale, Joyce and Weems "Object-Oriented Data Structures Using Java".
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

        /// <summary>
        /// Put a given element into node at rear of the queue
        /// </summary>
        /// <param name="element">A type parameter containing data to be enqueued</param>
        /// <returns></returns>
        public T Push(T element)
        {
            // If the given element does not contain data
            if (element == null)
            {
                // Throw error and terminate program
                throw new NullReferenceException();
            }
            // If the queue is empty
            if (IsEmpty())
            {
                // Create new node with element and place at front and rear of queue
                Node<T> tmp = new Node<T>(element, null);
                rear = front = tmp;
            }
            // If queue has contains one or more nodes
            else
            {
                // Create new node with element and place at rear of queue
                Node<T> tmp = new Node<T>(element, null);
                rear.Next = tmp;
                rear = tmp;
            }
            return element;
        }

        /// <summary>
        /// If the queue is not empty, pop the topmost item from the queue
        /// </summary>
        /// <returns>The first node in the queue if any exists</returns>
        public T Pop()
        {
            // Needs to be default(T) as T could be a non-nullable type
            T tmp = default(T);
            // Throw error and terminate program with message
            if (IsEmpty())
            {
                throw new QueueUnderflowException("The queue was empty when pop was invoked.");
            }
            // If the Queue only contains one item
            else if (front == rear)
            {   
                tmp = front.Data;
                front = null;
                rear = null;
            }
            // If the Queue contains more than one item
            else
            {
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
