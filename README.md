# Tree2JSON
A lightweight Tree to JSON string Serializer which implemented by C#. this can be easily integrated into code if a 3rd party library is NOT desired


this scriptlet can be freely copy, modify or distrabute in your code.

However, Please take below into consideration:
 
The runtime enginee for C# have method stack frame, meaning that the stack size are limited.
Therefore, if the tree size are too huge, then it most likely exceed the stack size when
traverse the whole nodes tree for generating JSON string.

IF you find anything during your testing or any suggestion, please mail to houxuyong@hotmail.com
