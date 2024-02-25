namespace LockLesson.Semaphore;

internal class SemaphoreS
{
    private SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(2);
    private int Counter { get; set; } = default;

    public async Task IncrementCounterAsync()
    { 
        await _semaphoreSlim.WaitAsync();

		try
		{
			Counter++;
            Console.WriteLine($"curecnt counter {Counter}");
        }
		finally 
        { 
            _semaphoreSlim.Release();
        }
		
    }

    public async Task DicrementCounterAsync()
    {
        await _semaphoreSlim.WaitAsync();

        try
        {
            Counter--;
            Console.WriteLine($"curecnt counter {Counter}");
        }
        finally
        {
            _semaphoreSlim.Release();
        }

    }
}
