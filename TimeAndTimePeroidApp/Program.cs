using System;
using System.Collections.Generic;
using System.Text;

namespace TimeAndTimePeroidLib
{
    class Program
    {
        static void Main(string[] args)
        {

            Time firstTime = new Time("13:10:00");
            Time secondTime = new Time(14, 10,00);
            Time thirdTime = new Time(13, 10);

            Console.WriteLine(firstTime.ToString());
            Console.WriteLine(secondTime.ToString());
            Console.WriteLine(thirdTime.ToString());

            TimePeriod firstTimePeroid = new TimePeriod(2137);
            TimePeriod secondTimePeroid = new TimePeriod(firstTime, secondTime);

            Console.WriteLine(firstTimePeroid.ToString());
            Console.WriteLine(secondTimePeroid.ToString());

            Console.WriteLine(firstTime.Equals(secondTime));
            Console.WriteLine(firstTime.Equals(thirdTime));
            Console.WriteLine(firstTimePeroid == secondTimePeroid);

            Console.WriteLine(firstTimePeroid.Plus(secondTimePeroid));

            Console.WriteLine(firstTime.Plus(firstTimePeroid));

            
        }
    }
}
