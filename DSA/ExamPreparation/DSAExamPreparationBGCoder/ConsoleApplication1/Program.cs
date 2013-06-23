using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wintellect.PowerCollections;

public class ShipDamage
{
    static PriorityQueue<Task> tasks;
    static void Main()
    {

        int n = int.Parse(Console.ReadLine());
        tasks = new PriorityQueue<Task>();

        StringBuilder sb = new StringBuilder();
       
        for (int i = 0; i < n; i++)
        {
            string[] commandParameters = Console.ReadLine().Split();
            if (commandParameters[0][0] == 'N')
            {
                
                tasks.Enqueue(new Task(commandParameters[1], commandParameters[2]));
            }
            else
            {
                sb.AppendLine(Solve());
            }
        }
        Console.Write(sb);
    }

    static string Solve()
    {
        if (tasks.Count > 0)
        {
            return tasks.Dequeue().Name;
        }
        else
        {
            return "Rest";
        }
    }


}

public class PriorityQueue<T> 
    where T : IComparable<T>
{
    private OrderedBag<T> bag;

    public PriorityQueue()
    {
        this.bag = new OrderedBag<T>();
    } 

    public int Count
    {
        get { return bag.Count; }
        private set { }
    }

    public void Enqueue(T element)
    {
        bag.Add(element);
    }

    public T Dequeue()
    {

        var element = bag.GetFirst();
        bag.Remove(element);
        return element;
    }
}

public class Task : IComparable<Task>
{
    public int Complexity { get; set; }
    public string Name { get; set; }

    public Task(string complexity, string name) 
    {
        this.Complexity = int.Parse(complexity);
        this.Name = name;
    }

    public override int GetHashCode()
    {
        return this.Complexity ^ this.Name.GetHashCode();
    }

    public int CompareTo(Task other)
    {
        if (this.Complexity.CompareTo(other.Complexity)==0)
        {
            return this.Name.CompareTo(other.Name);
        }
        return this.Complexity.CompareTo(other.Complexity);
    }
}