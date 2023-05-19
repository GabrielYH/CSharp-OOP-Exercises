using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        public class RepairsShopTests
        {
            private Car car;
            private Car brokenCar;
            private Garage garage;
            [SetUp]
            public void Setup()
            {
                car = new Car("BMW", 0);
                brokenCar = new Car("Audi", 10);
                garage = new Garage("ST", 5);
                garage.AddCar(brokenCar);
            }

            [Test]
            public void TestCarCtor()
            {
                Assert.AreEqual("BMW", car.CarModel);
                Assert.AreEqual(0, car.NumberOfIssues);
                Assert.AreEqual(true, car.IsFixed);
                Assert.AreEqual(false, brokenCar.IsFixed);
            }

            [Test]
            public void TestGarageCtor()
            {
                Assert.AreEqual("ST", garage.Name);
                Assert.AreEqual(5, garage.MechanicsAvailable);
                Assert.AreEqual(1, garage.CarsInGarage);
            }

            [TestCase("")]
            [TestCase(null)]
            public void TestName(string value)
            {
                Assert.Throws<ArgumentNullException>((() =>
                {
                    Garage garage = new Garage(value, 10);
                }));
            }

            [TestCase(0)]
            [TestCase(-1)]
            [TestCase(-10)]
            public void TestMechanicsInvalid(int value)
            {
                Assert.Throws<ArgumentException>((() =>
                {
                    Garage garage = new Garage("TEST", value);
                }));
            }

            [Test]
            public void TestAddCarMethod()
            {
                Garage garage = new Garage("TEST", 1);
                Assert.AreEqual(0, garage.CarsInGarage);
                garage.AddCar(brokenCar);
                Assert.AreEqual(1, garage.CarsInGarage);
                Assert.Throws<InvalidOperationException>((() =>
                {
                    garage.AddCar(new Car("Mercedes", 5));
                }));
            }

            [Test]
            public void TestCarToFix()
            {
                Assert.Throws<InvalidOperationException>((() =>
                {
                    garage.FixCar("asd");
                }));
                Car test = new Car("Test", 10);
                garage.AddCar(test);
                Car returnedCar = garage.FixCar("Test");
                Assert.AreEqual(0, test.NumberOfIssues);
                Assert.AreEqual(true, test.IsFixed);
                Assert.AreEqual(test, returnedCar);

            }

            [Test]
            public void TestRemoveFixedCarMethod()
            {
                Assert.Throws<InvalidOperationException>((() =>
                {
                    garage.RemoveFixedCar();
                }));
                garage.AddCar(car);
                int actual = garage.RemoveFixedCar();
                Assert.AreEqual(1, actual);
            }

            [Test]
            public void TestReportMethod()
            {
                garage.AddCar(new Car("Pesho", 5));
                garage.AddCar(new Car("Mesho", 5));
                garage.AddCar(new Car("Gesho", 5));
                string expected = $"There are 4 which are not fixed: Audi, Pesho, Mesho, Gesho.";
                string actual = garage.Report();
                Assert.AreEqual(expected, actual);
            }
        }
    }
}