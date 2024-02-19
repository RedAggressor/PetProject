namespace ThreadLesson
{
    internal class RequiredInterLLocked
    {
        private int counter = 0;

        public void Run()
        {
            var tasks = new Task[10];

            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => 
                {
                    for (int j = 0; j < 1000; j++)
                    {
                        Interlocked.Increment(ref counter);
                    }
                });
            }

            Task.WaitAll(tasks);

            

            Console.WriteLine($"Finaly {counter}");
        }
    }
}
