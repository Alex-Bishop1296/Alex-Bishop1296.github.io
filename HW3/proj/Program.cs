// Here is my file for Program with the purpose of replicating the code in Main.java but in C#.
// Written By: Alex Bishop
// Last Edit: 10/10/2018

// For linked list generic object
using System.Collections.Generic;
// For StringBuilder() and .ToString()
using System.Text;
// For Count()
using System.Linq;

namespace SolHW
{
    /// <summary>  
    ///  Print the binary representation of all numbers from 1 up to n.
    ///  This is accomplished by using a FIFO queue to perform a level
    ///  order(i.e.BFS) traversal of a virtual binary tree
    ///  and then storing each "value" in a list as it is "visited".
    /// </summary>
    public class Program
    {
        static LinkedList<string> generateBinaryRepresentationList(int n)
        {
            //This starting section of intializing our lists with default constructors

            // Stringbulder is a dynamic object that allows you to expand the
            // amount of characters in a string
            // Create an empty queue of strings with which to perfrom the traversal
            LinkedQueue<StringBuilder> q = new LinkedQueue<StringBuilder>();

            // A list for returning the binary values
            LinkedList<string> output = new LinkedList<string>();

            // Error checking for bad values
            if(n < 1)
            {
                // binary representation of negative values is not supported
                // return an empty list
                return output;
            }

            // Enqueue the first binary. Use dynamic string to avoid concat
            q.Push(new StringBuilder("1"));

            // Breadth-first search (BFS)
            while (n-- > 0)
            {

            }
        }
    }
}
