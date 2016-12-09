using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeAndFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new MyThing { FullName = "Ian Cousins" };

            Console.WriteLine(data.FullName);

            var myThingPipeLine = new MyThingPipeLine();

            myThingPipeLine.Register(new LowerCaseFilter()).Register(new UpperCaseFilter());

            var status = myThingPipeLine.Process(data);

            Console.ReadKey();
        }
    }

    public class MyThing
    {
        public string FullName { get; set; }
    }

    public class MyOtherThing
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class LowerCaseFilter : IFilter<MyThing>
    {
        public MyThing Execute(MyThing input)
        {
            var item = input;

            item.FullName = item.FullName.ToLower();

            Console.WriteLine(item.FullName);

            return item;
        }
    }

    public class UpperCaseFilter : IFilter<MyThing>
    {
        public MyThing Execute(MyThing input)
        {
            var item = input;

            item.FullName = item.FullName.ToUpper();

            Console.WriteLine(item.FullName);

            return item;
        }
    }

    public class MyThingPipeLine : Pipeline<MyThing>
    {
        public override MyThing Process(MyThing input)
        {
            foreach (var filter in Filters)
            {
                input = filter.Execute(input);
            }

            return input;
        }
    }

    public abstract class Pipeline<T>
    {
        protected readonly List<IFilter<T>> Filters = new List<IFilter<T>>();

        public Pipeline<T> Register(IFilter<T> filter)
        {
            Filters.Add(filter);
            return this;
        }

        public abstract T Process(T input);
    }

    public interface IFilter<T>
    {
        T Execute(T input);
    }
}
