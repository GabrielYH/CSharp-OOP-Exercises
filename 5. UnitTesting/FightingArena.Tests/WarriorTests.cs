namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        private Warrior defaultWarrior;
        private Warrior enemyWarrior;
        [SetUp]
        public void Setup()
        {
            defaultWarrior = new("Gosho", 60, 100);
            enemyWarrior = new("Dobri", 10, 100);
        }
        [Test]
        public void CtorSHouldSetValues()
        {
            Assert.AreEqual("Gosho", defaultWarrior.Name);
            Assert.AreEqual(60, defaultWarrior.Damage);
            Assert.AreEqual(100, defaultWarrior.HP);
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void IfNameIsEmptyShouldThrowException(string name)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                defaultWarrior = new(name, 60, 100);
            }));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-10)]
        public void IfDamageIsZeroOrNegativeShouldThrowException(int damage)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                defaultWarrior = new("Gosho", damage, 100);
            }));
        }

        [TestCase(-1)]
        [TestCase(-10)]
        public void IfHPIsZeroOrNegativeShouldThrowException(int hp)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                defaultWarrior = new("Gosho", 60, hp);
            }));
        }

        [TestCase(29)]
        [TestCase(1)]
        [TestCase(0)]
        public void AttackWhenWarriorIsLessThan30HP_ShouldThrowException(int hp)
        {
            defaultWarrior = new("Gosho", 60, hp);
            Assert.Throws<InvalidOperationException>((() =>
            {
                defaultWarrior.Attack(new Warrior("Dobri", 10, 100));
            }));
        }

        [TestCase(29)]
        [TestCase(1)]
        [TestCase(0)]
        public void AttackingAnotherWarriorWhosHPIsLessThan30_SHouldThrowException(int hp)
        {
            defaultWarrior = new("Gosho", 60, 100);
            Assert.Throws<InvalidOperationException>((() =>
            {
                defaultWarrior.Attack(new Warrior("Dobri", 10, hp));
            }));
        }

        [Test]
        public void AttackingWarriorWithBiggerDamageThanYourHp_SHouldThrowException()
        {
            defaultWarrior = new("Gosho", 60, 100);
            Assert.Throws<InvalidOperationException>((() =>
            {
                defaultWarrior.Attack(new Warrior("Dobri", 101, 100));
            }));
        }

        [Test]
        public void AttackingWarriorWithValidDataShouldDecreaseHealthByEnemysDamage()
        {
            defaultWarrior = new("Gosho", 60, 100);
            int expectedHp = defaultWarrior.HP - enemyWarrior.Damage;
            defaultWarrior.Attack(enemyWarrior);
            Assert.AreEqual(expectedHp, defaultWarrior.HP);
        }

        [Test]
        public void EnemyWarriorShouldDieIfOurWarriorHasMoreDamageThanEnemysHealth()
        {
            defaultWarrior = new("Gosho", 67, 100);
            enemyWarrior = new Warrior("Dobri", 10, 60);
            defaultWarrior.Attack(enemyWarrior);
            Assert.AreEqual(0, enemyWarrior.HP);
        }


    }
}