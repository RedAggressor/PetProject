namespace LockLesson.DeadLock;

internal class AcquireLock
{
    public bool TryAcquireLock(object lookObject, TimeSpan timeOut)
    {
        var startTime = DateTime.UtcNow;
        var lookAcquire = false;

        do
        {
            if (Monitor.TryEnter(lookObject))
            {
                lookAcquire = true;
                break;
            }

            Thread.Sleep(100);
        }
        while ((DateTime.UtcNow - startTime) < timeOut);

        return lookAcquire;
    }
}
