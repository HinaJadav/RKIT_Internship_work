using Creating_AddingReferenceToAssemblies;
using System;

class UseOfNamespace
{
    static void Main(string[] args)
    {
        Console.Write("Enter user name: ");
        string name = Console.ReadLine();

        MyNamespace myNamespace = new MyNamespace();
        string welcomeMassage = myNamespace.welcomeMassage(name);
        Console.WriteLine(welcomeMassage);
    }
}