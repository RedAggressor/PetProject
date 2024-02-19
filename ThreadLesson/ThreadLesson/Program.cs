namespace ThreadLesson
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //var expected = new TaskExample();

            //expected.Run();

            //Console.ReadLine();

            //var ex = new RequiredInterLLocked();

            //ex.Run();

            //Console.ReadLine();

            var example = new AsyncAwaitExample();
            var result = await example.FActorialAsync();

            Console.WriteLine(result);
        }
    }
}
