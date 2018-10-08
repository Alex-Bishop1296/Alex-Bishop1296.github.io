// Here is my file for Node.cs with the purpose of replicating the code in Node.java but in C#.
// Written By: Alex Bishop
// Last Edit: 10/8/2018


namespace SolHW
{
    /// <summary>
    /// Singly Linked Node class
    /// </summary>
    /// <remark>
    /// This is just the cointainer for the data, not actually a controllable hierarchy, but can be used in a framework for one. 
    /// </remark>
    public class Node<T>
    {
        /// <summary>
        /// Type Parameter for stored Node data
        /// </summary>
        public T Data;
        /// <summary>
        /// Pointer for next Node in sequence
        /// </summary>
        public Node<T> Next;


        /// <summary>
        /// Constructor for when the node is made, and data and next are given
        /// </summary>
        /// <param name="data">Type Parameter for stored Node data</param>
        /// <param name="next">Pointer for next Node in sequence</param>
        public Node(T data, Node<T> next)
        {
            this.Data = data;
            this.Next = next;
        }
    }
}