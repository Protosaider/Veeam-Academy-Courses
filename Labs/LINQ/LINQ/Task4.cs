using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace LINQ
{
    public class CElement
    {
        public String Name { get; set; }
        public Int32 Value { get; set; }

        public CElement(String name, Int32 value)
        {
            Name = name;
            Value = value;
        }
    }

    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random s_local;

        public static Random ThisThreadsRandom =>
            s_local ?? (s_local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId)));
    }

    public static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Int32 n = list.Count;
            while (n > 1)
            {
                n--;
                Int32 k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }

    public class CTask4
    {
        public static void MainX()
        {
            List<CElement> Elements = new List<CElement>
            {
                new CElement("Sword", 100),
                new CElement("Warhammer", 112),
                new CElement("Crossbow", 142),
                new CElement("Axe", 1153),
                new CElement("Greatsword", 113),
                new CElement("Dagger", 145),
                new CElement("Broadsword", 156),
                new CElement("Staff", 144),
                new CElement("Spear", 175),
                new CElement("Bow", 107)
            };
            //Elements.Shuffle();

            Char delimiter = ';';

            //4. Для выборки элементов (предполагая, что у каждого элемента есть имя Name) произвести конкатенацию имен всех элементов, кроме первых трех,
            //в одну строку, разделенных заданным параметром (символом) delimeter


            Console.WriteLine("Task 4 with StringBuilder");
            StringBuilder sb = new StringBuilder();
            foreach (var element in Elements.Select(x => x.Name).Skip(3))
            {
                sb.Append(element);
                sb.Append(delimiter);
            }
            Console.WriteLine(sb.ToString());
            Console.ReadLine();

            Console.WriteLine("Task 4 with String.Join");
            var str = String.Join(delimiter.ToString(), Elements.Select(x => x.Name).Skip(3));
            Console.WriteLine(str);
            Console.ReadLine();


            Console.WriteLine("Task 4 Linq only");
            var result = Elements.Select(x => x.Name).Skip(3).Aggregate((i, j) => i + delimiter + j);
            Console.WriteLine(result);
            Console.ReadLine();           

        }


        public static String Task4LinqOnly(List<CElement> elements, Char delimiter) => elements.Select(x => x.Name).Skip(3).Aggregate((i, j) => i + delimiter + j);

        public static String Task4StringJoin(List<CElement> elements, Char delimiter) => String.Join(delimiter.ToString(), elements.Select(x => x.Name).Skip(3));

        public static String Task4StringBuilder(List<CElement> elements, Char delimiter)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var element in elements.Select(x => x.Name).Skip(3))
            {
                sb.Append(element);
                sb.Append(delimiter);
            }
            return sb.ToString();
        }
    }
}