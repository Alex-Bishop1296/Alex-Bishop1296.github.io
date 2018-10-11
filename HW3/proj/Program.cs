// Here is my file for Program with the purpose of replicating the code in Main.java but in C#.
// Written By: Alex Bishop
// Last Edit: 10/10/2018


using System;
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
        /// <summary>
        /// A function for generating the list of binary numbers for a given number
        /// </summary>
        /// <param name="n">The number you want to represent in binary</param>
        /// <returns>A linked list of binaries forming n</returns>
        static LinkedList<string> generateBinaryRepresentationList(int n)
        {
            // This starting section of intializing our lists with default constructors

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
                // print the front of the queue
                StringBuilder sb = q.Pop();
                output.AddLast(sb.ToString());

                // Make a copy
                StringBuilder sbc = new StringBuilder(sb.ToString());

                // Left Child
                sb.Append('0');
                q.Push(sb);

                // Right Child
                sbc.Append('1');
                q.Push(sbc);
            }
            return output;
        }

        /// <summary>
        /// Driver program to test above function
        /// </summary>
        /// <param name="args">args taken in from the command line</param>
        public static void Main(string[] args)
        {
            // Number to represent in binary
            int n = 10;
            // If args from user are not given
            if (args.Length < 1)
            {
                Console.WriteLine("Please invoke with the max value to print binary up to, like this:");
                Console.WriteLine("\tProgram 12");
                return;
            }
            try
            {
                n = int.Parse(args[0]);
            }
            catch (FormatException)
            {
                Console.WriteLine("I'm sorry, I can't understand the number: " + args[0]);
                return;
            }
            LinkedList<string> output = generateBinaryRepresentationList(n);
            // Print it right justified. Longest string is the last one
            // Print enough spaces to move it over the correct distance
            int maxLength = output.Count();
            foreach(string s in output) 
            {
                for (int i = 0; i < maxLength - s.Length; ++i)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(s);
            }
          
        }
    }
}
