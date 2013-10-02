using System;

public class JobQueue
{
    private Queue<int> Monday;
    private Queue<int> Tuesday;
    private Queue<int> Wednesday;
    private Queue<int> Thursday;
    private Queue<int> Friday;
    private Queue<int> Saturday;
    private Queue<int> Sunday;

	public JobQueue()
	{
        Monday = new Queue();
        Tuesday = new Queue();
        Wednesday = new Queue();
        Thursday = new Queue();
        Friday = new Queue();
        Saturday = new Queue();
        Sunday = new Queue();

       
	}
}
