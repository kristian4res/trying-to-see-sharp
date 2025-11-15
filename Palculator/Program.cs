
namespace Palculator;

class Program {
    static void Main()
    {
        Cal = new TwoNumCalculator();
    }
}

// Classes
public class TwoNumCalculator()
{
    public static double Add(double num1, double num2)
    {
        return num1 + num2;
    }

    public static double Subtract(double num1, double num2)
    {
        return num1 - num2;
    }

    public static double Multiply(double num1, double num2)
    {
        return num1 * num2;
    }

    public static double? Divide(double num1, double num2)
    {
        if (num2 == 0)
        {
            Console.WriteLine("Error: Cannot divide by zero.");
            return null;
        }
        return num1 / num2;
    }
    
    public static void DisplayNumbers()
    {
        Console.WriteLine("Counting from 1 to 10...");
        for (int i = 1; i < 11; i++)
        {
            Console.WriteLine(i);
        }
    }
}