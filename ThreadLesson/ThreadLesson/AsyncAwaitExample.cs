namespace ThreadLesson
{
    internal class AsyncAwaitExample
    {
        private int Factorial()
        {
            int factorialNumber = 6;

            int result = 1;

            for (int i = 1; i <= factorialNumber; i++)
            {
                result *= i;
            }

            Thread.Sleep(3000);
            Console.WriteLine($"Factorial is: {result}");
            return result;

        }
        public async Task<int> FActorialAsync()
        {
            Console.WriteLine("Start");

            var result = await Task.Run(() => Factorial());

            Console.WriteLine("Exite....");

            return result;
        }
    }
}
