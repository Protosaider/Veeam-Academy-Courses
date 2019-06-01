using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LINQ.Test
{
    public class CTask5Test
    {
        readonly List<CElement> TestElements = new List<CElement>
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

        readonly List<Tuple<String, Int32>> Expected = new List<Tuple<String, Int32>>()
        {
            new Tuple<String, Int32>("Sword", 0),
            new Tuple<String, Int32>("Warhammer", 1),
            new Tuple<String, Int32>("Crossbow", 2),
            new Tuple<String, Int32>("Greatsword", 3),
            new Tuple<String, Int32>("Dagger", 4),
            new Tuple<String, Int32>("Broadsword", 5),
        };

        [Fact]
        public void TestLinq() => Assert.Equal(Expected, CTask5.Task5Linq(TestElements));
    }
}
