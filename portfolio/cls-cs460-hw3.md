# CS460 Homework 2

# Return to?
### [Code Repo](https://github.com/Alex-Bishop1296/Alex-Bishop1296.github.io) 
### [Home](../index.md) 
### [CS460 Assignments](cls-cs460.md) 

# Notes

# 1 & 2.[Setup] 
Setup for this project was just having visual studio community 2017 installed, as I had already done before in the past their was noting else to do for this step. I simply downloaded the java files and started to translate them as I will detail below.

# 3.[Planning and Design]
Let me go over some of the concepts I learn pouring over the java code. The first thing The first node file contains the class with the public scope Node\<T>. Seen here:

![Node.java code](example/node.PNG)

 The \<T> is a type parameter (also called type variables) that can be used A type variable can be any non-primitive type you specify: any class type, any interface type, any array type, or even another type variable. This same technique can be applied to create generic interfaces. Essentially this allows multitudes of object types inside this object class. The genral purpose of the node code being holding an object and the reference to the next object it points to be used in a abstact data structure.

 The next major concept from the java code was the interface, shown here:

 ![QueueInterface.java code](example/interface.PNG)

 An interface is a container for a future class of data that contains prebuilt either function names or default functions that can be implemented to facilitate use in the same style. This allows a classes of similar types to share code with repeating the implementation of it. In our case the interface is just used to for the sake of it, as LinkedQueue is the only class that uses it. Interfaces can also be though of the header file in a C++ enviroment, although more specialized.

 The final major concept in the java file I would like to go over is the Exception, seen here: 

 ![QueueUnderflowException code](example/exception.PNG)

 Exceptions are a way of handling errors with the proper response to the user. In this case, it contains both a messaged exception and one without a message. This allows the exception to be called with just the name of it for the user or a more specific error message is some cases. Exceptions are a common language concept typically associated with try catch execution.

 These where the major concepts from the java code that jumped out at me as needing some review. From here I decided to to brush up on some C# syntax and then jump into the translation with the Node.java file.

# 4 - 6.[Coding and Content]
