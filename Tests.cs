using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace DeepCopyTester
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void CloneShouldHandleEnumerableProperty()
        {
            //var original = new MyClass { EnumerableProperty = Enumerable.Range(0, 1) }; // All is good
            var original = new MyClass { EnumerableProperty = Enumerable.Range(0, 1).Select(i => i) }; // Fails
            //var original = new MyClass { EnumerableProperty = Enumerable.Range(0, 1).Select(i => i).ToList() }; // All is good

            var clone = original.DeepCopyByExpressionTree();

            var enumeratedClonedProperty = clone.EnumerableProperty.Single(); // Boom! NullReferenceException at System.Linq.Enumerable.SelectRangeIterator`1.MoveNext()
            Assert.AreEqual(0, enumeratedClonedProperty);
        }

        private class MyClass
        {
            public IEnumerable<int> EnumerableProperty { get; set; }
        }
    }
}
