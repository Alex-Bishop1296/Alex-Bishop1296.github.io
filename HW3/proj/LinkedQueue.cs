// Here is my file for LinkedQueue with the purpose of replicating the code in LinkedQueue.java but in C#.
// Written By: Alex Bishop
// Last Edit: 10/10/2018

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

            return element;
        }
    }
}
