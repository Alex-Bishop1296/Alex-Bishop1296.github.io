// allows the use of exception
using System;

namespace proj
{
    /// <summary>
    ///  custom unchecked exception to represent situations where 
    ///  an illegal operation was performed on an empty queue.
    /// </summary>
    class QueueUnderflowException : Exception
    {

        public QueueUnderflowException()
            : base()
        {
        }

        public QueueUnderflowException(string message)
            : base(message)
        {
        }
    }
}
