using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        private Axe axe;
        private Dummy dummy;
        private int attackPoints = 10;
        private int durabilityPoints = 15;
        [SetUp]
        public void Setup()
        {
            this.axe = new Axe(attackPoints, durabilityPoints);
            this.dummy = new Dummy(100, 100);
        }
        [Test]
        public void IfWeaponLosesDurabilityAfterEachAttackShouldLose()
        {
            for (int i = 1; i <= 5; i++)
            {
                this.axe.Attack(this.dummy);
            }
            Assert.AreEqual(durabilityPoints - 5, this.axe.DurabilityPoints);
        }

        [Test]
        public void Test_ConstructorShouldSetDataProperly()
        {

            Assert.AreEqual(10, this.axe.AttackPoints);
            Assert.AreEqual(15, this.axe.DurabilityPoints);
        }

        [Test]
        public void Test_AttackingWithZeroDurability_ShouldThrow()
        {
            axe = new Axe(10, 0);
            Assert.Throws<InvalidOperationException>((() =>
            {
                axe.Attack(dummy);
            }));

        }
        [Test]
        public void Test_AttackingWithNegativeDurability_ShouldThrowException()
        {
            axe = new Axe(10, -4);
            Assert.Throws<InvalidOperationException>((() =>
            {
                axe.Attack(dummy);
            }));
        }


    }
}