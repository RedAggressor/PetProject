//using LockLesson.DeadLock;

//var resourseA = new ResourceA();
//var resourseB = new ResourseB();

//var thread1 = new Thread(() =>resourseA.MethodA(resourseB));
//var thread2 = new Thread(()=> resourseB.MethodB(resourseA));

//thread1.Start();
//thread2.Start();

//thread1.Join();
//thread2.Join();

//Console.WriteLine("deadlock");
//using LockLesson.Mutex;

//var sharedResoursces = new SheredResourse();
//var numberOfiteracions = 50;

//var thread = new Thread(() =>
//{

//    for (int i = 0; i < numberOfiteracions; i++)
//    {
//        sharedResoursces.IncrementCounter();
//        Task.Delay(1000);
//    }
//});

//var thread1 = new Thread(() =>
//{
//    for (int i = 0; i < numberOfiteracions; i++)
//    {
//        sharedResoursces.DecrementCounter();
//        Task.Delay(1000);
//    }
//});

//thread.Start();
//thread1.Start();
//thread.Join();
//thread1.Join();

using LockLesson.Semaphore;

var sharedResoursces = new SemaphoreS();
var numberOfiteracions = 50;


var task = Task.Run(async()=>
{
    for (int i = 0; i < numberOfiteracions; i++)
    {
        await sharedResoursces.IncrementCounterAsync();
        await Task.Delay(1000);
    }
});

var task1 = Task.Run(async() =>
{
    for (int i = 0; i < numberOfiteracions; i++)
    {
        await sharedResoursces.DicrementCounterAsync();
        await Task.Delay(1000);
    }
});

Task.WaitAll(task,task1);

