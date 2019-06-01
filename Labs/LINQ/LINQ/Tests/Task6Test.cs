using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LINQ.Test
{
    //TODO Спросить, как тестировать анонимные типы
    public class CTask6Test
    {
        readonly String TestSentence = "Это что же получается: ходишь, ходишь в школу, а потом бац - вторая смена";

        readonly List<(Int32 Length, Int32 Count, List<String> Values)> Expected = 
            new List<(Int32 Length, Int32 Count, List<String> Values)>()
            {
                (Length: 6,  Count: 3,  Values: new List<String>{"ходишь", "ходишь", "вторая"}),
                (Length: 5,  Count: 3,  Values: new List<String>{"школу", "потом", "смена"}),
                (Length: 3,  Count: 3,  Values: new List<String>{ "Это", "что", "бац"}),
                (Length: 1,  Count: 2,  Values: new List<String>{ "в", "а" }),
                (Length: 10, Count: 1,  Values: new List<String>{ "получается" }),
                (Length: 2,  Count: 1,  Values: new List<String>{ "же" }),
            };

        [Fact]
        public void TestRegex() => Assert.Equal(Expected, CTask6.Task6Regex(TestSentence));
        [Fact]
        public void TestStringJoin() => Assert.Equal(Expected, CTask6.Task6StringJoin(TestSentence));
    }
}
