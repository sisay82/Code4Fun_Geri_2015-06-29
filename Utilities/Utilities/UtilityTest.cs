using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Utilities
{
    [TestFixture]
    public class RandomArrayFillTest : AssertionHelper
    {
        Object[] ints = new Object[20];

        [SetUp]
        public void Init()
        {
            Utility.RandomArrayFill(ints);
        }

        [Test]
        public void Check01NotNullElements()
        {
            Assert.That(ints, Is.All.Not.Null);
        }

        [Test]
        public void Check02IntTypeElements()
        {
            Assert.That(ints, Is.All.InstanceOf(typeof(int)));
        }
        
        [Test]
        public void Check03UniqueElements()
        {
            Assert.That(ints, Is.Unique);
        }

        [Test]
        public void Check04ValuesRange()
        {
            Assert.That(ints, Is.All.GreaterThan(0));
            Assert.That(ints, Is.All.LessThanOrEqualTo(ints.Length));
        }

    }

    [TestFixture]
    public class LatinLetterEncodingTest : AssertionHelper
    {
        [TestCase("af5c a#!", "1653 1#!")]
        [TestCase("hello 45", "85121215 45")]
        [TestCase("jaj-a", "10110-1")]
        public void CheckEncodedString(String input, String output)
        {
            Assert.That(Utility.GetLatinLetterEncoded(input), Is.EqualTo(output));
        }
    }

    [TestFixture]
    public class BracketCheckTest : AssertionHelper
    {
        [TestCase("afdoi$%dw", true)]
        [TestCase("(hello (world))", true)]
        [TestCase("((hello (world))", false)]
        [TestCase("(c(oder)) b(yte)", true)]
        [TestCase("(coder))byte(", false)]
        public void CheckCorrectBracket(String input, Boolean result)
        {
            Assert.That(Utility.CheckBackets(input) == result);
        }
    }
}
