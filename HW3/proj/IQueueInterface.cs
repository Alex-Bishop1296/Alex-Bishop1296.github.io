// Here is my file for QueueInterface with the purpose of replicating the code in QueueInterface.java but in C#.
// Written By: Alex Bishop
// Last Edit: 10/8/2018

namespace SolHW
{   ///<summary>
    /// A FIFO (First In First Out) queue interface. This ADT
    /// (Abstract Data Type) is suitable for a singly linke queue.
    /// </summary>
    public interface IQueueInterface<T>
    {
        ///<summary>
        ///Add an element to the rear of the queue
        ///</summary>
        ///<returns> Returns the element that was enqueued</returns>
        T Push(T element);

        ///<summary>
        ///Add an element to the rear of the queue
        ///</summary>
        /// <exception cref="QueueUnderflowException">Why it's thrown.</exception>
        T Pop();

        /// <summary>
        /// Test if the queue is empty
        /// </summary>
        /// <returns>True if the queue is empty; false otherwise</returns>
        bool IsEmpty();
    }
}