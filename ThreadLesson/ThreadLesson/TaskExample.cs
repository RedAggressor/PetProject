using System.Xml.Linq;

namespace ThreadLesson
{
    internal class TaskExample
    {
        private const int ArraySize = 10000;
        private const int MaxThread = 4;

        private int[] _numbers;
        private long _totalSum;
        public void Run()
        {
            Task task1 = Task.Run(() =>
            {
                Console.WriteLine("Task 1 is running...");
                Task.Delay(300).Wait();
                Console.WriteLine("Task 1 here...");
                Task.Delay(100).Wait();
                Console.WriteLine("Task 1 is done...");
            }
            );

            Task task2 = Task.Run(() => 
            {
                Console.WriteLine("Task 2 is running...");
                Task.Delay(300).Wait();
                Console.WriteLine("Task 2 here...");
                Task.Delay(100).Wait();
                Console.WriteLine("Task 2 is done...");
            });

            Task task3 = Task.Run(() =>
            {
                Console.WriteLine("Task 3 is running...");
                Task.Delay(300).Wait();
                Console.WriteLine("Task 3 here...");
                Task.Delay(100).Wait();
                Console.WriteLine("Task 3 is done...");
            }
            );

            Task task4 = Task.Run(() =>
            {
                Console.WriteLine("Task 4 is running...");
                Task.Delay(300).Wait();
                Console.WriteLine("Task 4 here...");
                Task.Delay(100).Wait();
                Console.WriteLine("Task 4 is done...");
            }
            );

            Console.WriteLine("All tasks have complited");
        }

        public TaskExample()
        {
            _numbers = new int[ArraySize];

            var rand = new Random();
            for (int i = 0; i < ArraySize; i++)
            {
                _numbers[i] = rand.Next(10);
            }
        }
        public void Run2()
        {
            _totalSum = 0;
            var threads = new Thread[MaxThread];

            for (int i = 0; i < MaxThread; i++)
            {
                threads[i] = new Thread(ComputeSum);
                threads[i].Start(i);
            }
        }

        private void ComputeSum(object threadIndexObj)
        {
            int threadIndex = (int)threadIndexObj;
            long sum = default;

            int startIndex = (ArraySize / MaxThread) * threadIndex;
            int endIndex = (ArraySize / MaxThread) * (threadIndex +1);

            for (int i = startIndex; i < endIndex; i++)
            {
                sum += _numbers[i];
            }

            Interlocked.Add(ref _totalSum, sum);
        }
    }
}
