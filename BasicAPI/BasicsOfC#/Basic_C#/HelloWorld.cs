using System; // Basic library (Ref)

namespace Basic_C_
{
    internal class HelloWorld
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World");
            // If not import "System" Library 
            System.Console.WriteLine("Hello C#");

            //---------------------------------------------------------------------------------
            // Basic Information:

            // Mathematical calculations:
            Console.WriteLine(1 + 3);

            // Write() method:
            Console.Write("How are you?");
            Console.WriteLine("I'm fine.");

            //---------------------------------------------------------------------------------
            // Numeric data types:

            int age = 24;
            Console.WriteLine("Age: " + age);

            // int datatype, initial '0' not affect actual value
            int tempNum = 0010;
            Console.WriteLine("Zero appended num: " + tempNum);
            // If we want '0' in beginning then we use string 


            // Data type size range:
            Console.WriteLine("Max value for int: " + int.MaxValue);
            Console.WriteLine("Min value for int: " + int.MinValue);

            long salary = 10000000L; // By default Int32 for consider it as Int64 put "L" at the end
            Console.WriteLine("Salary: " + salary);

            Console.WriteLine("Max value for long: " + long.MaxValue);
            Console.WriteLine("Min value for long: " + long.MinValue);

            // Negative numeric value
            int negativeNum = -4;
            Console.WriteLine("Negative number: " + negativeNum);

            double spi = 8.5;
            // double spi = 8.5D; // Also write like this
            Console.WriteLine("SPI value: " + spi);

            float cpi = 8.25F; // "F" needs to write for float 
            Console.WriteLine("CPI value: " + cpi);

            decimal piValue = 3.141592653589793M; // "M" needs to write for decimal
            Console.WriteLine("Pi value: " + piValue);

            //---------------------------------------------------------------------------------
            // Text based data types:

            string name = "Priyank";
            Console.WriteLine("Name: " + name);

            // Empty string
            Console.WriteLine("Empty string: " + "");

            // Char type
            char grade = 'A';
            Console.WriteLine("Grade: " + grade);

            // Null char (representing the absence of a character)
            Console.WriteLine("Null character: " + '\0');

            // Boolean data type:
            bool isBool = false;

            //---------------------------------------------------------------------------------
            // Explicit type conversion: (Using "Convert" class and it's inbuilt methods)
            // 1) Converting string to numbers: 

            string textNum = "10";
            int num = Convert.ToInt32(textNum);
            Console.WriteLine("Converted to int: " + num);

            string textLongNum = "6000000000";
            long longNum = Convert.ToInt64(textLongNum);
            Console.WriteLine("Converted to long: " + longNum);

            string textDoubleNum = "34.88";
            double doubleNum = Convert.ToDouble(textDoubleNum);
            Console.WriteLine("Converted to double: " + doubleNum);

            string textFloatNum = "34.88";
            float floatNum = Convert.ToSingle(textFloatNum);
            Console.WriteLine("Converted to float: " + floatNum);

            string textDecimalNum = "3.145555555555";
            decimal decimalNum = Convert.ToDecimal(textDecimalNum);
            Console.WriteLine("Converted to decimal: " + decimalNum);

            // 2) Converting int to string and char

            // Using ToString()
            int intNum = 123;
            string string1 = Convert.ToString(intNum);
            Console.WriteLine("Converted int to string using ToString(): " + string1);

            // Using Interpolation
            string string2 = $"{intNum}";
            Console.WriteLine("Converted int to string using Interpolation: " + string2);

            int intNum1 = 65;
            char character = (char)intNum1;
            Console.WriteLine("Converted int to char: " + character);

            int intIsCorrect = 1;
            bool isCorrect = Convert.ToBoolean(intIsCorrect);
            Console.WriteLine("Converted int to boolean: " + isCorrect);

            // 3) Boxing and Unboxing
            // Boxing: Converts value type to reference type.
            string planetName = "Earth";
            object planetObject = planetName;
            Console.WriteLine($"\nBoxing (string to object): {planetObject}");

            // Unboxing: Reverse process of Boxing
            object stateObject = "Gujarat";
            string stateName = (String)stateObject;
            Console.WriteLine($"Unboxing (object to string): {stateName}\n");

            //---------------------------------------------------------------------------------
            // Implicit conversions

            // 1) Numeric implicit conversions
            int intValue = 100;
            long longValue = intValue; // int to long
            Console.WriteLine("Implicitly converted int to long: " + longValue);

            float floatValue = intValue; // int to float
            Console.WriteLine("Implicitly converted int to float: " + floatValue);

            double doubleValue = floatValue; // float to double
            Console.WriteLine("Implicitly converted float to double: " + doubleValue);

            // 2) char to integer 
            char c = 'A';
            int asciiValue = c; // char to int
            Console.WriteLine("Implicitly converted char to int: " + asciiValue);

            //---------------------------------------------------------------------------------
            // Operators:

            int n1 = 10;

            // ++, --
            n1++;
            Console.WriteLine("After incrementing (n1++): " + n1);

            n1--;
            Console.WriteLine("After decrementing (n1--): " + n1);

            // +=, -=, /=, *=

            n1 += 2;
            Console.WriteLine("After adding 2 (n1 += 2): " + n1);

            n1 -= 2;
            Console.WriteLine("After subtracting 2 (n1 -= 2): " + n1);

            n1 *= 2;
            Console.WriteLine("After multiplying by 2 (n1 *= 2): " + n1);

            n1 /= 2;
            Console.WriteLine("After dividing by 2 (n1 /= 2): " + n1);

            // "+" with string : string concatenation 
            string str1 = "Hello",
                str2 = " World";
            Console.WriteLine("String concatenation (str1 + str2): " + str1 + str2);

            // "+" with char: add characters ASCII values
            char c1 = 'h',
                c2 = 'i';
            Console.WriteLine("Adding ASCII values of c1 and c2 (c1 + c2): " + (c1 + c2));

            // User: == != 
            // ternary operator

            // Example 

            Console.Write("Test your IQ (29 * 89) : ");
            int inputAns = int.Parse(Console.ReadLine());

            Console.WriteLine(inputAns == 2581 ? "Correct" : "Incorrect");

            //---------------------------------------------------------------------------------
            // Example:

            // Find if the input is Even or Odd:

            Console.WriteLine("");
            Console.Write("Enter number for check (Odd / Even): ");
            int n;
            n = Convert.ToInt32(Console.ReadLine()); // Read the number and convert to int

            if (n % 2 == 0)
            {
                Console.WriteLine("EVEN");
            }
            else
            {
                Console.WriteLine("ODD");
            }

            Console.ReadLine(); // Prevent the application from closing directly.(Wait for User input)
        }
    }
}
