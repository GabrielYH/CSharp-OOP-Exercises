namespace Aquariums.Tests
{
    using NUnit.Framework;
    using System;

    public class AquariumsTests
    {
        private Fish fish;
        private Aquarium aquarium;
        [SetUp]
        public void Setup()
        {
            fish = new Fish("Nemo");
            aquarium = new Aquarium("Voden", 2);
            aquarium.Add(fish);
        }

        [Test]
        public void CtorFish()
        {
            Assert.AreEqual("Nemo", fish.Name);
            Assert.AreEqual(true, fish.Available);
        }

        [Test]
        public void CtorAquarium()
        {
            Assert.AreEqual("Voden", aquarium.Name);
            Assert.AreEqual(2, aquarium.Capacity);
            Assert.AreEqual(1, aquarium.Count);
        }

        [TestCase("")]
        [TestCase(null)]
        public void InvalidName(string value)
        {
            Assert.Throws<ArgumentNullException>((() =>
            {
                Aquarium asd = new Aquarium(value, 5);
            }));
        }

        [TestCase(-1)]
        [TestCase(-10)]
        public void InvalidCap(int value)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                Aquarium asd = new Aquarium("TEst", value);
            }));
        }

        [Test]
        public void TestAddMethod()
        {
            Fish fisha = new Fish("asd");
            aquarium.Add(fisha);
            Assert.AreEqual(2, aquarium.Count);
            Fish fisha2 = new Fish("asd2");
            Assert.Throws<InvalidOperationException>((() =>
            {
                aquarium.Add(fisha2);
            }));

        }

        [Test]
        public void TestRemoveMethod()
        {
            Assert.Throws<InvalidOperationException>((() =>
            {
                aquarium.RemoveFish("asd");
            }));
            aquarium.RemoveFish("Nemo");
            Assert.AreEqual(0, aquarium.Count);
        }

        [Test]
        public void TestSellMethod()
        {
            Assert.Throws<InvalidOperationException>((() =>
            {
                aquarium.SellFish("asd");
            }));
            Fish actual = aquarium.SellFish("Nemo");
            Assert.AreEqual(false, fish.Available);
            Assert.AreEqual(fish, actual);
        }

        [Test]
        public void TestReportMethod()
        {
            aquarium.Add(new Fish("Patrik"));
            string actual = aquarium.Report();
            string expected = $"Fish available at Voden: Nemo, Patrik";
            Assert.AreEqual(expected, actual);
        }
    }
}
