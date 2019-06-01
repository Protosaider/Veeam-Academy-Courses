using System;
using System.Collections.Generic;
using System.Linq;

namespace LINQ
{
    public class CTask5
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
            Elements.Shuffle();

            //5. Найти все элементы в последовательности / выборке, длина имени (количество символов) у которых больше, чем позиция,
            //которую они занимают в последовательности / выборке

            Console.WriteLine("Task 5 Linq");
            foreach (var element in Elements.Where((i, j) => i.Name.Length > j).Select((i, j) => new { i, j }))
            {
                Console.WriteLine(element.i.Name + " " + element.j.ToString());
            }
            Console.ReadLine();
        }

        public static List<Tuple<String, Int32>> Task5Linq(List<CElement> elements)
        {
            var result = new List<Tuple<String, Int32>>();
            foreach (var element in elements.Where((i, j) => i.Name.Length > j).Select((i, j) => new { i, j }))
            {
                result.Add(new Tuple<String, Int32>(element.i.Name, element.j));
            }
            return result;
        }
    }
}