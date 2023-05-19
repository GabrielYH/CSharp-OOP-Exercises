namespace Presents.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class PresentsTests
    {
        private Bag bag;
        private Present present;
        [SetUp]
        public void Setup()
        {
            bag = new Bag();
            present = new Present("Kofa", 10);
            bag.Create(present);
        }

        [Test]
        public void TestPresentCtor()
        {
            Assert.AreEqual("Kofa", present.Name);
            Assert.AreEqual(10, present.Magic);
        }

        [Test]
        public void TestBagCtor()
        {
            var collection = new List<Present>();
            collection.Add(present);
            Assert.AreEqual(collection, bag.GetPresents());
            Assert.AreEqual(1, bag.GetPresents().Count);
        }

        [Test]
        public void TestCreateMethod()
        {
            Assert.Throws<ArgumentNullException>((() =>
            {
                bag.Create(null);
            }));
            Assert.Throws<InvalidOperationException>((() =>
            {
                bag.Create(present);
            }));
            string actual = bag.Create(new Present("Test", 10));
            Assert.AreEqual(2, bag.GetPresents().Count);
            string expected = $"Successfully added present Test.";
            Assert.AreEqual(expected, actual);

        }

        [Test]
        public void TestRemoveMethod()
        {
            bag.Remove(present);
            Assert.AreEqual(0, bag.GetPresents().Count);
        }

        [Test]
        public void TestGetPresentWithLeastMagicMethod()
        {
            Present least = new Present("Test", 1);
            bag.Create(least);
            Present actual = bag.GetPresentWithLeastMagic();
            Assert.AreEqual(least, actual);
        }

        [Test]
        public void TestGetPresentMethod()
        {
            Present actual = bag.GetPresent("Kofa");
            Present actual2 = bag.GetPresent("asd");
            Assert.AreEqual(present, actual);
            Assert.AreEqual(null, actual2);
        }

    }
}
