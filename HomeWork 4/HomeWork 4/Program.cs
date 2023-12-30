using System.Diagnostics.CodeAnalysis;

namespace HomeWork_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int lengthArray;
            do
            {
                Console.Write($"Enter length of array what you want to create at 0 for {int.MaxValue / 2}: ");

                Console.WriteLine();
            }
            while (!int.TryParse(Console.ReadLine(), out lengthArray) || lengthArray < 0 || lengthArray > int.MaxValue / 2);

            /* First array with rendom elemet from 1 to 26 value */

            Console.WriteLine("Display first array: ");

            CreateAndDysplayRandomArray(lengthArray);

            /* Start create add work with Even number array and transfrom to char */

            Console.WriteLine("Displey Even char array with Upper: ");

            int jumperToEven = 0;

            char[] charEvenArray = CreateAndChangeArrayFromIntToChar(lengthArray, jumperToEven);

            int countEvenCharUpper = CountOfUpperChar(charEvenArray);

            Console.WriteLine();

            /* Start create add work with Odd number array and transfrom to char */

            Console.WriteLine("Displey Odd char array with Upper: ");

            int jumperToOdd = 1;

            char[] charOddArray = CreateAndChangeArrayFromIntToChar(lengthArray, jumperToOdd);

            int countOddCharUpper = CountOfUpperChar(charOddArray);

            Console.WriteLine();

            /* Compare two char array */

            Console.WriteLine($"array with even numbers have {countEvenCharUpper} upper letter, array odd numbers have {countOddCharUpper} upper letter");

            CompareTwoValues(countEvenCharUpper, countOddCharUpper);
        }

        static int[] CreateAndDysplayRandomArray(int lengthArray)
        {
            int[] arrayOfNumbers = new int[lengthArray];

            AddElementWithRandomValue(arrayOfNumbers);

            DisplayShowIntArray(arrayOfNumbers);

            Console.WriteLine();

            return arrayOfNumbers;
        }

        static char[] CreateAndChangeArrayFromIntToChar(int lengthArray, int jumperOddEven)
        {
            int[] arrayEvenNumbers = new int[lengthArray];

            GenerationEvenOrOddElementToArray(arrayEvenNumbers, jumperOddEven);

            char[] changeEvenArray = ChangeNumberToChar(arrayEvenNumbers);

            ChangeOnUpperCaseSpecificChar(changeEvenArray);

            DisplayShowCharArray(changeEvenArray);

            return changeEvenArray;
        }

        static void DisplayShowIntArray(int[] array)
        {
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
        }

        static void DisplayShowCharArray(char[] array)
        {
            foreach (var item in array)
            {
                Console.Write(item + " ");
            }

            Console.WriteLine();
        }

        static int[] AddElementWithRandomValue(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = new Random().Next(1, 27);
            }

            return array;
        }

        static void GenerationEvenOrOddElementToArray(int[] array, int jumperOddEven)
        {
            int randomNumber;

            for (int i = 0; i < array.Length; i++)
            {
                randomNumber = new Random().Next(1, 26);

                if (randomNumber % 2 == jumperOddEven)
                {
                    array[i] = randomNumber;
                }
                else
                {
                     array[i] = randomNumber + 1;
                }
            }
        }

        static char[] ChangeNumberToChar(int[] array)
        {
            char[] arrayChar = new char[array.Length];

            int corectionChanged = 1;

            char countdownBegins = 'a';

            for (int i = 0; i < array.Length; i++)
            {
                arrayChar[i] = Convert.ToChar(countdownBegins + array[i] - corectionChanged); // 'a' the first char from which we look for the one we need by adding our number(array[i]) with a correction variable
            }

            return arrayChar;
        }

        static char[] ChangeOnUpperCaseSpecificChar(char[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (CheckSpecificChar(array[i]))
                {
                    array[i] = char.ToUpper(array[i]);
                }
            }

            return array;
        }

        static int CountOfUpperChar(char[] array)
        {
            int counter = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (char.IsUpper(array[i]))
                {
                    counter++;
                }
            }

            return counter;
        }

        static bool CheckSpecificChar(char ch)
        {
            switch (ch)
            {
                case 'a':
                case 'e':
                case 'i':
                case 'd':
                case 'h':
                case 'j':
                    return true;

                default:
                    return false;
            }
        }

        static void CompareTwoValues(int firstValue, int secondValue)
        {
            switch ((firstValue, secondValue))
            {
                case (>= 0, >= 0) when firstValue > secondValue:

                    Console.WriteLine("array with upper letter even numbers > array with upper letter odd numbers");

                    break;

                case (>= 0, >= 0) when firstValue == secondValue:

                    Console.WriteLine("array with upper letter even numbers = array with upper letter odd numbers");

                    break;

                case (>= 0, >= 0) when firstValue < secondValue:

                    Console.WriteLine("array with upper letter odd numbers > array with upper letter even numbers");

                    break;
            }
        }
    }
}
