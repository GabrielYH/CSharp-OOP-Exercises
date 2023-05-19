namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;
        private Warrior defaultWarrior;
        private Warrior enemyWarrior;
        [SetUp]
        public void Setup()
        {
            arena = new();
            defaultWarrior = new("Gosho", 60, 100);
            enemyWarrior = new("Dobri", 10, 100);
            arena.Enroll(defaultWarrior);
            arena.Enroll(enemyWarrior);
        }

        [Test]
        public void EnrollngShouldIncreaseCounter()
        {
            arena.Enroll(new Warrior("Test", 10, 10));
            Assert.AreEqual(3, arena.Count);
        }

        [Test]
        public void EnrollngExistingWarrior_ShouldThrowException()
        {
            Assert.Throws<InvalidOperationException>((() =>
            {
                arena.Enroll(new Warrior("Dobri", 10, 100));
            }));
        }

        [Test]
        public void MethodFightShouldThrowExceptionIfAttackingWarriorIsNotEnrolled()
        {
            Assert.Throws<InvalidOperationException>((() =>
            {
                arena.Fight("Test", "Gosho");
            }));
        }
        [Test]
        public void MethodFightShouldThrowExceptionIfDefendingIsNotEnrolled()
        {
            Assert.Throws<InvalidOperationException>((() =>
            {
                arena.Fight("Gosho", "Test");
            }));
        }

        [Test]
        public void MethodFightValidData()
        {
            arena.Fight("Gosho", "Dobri");
            Assert.AreEqual(40, enemyWarrior.HP);
        }
    }
}

