﻿// Here is my file for QueueInterface with the purpose of replicating the code in QueueInterface.java but in C#.
// Written By: Alex Bishop
// Last Edit: 10/9/2018
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
        /// <summary>
        /// Default call of the Exception
        /// </summary>
        public QueueUnderflowException()
            : base()
        {
        }

        /// <summary>
        /// Call of the Exception that allows a custom message
        /// </summary>
        /// <param name="message"></param>
        public QueueUnderflowException(string message)
            : base(message)
        {
        }
    }
}
