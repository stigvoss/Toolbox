using NUnit.Framework;
using System.Linq;
using Toolbox.Collections.Generic.Extensions;

namespace Toolbox.Extensions.Tests
{
    [TestFixture]
    public class An_IEnumerable
    {
        [Test]
        public void Can_Convert_Using_ToConcurrentHashSet()
        {
            var data = Enumerable.Range(0, 4).ToConcurrentHashSet();
            var expected = new int[] { 0, 1, 2, 3 };
            Assert.That(data, Is.EquivalentTo(expected));
        }

        [Test]
        public void Can_Convert_Using_ToConcurrentHashSet_With_Selector()
        {
            var data = Enumerable.Range(0, 4).ToConcurrentHashSet(x => x * 2);
            var expected = new int[] { 0, 2, 4, 6 };
            Assert.That(data, Is.EquivalentTo(expected));
        }
    }
}
