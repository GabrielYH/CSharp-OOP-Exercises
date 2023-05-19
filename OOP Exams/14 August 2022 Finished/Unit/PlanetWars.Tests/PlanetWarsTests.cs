using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            private Weapon weapon;
            private Weapon weapon2;
            private Planet planet;
            [SetUp]
            public void Setup()
            {
                this.weapon = new Weapon("Nuclear", 100, 10);
                this.weapon2 = new Weapon("Atomic", 100, 10);
                planet = new Planet("Earth", 300);
                planet.AddWeapon(weapon);
                planet.AddWeapon(weapon2);
            }

            [Test]
            public void WeaponCtor()
            {
                Assert.AreEqual("Nuclear", weapon.Name);
                Assert.AreEqual(10, weapon.DestructionLevel);
                Assert.AreEqual(100, weapon.Price);
            }

            [TestCase(-1)]
            [TestCase(-10)]
            public void TestNegativePrice(int value)
            {

                Assert.Throws<ArgumentException>((() =>
                {
                    Weapon weapon = new Weapon("Test", value, 10);
                }));
            }

            [Test]
            public void TestIncreaseDestructionLevel()
            {
                weapon.IncreaseDestructionLevel();
                Assert.AreEqual(11, weapon.DestructionLevel);
                weapon.IncreaseDestructionLevel();
                weapon.IncreaseDestructionLevel();
                weapon.IncreaseDestructionLevel();
                Assert.AreEqual(14, weapon.DestructionLevel);

            }

            [Test]
            public void TestIsNuclear()
            {
                Weapon wep = new Weapon("Test", 10, 9);
                Assert.AreEqual(false, wep.IsNuclear);
                wep.IncreaseDestructionLevel();
                wep.IncreaseDestructionLevel();
                wep.IncreaseDestructionLevel();
                Assert.AreEqual(true, wep.IsNuclear);
            }


            [Test]
            public void TestPlanetCtor()
            {
                Assert.AreEqual("Earth", planet.Name);
                Assert.AreEqual(300, planet.Budget);
                Assert.AreEqual(2, planet.Weapons.Count);
                Assert.AreEqual(20, planet.MilitaryPowerRatio);
            }

            [TestCase("")]
            [TestCase(null)]
            public void InvalidPlanetName(string value)
            {
                Assert.Throws<ArgumentException>((() =>
                {
                    Planet planet = new Planet(value, 500);
                }));
            }

            [TestCase(-1)]
            [TestCase(-10)]
            public void InvalidBudget(double value)
            {
                Assert.Throws<ArgumentException>((() =>
                {
                    Planet planet = new Planet("Test", value);
                }));

            }

            [Test]
            public void TestWeaponsProp()
            {
                List<Weapon> weapons = new List<Weapon>();
                weapons.Add(weapon);
                weapons.Add(weapon2);
                Assert.AreEqual(weapons, planet.Weapons);
            }

            [Test]
            public void SpendFunds()
            {
                planet.SpendFunds(200);
                Assert.AreEqual(100, planet.Budget);
                Assert.Throws<InvalidOperationException>((() =>
                {
                    planet.SpendFunds(400);
                }));

            }

            [Test]
            public void AddWeapon()
            {
                planet.AddWeapon(new Weapon("Test", 100, 10));
                Assert.AreEqual(3, planet.Weapons.Count);
                Assert.Throws<InvalidOperationException>((() =>
                {
                    planet.AddWeapon(weapon);
                }));

            }

            [Test]
            public void RemoveWeapon()
            {
                planet.RemoveWeapon(weapon.Name);
                Assert.AreEqual(1, planet.Weapons.Count);
            }

            [Test]
            public void UpgradeWeapon()
            {
                Assert.Throws<InvalidOperationException>((() =>
                {
                    planet.UpgradeWeapon("asd");
                }));
                planet.UpgradeWeapon("Nuclear");
                Assert.AreEqual(11, weapon.DestructionLevel);
                
            }

            [Test]
            public void DestructOpponent()
            {
                Planet opponent = new Planet("Mars", 100);
                opponent.AddWeapon(weapon);
                opponent.AddWeapon(weapon2);
                Assert.Throws<InvalidOperationException>((() =>
                {
                    planet.DestructOpponent(opponent);
                }));

                planet.AddWeapon(new Weapon("Test", 100, 15));
                string actual = planet.DestructOpponent(opponent);
                string expected = $"{opponent.Name} is destructed!";
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void Profit()
            {
                planet.Profit(100);
                Assert.AreEqual(400, planet.Budget);
            }
        }
    }
}
