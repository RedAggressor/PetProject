using Mut = System.Threading.Mutex;
namespace LockLesson.Mutex;

internal class SheredResourse
{
    private Mut mutex = new Mut();
    public int Counter { get; set; } = default;

    public void IncrementCounter()
    { 
        mutex.WaitOne();
		try
		{
			Counter++;
            Console.WriteLine($"Current count is {Counter}");
        }
		finally
		{

			mutex.ReleaseMutex();
		}
    }

    public void DecrementCounter()
    {
        mutex.WaitOne();
        try
        {
            Counter--;
            Console.WriteLine($"Current count is {Counter}");
        }
        finally
        {

            mutex.ReleaseMutex();
        }
    }
}
