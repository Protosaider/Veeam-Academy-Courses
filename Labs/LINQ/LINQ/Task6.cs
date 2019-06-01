using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace LINQ
{
    public class CTask6
    {
        public static void MainX()
        {
            String sentence = "Это что же получается: ходишь, ходишь в школу, а потом бац - вторая смена";

            //6. Для заданного предложения сгруппировать слова одинаковой длины, отсортировать группы по убыванию количества элементов в каждой группе,
            //вывести информацию по каждой группе: длина(количество букв в словах группы), количество элементов. Знаки препинания не учитывать.
            Console.WriteLine("Task 6 Linq");

            Console.WriteLine("Using string.Join");

            var res = String.Join(null, sentence.Where(x => Char.IsLetter(x) || x == ' '))
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .GroupBy((x) => x.Length)
                .Select(x => new { Length = x.Key, Count = x.Count(), Values = x.Select(i => i) })
                .OrderByDescending(x => x.Count)
                .ThenByDescending(x => x.Length);

            foreach (var element in res)
            {
                Console.WriteLine(element.Length.ToString() + " " + element.Count.ToString());
                foreach (var value in element.Values)
                {
                    Console.WriteLine(value);
                }
            }
            Console.ReadLine();
            Console.WriteLine("Using Regex");

            //Regex rgx = new Regex(@"[\W]");
            Regex rgx = new Regex(@"[\W-[ ]]");
            res = rgx.Replace(sentence, "")
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .GroupBy((x) => x.Length)
                .Select(x => new { Length = x.Key, Count = x.Count(), Values = x.Select(i => i) })
                .OrderByDescending(x => x.Count)
                .ThenByDescending(x => x.Length);

            foreach (var element in res)
            {
                Console.WriteLine(element.Length.ToString() + " " + element.Count.ToString());
                foreach (var value in element.Values)
                {
                    Console.WriteLine(value);
                }
            }

            Console.ReadLine();

        }

        public static List<(Int32 Length, Int32 Count, List<String> Values)> Task6StringJoin(String sentence)
        {
            var res = String.Join(null, sentence.Where(x => Char.IsLetter(x) || x == ' '))
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .GroupBy((x) => x.Length)
                .Select(x => (Length: x.Key, Count: x.Count(), Values: x.Select(i => i).ToList()))
                .OrderByDescending(x => x.Count)
                .ThenByDescending(x => x.Length);

            return res.ToList();
        }

        public static List<(Int32 Length, Int32 Count, List<String> Values)> Task6Regex(String sentence)
        {
            Regex rgx = new Regex(@"[\W-[ ]]");
            var res = rgx.Replace(sentence, "")
                .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)
                .GroupBy((x) => x.Length)
                .Select(
                    x => (Length: x.Key, Count: x.Count(), Values: x.Select(i => i).ToList())
                )
                .OrderByDescending(x => x.Count)
                .ThenByDescending(x => x.Length);

            return res.ToList();
        }

    }
}