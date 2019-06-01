using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LINQ.Test
{
    public class CTask4Test
    {
        List<CElement> TestElements = new List<CElement>
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

        Char TestDelimiter = ';';

        String Expected = "Axe;Greatsword;Dagger;Broadsword;Staff;Spear;Bow";

        [Fact]
        public void TestLinqOnly() => Assert.Equal(Expected, CTask4.Task4LinqOnly(TestElements, TestDelimiter));
        [Fact]                                     
        public void TestStringJoin() => Assert.Equal(Expected, CTask4.Task4StringJoin(TestElements, TestDelimiter));
        [Fact]                                     
        public void TestStringBuilder() => Assert.Equal("Axe;Greatsword;Dagger;Broadsword;Staff;Spear;Bow;", CTask4.Task4StringBuilder(TestElements, TestDelimiter));
    }
}
