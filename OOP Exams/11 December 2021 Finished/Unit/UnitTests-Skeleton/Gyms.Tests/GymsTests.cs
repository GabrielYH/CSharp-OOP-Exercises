using NUnit.Framework;
using System;

namespace Gyms.Tests
{
    public class GymsTests
    {
        private Athlete athlete;
        private Gym gym;
        [SetUp]
        public void Setup()
        {
            athlete = new Athlete("Pesho");
            gym = new Gym("Sparta", 3);
            gym.AddAthlete(athlete);
        }

        [Test]
        public void TestCtorAthlete()
        {
            Assert.AreEqual("Pesho", athlete.FullName);
            Assert.AreEqual(false, athlete.IsInjured);
        }

        [Test]
        public void TestCtorGym()
        {
            Assert.AreEqual(1, gym.Count);
            Assert.AreEqual(3, gym.Capacity);
            Assert.AreEqual("Sparta", gym.Name);
        }

        [TestCase("")]
        [TestCase(null)]
        public void TestNameInvalid(string value)
        {
            Assert.Throws<ArgumentNullException>((() =>
            {
                Gym gym = new Gym(value, 5);
            }));
        }

        [TestCase(-1)]
        [TestCase(-10)]
        public void TestNameCapacity(int value)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                Gym gym = new Gym("Test", value);
            }));
        }

        
        [Test]
        public void TestAddAthleteMethod()
        {
            gym.AddAthlete(new Athlete("asd"));
            gym.AddAthlete(new Athlete("asd3"));
            Assert.AreEqual(3, gym.Count);
            Assert.Throws<InvalidOperationException>((() =>
            {
                gym.AddAthlete(new Athlete("asd2"));
            }));
        }

        [Test]
        public void TestRemoveAthleteMethod()
        {
            Assert.Throws<InvalidOperationException>((() =>
            {
                gym.RemoveAthlete("asfas");
            }));
            gym.RemoveAthlete(athlete.FullName);
            Assert.AreEqual(0, gym.Count);
            
        }

        [Test]
        public void TestInjureAthleteMethod()
        {
            Assert.Throws<InvalidOperationException>((() =>
            {
                gym.InjureAthlete("asfas");
            }));
            Athlete actual = gym.InjureAthlete(athlete.FullName);
            Assert.AreEqual(true, athlete.IsInjured);
            Assert.AreEqual(athlete, actual);

        }

        [Test]
        public void TestReportMethod()
        {
            gym.AddAthlete(new Athlete("asd1"));
            gym.AddAthlete(new Athlete("asd2"));
            string actual = gym.Report();
            string expected = $"Active athletes at Sparta: Pesho, asd1, asd2";
            Assert.AreEqual(expected, actual);

        }
    }
}
