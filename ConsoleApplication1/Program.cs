using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Parent t = new Child1();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                t.Run();
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds);
        }
    }

    abstract class Parent
    {
        [ParentDemo(Order = 1)]
        public virtual void Run()
        {
            var method = this.GetType().GetMethod("Run");
            FilterAttribute[] methods = method.GetCustomAttributes(typeof(FilterAttribute), true) as FilterAttribute[];
            if (methods != null)
            {
                Array.Reverse(methods);
                Array.Sort<FilterAttribute>(methods, (t1, t2) => { return (t1.Order < t2.Order) ? 1 : ((t1.Order == t2.Order) ? 0 : -1); });
                for (int i = 0; i < methods.Length; i++)
                {
                    methods[i].Run();
                }
            }
        }
    }

    class Child1 : Parent
    {
        [Demo]
        public override void Run()
        {
            base.Run();
            Console.WriteLine("child1 Run");

        }
    }

    abstract class FilterAttribute : Attribute
    {
        public abstract void Run();
        public int Order { get; set; }
    }

    class ParentDemoAttribute : FilterAttribute
    {
        public override void Run()
        {
            Console.WriteLine("Parent Demo Attribute");
        }
    }
    class DemoAttribute : FilterAttribute
    {
        public override void Run() { Console.WriteLine("Demo Attribute"); }
    }
}
