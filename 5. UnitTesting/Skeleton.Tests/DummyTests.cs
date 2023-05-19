using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        private Axe axe;
        private Dummy dummy;
        private Dummy deadDummy;
        private int health;
        private int experience;

        [SetUp]
        public void Setup()
        {
            health = 10;
            experience = 15;
            this.dummy = new Dummy(health, experience);
            this.deadDummy = new(health, experience);
            deadDummy.TakeAttack(health + 10);
        }
        [Test]
        public void Test_DoesDoesConstructorSetValuesProperly_ShoudWork()
        {
            Assert.AreEqual(health, dummy.Health);
        }

        [Test]
        public void Test_DoesDummyLoseHealthWhenAttacked_ShoudLose()
        {
            dummy.TakeAttack(5);
            Assert.AreEqual(health-5, dummy.Health);
        }

        [Test]
        public void Test_DummysHealthIsZero_ShouldThrowException()
        {
            dummy.TakeAttack(health);
            Assert.Throws<InvalidOperationException>((() =>
            {
                dummy.TakeAttack(1);
            }));
        }
        [Test]
        public void Test_DummysHealthIsNegative_ShouldThrowException()
        {
            
            Assert.Throws<InvalidOperationException>((() =>
            {
                deadDummy.TakeAttack(1);
            }));
        }
        [Test]
        public void Test_DummyShouldGiveExperienceWhenDead()
        {
            
            var experience = deadDummy.GiveExperience();
            Assert.AreEqual(this.experience, experience);
        }
        [Test]
        public void Test_AliveDummyShouldNotGiveExperience()
        {
            dummy = new Dummy(10, 10);

            Assert.Throws<InvalidOperationException>((() =>
            {
                var experience = dummy.GiveExperience();
            }));
        }

    }
}