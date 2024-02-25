namespace LockLesson.DeadLock;

internal class ResourceA
{
    public void MethodA(ResourseB resourseB)
    {
        
        var acquireLock = new AcquireLock();

        try
        {
            lock (this)
            {
                Console.WriteLine("Method A is currently runnig...");

                Thread.Sleep(1000);

                lock (resourseB)
                {
                    Console.WriteLine("Resourse inside resourse A is working...");
                }
            }
            
        }
        finally
        {
            if (acquireLock.TryAcquireLock(resourseB, TimeSpan.FromSeconds(13)))
            {
                Console.WriteLine("A");
                Monitor.Exit(resourseB);
            }
        }


    }
}
