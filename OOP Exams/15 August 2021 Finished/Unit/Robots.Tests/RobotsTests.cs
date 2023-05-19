namespace Robots.Tests
{
    using NUnit.Framework;
    using System;

    public class RobotsTests
    {
        private Robot robot;
        private RobotManager manager;
        [SetUp]
        public void Setup()
        {
            robot = new Robot("Pesho", 100);
            manager = new RobotManager(2);
            manager.Add(robot);
        }

        [Test]
        public void TestRobotCtor()
        {
            Assert.AreEqual("Pesho", robot.Name);
            Assert.AreEqual(100, robot.MaximumBattery);
            Assert.AreEqual(100, robot.Battery);
        }

        [Test]
        public void TestRobotManagerCtor()
        {
            Assert.AreEqual(1, manager.Count);
            Assert.AreEqual(2, manager.Capacity);
        }

        [TestCase(-1)]
        [TestCase(-10)]
        public void TestCapacityInvalid(int value)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                RobotManager manager = new RobotManager(value);
            }));
        }

        [Test]
        public void TestAddMethod()
        {
            Assert.Throws<InvalidOperationException>((() =>
            {
                manager.Add(new Robot("Pesho", 100));
            }));
            manager.Add(new Robot("Dobri", 100));
            Assert.AreEqual(2, manager.Count);
            Assert.Throws<InvalidOperationException>((() =>
            {
                manager.Add(new Robot("asd", 100));
            }));
        }

        [Test]
        public void TestRemoveMethod()
        {
            Assert.Throws<InvalidOperationException>((() =>
            {
                manager.Remove("Nqma me");
            }));
            manager.Remove("Pesho");
            Assert.AreEqual(0, manager.Count);
        }

        [Test]
        public void TestWorkMethod()
        {
            Assert.Throws<InvalidOperationException>((() =>
            {
                manager.Work("asd", "Kopai", 20);
            }));
            Assert.Throws<InvalidOperationException>((() =>
            {
                manager.Work("Pesho", "Kopai", 220);
            }));
            manager.Work("Pesho", "Kopai", 20);
            Assert.AreEqual(80, robot.Battery);
        }

        [Test]
        public void TestChargeMethod()
        {
            Assert.Throws<InvalidOperationException>((() =>
            {
                manager.Charge("Nqma me");
            }));
            manager.Work("Pesho", "Kopai", 20);
            Assert.AreEqual(80, robot.Battery);
            manager.Charge("Pesho");
            Assert.AreEqual(100, robot.Battery);
        }
    }
}
