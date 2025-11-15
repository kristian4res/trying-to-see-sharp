// Problem 1
// public class Program
// {
// // Method to calculate the final price after a discount
// public static double ApplyDiscount(double price, double discountPercentage)
// {
//     return price - (price * (discountPercentage / 100));
// }

// public static void Main()
// {   
//     // Correct asnwer is 850
//     double finalPrice = ApplyDiscount(1000, 15);
//     Console.WriteLine("The final price is: " + finalPrice);
// }
// }

// Problem 2
// public class Program
// {
//     public static int FindMax(int[] numbers)
//     {
//         int max = numbers[0];
//         for (int i = 0; i < numbers.Length; i++)
//         {
//             if (numbers[i] > max)
//             {
//                 max = numbers[i];
//             }
//         }
//         return max;
//     }

//     public static void Main()
//     {
//         // Correct answer is -2
//         int[] myNumbers = { -5, -10, -3, -8, -2 };
//         int maxNumber = FindMax(myNumbers);
//         Console.WriteLine("The maximum number is: " + maxNumber);
//     }
// }