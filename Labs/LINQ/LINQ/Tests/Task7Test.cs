using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LINQ.Test
{
    public class CTask7Test
    {
        readonly String TestEnglishText = "This dog eats too much vegetables after lunch, what an awful creature!";
        readonly Int32 TestWordsOnSingleString = 3;
        readonly Dictionary<String, String> TestTranslationDictionary = new Dictionary<String, String>
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
        readonly List<String> Expected = new List<String>
        {
            "ЭТА СОБАКА ЕСТ",
            "СЛИШКОМ МНОГО ОВОЩЕЙ",
            "ПОСЛЕ ОБЕДА ЧТО",
            "ЗА ОТВРАТИТЕЛЬНОЕ СОЗДАНИЕ",
        };

        [Fact]
        public void TestLinq() => Assert.Equal(Expected, CTask7.Task7Linq(TestEnglishText, TestWordsOnSingleString, TestTranslationDictionary));
    }
}
