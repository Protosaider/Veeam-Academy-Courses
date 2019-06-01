using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LINQ
{
    public class CTask7
    {
        private static Regex regex = new Regex(@"\W+");

        public static void MainX()
        {
            String englishText = "This dog eats too much vegetables after lunch, what an awful creature!";

            Int32 wordsOnSingleString = 3;

            Dictionary<String, String> translationDictionary = new Dictionary<String, String>
            {
                ["this"] = "эта",           
                ["dog"] = "собака",
                ["eats"] = "ест",
                ["too"] = "слишком",
                ["much"] = "много",
                ["vegetables"] = "овощей",
                ["after"] = "после",
                ["lunch"] = "обеда",
                ["what"] = "что",
                ["an"] = "за",
                ["awful"] = "отвратительное",
                ["creature"] = "создание"
            };

            Console.WriteLine("Task 7 Linq");

            //7. Пусть есть англо-русский словарь. Есть некоторый текст на английском языке (представлен в виде последовательности слов).
            // Необходимо сверстать из этих предложений книгу на русском языке для плохо видящих так, что
            //  на одной странице книги помещается не более N слов
            //  при этом каждое слово напечатано в верхнем регистре.
            // Перевод необходимо осуществлять пословно без учета грамматики. Считается, что каждое слово имеет перевод в словаре.

            var splitStrings = regex.Split(englishText.ToLowerInvariant());

            var result = splitStrings
                .Where(x => !String.IsNullOrEmpty(x))
                .Select((x, i) => new
                    {Key = i / wordsOnSingleString, Value = translationDictionary[x].ToUpperInvariant()}
                )
                .GroupBy(x => x.Key, x => x.Value)
                .Select(x => x.Aggregate((i, j) => i + " " + j));

            foreach (var line in result)
            {
                Console.WriteLine(line);
            }

            Console.ReadLine();
        }

        public static List<String> Task7Linq(String englishText, Int32 wordsOnSingleString, Dictionary<String, String> translationDictionary)
        {
            var splitStrings = regex.Split(englishText.ToLowerInvariant());

            var result = splitStrings
                .Where(x => !String.IsNullOrEmpty(x))
                .Select((x, i) => new
                { Key = i / wordsOnSingleString, Value = translationDictionary[x].ToUpperInvariant() }
                )
                .GroupBy(x => x.Key, x => x.Value)
                .Select(x => x.Aggregate((i, j) => i + " " + j));

            return result.ToList();
        }
    }
}