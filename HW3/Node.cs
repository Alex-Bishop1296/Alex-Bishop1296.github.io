// Here is my file for Node.cs with the purpose of replicating the code in Node.java but in C#.
// Written By: Alex Bishop
// Last Edit: 10/7/2018
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
    public T data;
    /// <summary>
    /// Pointer for next Node in sequence
    /// </summary>
    public Node<T> next;


    /// <summary>
    /// Generic Constructor when no data is given
    /// </summary>
    public Node()
    {
        this.data = null;
        this.next = null;
    }

    /// <summary>
    /// Constructor when only data is given
    /// </summary>
    /// <param name="data">Type Parameter for stored Node data</param>
    /// <remark>
    /// We will always use this one in our code, others are placeholders for expansion
    /// </remark>
    public Node(T data)
    {
        this.data = data;
        this.next = null;
    }

    /// <summary>
    /// Constructor for when the node is made, and data and next are given
    /// </summary>
    /// <param name="data">Type Parameter for stored Node data</param>
    /// <param name="next">Pointer for next Node in sequence</param>
    public Node(T data, Node<T> next)
    {
        this.data = data;
        this.next = next;
    }
}