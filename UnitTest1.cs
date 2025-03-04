using NUnit.Framework.Legacy;

namespace TestsShopApp
{
    [TestFixture]
    public class Tests
    {
        [TestCase(1,1,1)]
        [TestCase(2,5,10)]
        public void Test(int x, int multiply, int result)
        {
            int value = x + multiply;
            //ClassicAssert.AreEqual(value, result);
            Assert.That(value, Is.EqualTo(result));
        }
    }
}
