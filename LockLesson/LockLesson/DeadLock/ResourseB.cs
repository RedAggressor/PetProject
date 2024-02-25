namespace LockLesson.DeadLock;

internal class ResourseB
{
    public void MethodB(ResourceA resourceA)
    {
        
        var acquireLock = new AcquireLock();

        try
        {
            lock (this)
            {
                Console.WriteLine("Method B is currently runnig...");

                Thread.Sleep(1000);

                lock (resourceA)
                {
                    Console.WriteLine("Resourse inside resourse B is working...");
                }
            }
        }
        finally
        {
            if (acquireLock.TryAcquireLock(resourceA, TimeSpan.FromSeconds(13)))
            {
                Console.WriteLine("B");
                Monitor.Exit(resourceA);
            }
        }
    }
}
