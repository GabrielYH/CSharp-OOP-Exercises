namespace Database.Tests
{
    using NUnit.Framework;
    using System;
    using System.Security.Cryptography;

    [TestFixture]
    public class DatabaseTests
    {
        private Database defDb;
        [SetUp]
        public void Setup()
        {
            defDb = new();
        }

        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2, 3, 4, 5 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        public void CheckIfConstructorSetValuesProperly(int[] data)
        {
            Database db = new Database(data);

            //Assert
            int expectedCount = data.Length;
            int actualCount = db.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 })]
        public void CtorShouldThrowExceptionWhenInputCountIsAboveSixteen(int[] data)
        {
            Assert.Throws<InvalidOperationException>((() =>
            {
                Database db = new(data);
            }));
        }

        [TestCase(new int[] { })]
        public void TestShouldThrowExceptionIfAttemptedToRemoveFromEmptyDataBase(int[] data)
        {
            Assert.Throws<InvalidOperationException>((() =>
            {
                Database db = new(data);
                db.Remove();
            }));
        }
        [Test]
        public void TestFetchShouldReturnCorrectDataInformation()
        {
            defDb = new(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            int[] expectedData = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            int[] actualData = defDb.Fetch();
            CollectionAssert.AreEqual(expectedData, actualData);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 })]
        public void TestShouldReturnCorrectCountWhenFetchingInformation(int[] data)
        {
            Database db = new(data);
            int expectedCount = data.Length;
            int[] fetchedArray = db.Fetch();
            int actualCount = fetchedArray.Length;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void AddingAlementShouldIncreaseCount()
        {
            int elements = 3;
            for (int i = 0; i < elements; i++)
            {
                defDb.Add(i);
            }
            Assert.AreEqual(elements, defDb.Count);

        }

        [Test]
        public void AddingAlementWhenArrayIsFullShouldThrowException()
        {
            defDb = new(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            int elements = 3;
            Assert.Throws<InvalidOperationException>((() =>
            {
                for (int i = 0; i < elements; i++)
                {
                    defDb.Add(i);
                }
            }));
        }

        [Test]
        public void AddingElementsShouldAddThemToTheCollection()
        {
            for (int i = 1; i <= 5; i++)
            {
                defDb.Add(i);
            }
            int[] expectedData = new int[] { 1, 2, 3, 4, 5 };
            int[] actualData = defDb.Fetch();
            CollectionAssert.AreEqual(expectedData, actualData);
        }

        [Test]
        public void CountShouldReduceWhenRemoveElement()
        {
            defDb = new(new int[] { 1, 2, 3, 4 });
            defDb.Remove();
            int expectedCount = 3;
            int actualCount = defDb.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }
    }
}
