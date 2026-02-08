using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Hello!");
        
        // إدخال الرقم الأول
        Console.WriteLine("Input the first number:");
        string input1 = Console.ReadLine();
        int num1 = int.Parse(input1);

        // إدخال الرقم الثاني
        Console.WriteLine("Input the second number:");
        string input2 = Console.ReadLine();
        int num2 = int.Parse(input2);

        // اختيار العملية
        Console.WriteLine("What do you want to do with those numbers?");
        Console.WriteLine("[A]dd");
        Console.WriteLine("[S]ubtract");
        Console.WriteLine("[M]ultiply");
        
        string choice = Console.ReadLine()?.ToUpper();

        // معالجة النتيجة بناءً على الاختيار
        if (choice == "A")
        {
            Console.WriteLine($"{num1} + {num2} = {num1 + num2}");
        }
        else if (choice == "S")
        {
            Console.WriteLine($"{num1} - {num2} = {num1 - num2}");
        }
        else if (choice == "M")
        {
            Console.WriteLine($"{num1} * {num2} = {num1 * num2}");
        }
        else
        {
            Console.WriteLine("Invalid option");
        }

        // الخروج
        Console.WriteLine("Press any key to close");
        Console.ReadKey();
    }
}
