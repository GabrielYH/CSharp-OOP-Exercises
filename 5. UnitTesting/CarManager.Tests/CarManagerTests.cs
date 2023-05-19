namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private Car defaultCar;
        private Car fueledCar;
        [SetUp]
        public void Setup()
        {
            defaultCar = new("BMW", "E46", 7, 60);
            fueledCar = new("Audi", "A5", 7, 60);
            fueledCar.Refuel(60);
        }
        [Test]
        public void CarCtorShouldSetValues_ValidData()
        {
            Assert.AreEqual("BMW", defaultCar.Make);
            Assert.AreEqual("E46", defaultCar.Model);
            Assert.AreEqual(7, defaultCar.FuelConsumption);
            Assert.AreEqual(60, defaultCar.FuelCapacity);
            Assert.AreEqual(0, defaultCar.FuelAmount);
        }

        [Test]
        public void CtorShouldThrowExceptionWhenEmptyMake()
        {
            Assert.Throws<ArgumentException>((() =>
            {
                defaultCar = new("", "E46", 7, 60);
            }));
        }

        [Test]
        public void CtorShouldThrowExceptionWhenEmptyModel()
        {
            Assert.Throws<ArgumentException>((() =>
            {
                defaultCar = new("BMW", "", 7, 60);
            }));
        }
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-4)]
        public void CtorShouldThrowExceptionWhenFuelConsumptionIsZeroOrNegative(double fuelConsumption)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                defaultCar = new("BMW", "E46", fuelConsumption, 60);
            }));
        }


        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-4)]
        public void CtorShouldThrowExceptionWhenFuelCapacityIsZeroOrNegative(double fuelCapacity)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                defaultCar = new("BMW", "E46", 7, fuelCapacity);
            }));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-10)]
        public void RefuelMethodShouldThrowExceptionWhenFuelAmmountIsZeroOrNegative(double fuelAmount)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                defaultCar.Refuel(fuelAmount);
            }));
        }

        [TestCase(61)]
        [TestCase(70)]
        [TestCase(200)]
        public void RefuelWithMoreFuelThanTheCapacity(double fuelAmount)
        {
            defaultCar.Refuel(fuelAmount);
            Assert.AreEqual(defaultCar.FuelCapacity, defaultCar.FuelAmount);
        }

        [TestCase(1)]
        [TestCase(20)]
        [TestCase(59)]
        public void RefuelWithValidFuelAmount(double fuelAmount)
        {
            double expectedFuelAmount = defaultCar.FuelAmount + fuelAmount;
            defaultCar.Refuel(fuelAmount);
            Assert.AreEqual(expectedFuelAmount, defaultCar.FuelAmount);
        }

        [TestCase(10)]
        public void MethodDriveShouldThrowException_IfFuelNeededIsMoreThanFuelAmount(double distance)
        {
            double fuelNeeded = (distance / 100) * defaultCar.FuelConsumption; //
            Assert.Throws<InvalidOperationException>((() =>
            {
                defaultCar.Drive(distance);
            }));
        }

        [TestCase(1)]
        [TestCase(50)]
        [TestCase(100)]
        public void MethodDriveShouldDecreaseFuelAmount(double distance)
        {
            double fuelNeeded = (distance / 100) * fueledCar.FuelConsumption;
            double expectedFuelAmount = fueledCar.FuelAmount - fuelNeeded;
            fueledCar.Drive(distance);
            Assert.AreEqual(expectedFuelAmount, fueledCar.FuelAmount);
        }

    }
}