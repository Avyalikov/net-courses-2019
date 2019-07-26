using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Events
{
    class Program
    {
        //public interface IOutput
        //{
        //    void WriteLine(string message);
        //}

        //public class ConsoleOutput : IOutput
        //{
        //    public void WriteLine(string message)
        //    {
        //        Console.WriteLine(message);
        //    }
        //}

        //// Define a class to hold custom event info
        //public class CustomEventArgs : EventArgs
        //{
        //    public CustomEventArgs(string s)
        //    {
        //        message = s;
        //    }
        //    private string message;

        //    public string Message
        //    {
        //        get { return message; }
        //        set { message = value; }
        //    }
        //}

        //// Class that publishes an event
        //class Publisher
        //{
        //    private readonly IOutput output;

        //    public Publisher(IOutput output)
        //    {
        //        this.output = output;
        //    }

        //    public event EventHandler<CustomEventArgs> RaiseCustomEvent
        //    {
        //        add
        //        {

        //        }
        //        remove
        //        {

        //        }
        //    }

        //    public void DoSomething()
        //    {
        //        while (true)
        //        {
        //            OnRaiseCustomEvent(new CustomEventArgs("Did something"));
        //            Thread.Sleep(500);
        //        }
        //    }

        //    protected virtual void OnRaiseCustomEvent(CustomEventArgs e)
        //    {
        //        //EventHandler<CustomEventArgs> handler = RaiseCustomEvent;

        //        // Race condition, we can pass RaiseCustomEvent, but at next step we can receive unsubscribed
        //        //if(RaiseCustomEvent != null)
        //        if (handler != null)
        //        {
        //            // simulate calculation before call delegate
        //            Thread.Sleep(5000);
        //            // Format the string to send inside the CustomEventArgs parameter
        //            e.Message += $" at {DateTime.Now}";

        //            // Use the () operator to raise the event.
        //            // RaiseCustomEvent(this, e);
        //            handler(this, e);
        //        }
        //    }
        //}

        ////Class that subscribes to an event
        //class Subscriber
        //{
        //    private string id;
        //    private readonly IOutput output;

        //    public Subscriber(string ID, IOutput output)
        //    {
        //        id = ID;
        //        this.output = output;
        //    }
             
        //    ~Subscriber()
        //    {
        //        this.output.WriteLine($"{id} -> destructor");
        //    }

        //    public void Subscribe(Publisher pub)
        //    {
        //        // Subscribe to the event using C# 2.0 syntax
        //        pub.RaiseCustomEvent += HandleCustomEvent;
        //        this.output.WriteLine(id + " subscribed... ");
        //    }

        //    public void Unsubscribe(Publisher pub)
        //    {
        //        pub.RaiseCustomEvent -= HandleCustomEvent;
        //        this.output.WriteLine(id + " unsubscribed... ");
        //    }

        //    // Define what actions to take when the event is raised.
        //    void HandleCustomEvent(object sender, CustomEventArgs e)
        //    {
        //        this.output.WriteLine($"{id} received this message: {e.Message}");
        //    }
        //}

        //static IOutput Output = new ConsoleOutput();

        static void Main(string[] args)
        {
            // case 1 - throws exception if eventhandler pointer is null (no subscriber)
            //{
            //    Publisher pub = new Publisher(Output);
            //    pub.DoSomething();
            //}

            // case 2 - throws exception if we unsubscribe twice, but on DoSomething
            //{
            //    Subscriber sub1 = new Subscriber("sub1", Output);
            //    Publisher pub = new Publisher(Output);

            //    sub1.Subscribe(pub);
            //    sub1.Unsubscribe(pub);
            //    sub1.Unsubscribe(pub);

            //    pub.DoSomething();
            //}

            // case 3 - throws exception if we racing unsubscribe
            //{
            //    Publisher pub = new Publisher(Output);
            //    var rnd = new Random();

            //    Task.Run(() => 
            //    {
            //        pub.DoSomething();
            //    });

            //    {
            //        Subscriber sub = new Subscriber($"{1}", Output);

            //        sub.Subscribe(pub);
            //        Thread.Sleep(rnd.Next(1000));

            //        sub.Unsubscribe(pub);
            //    }
               
            //}

            Console.ReadLine();
        }
    }
}

