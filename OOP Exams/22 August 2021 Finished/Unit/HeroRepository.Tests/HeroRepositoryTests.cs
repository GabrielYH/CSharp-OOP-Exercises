using System;
using System.Collections.Generic;
using NUnit.Framework;

public class HeroRepositoryTests
{
    private Hero hero;
    private HeroRepository repository;
    [SetUp]
    public void Setup()
    {
        hero = new Hero("Blondi", 100);
        repository = new HeroRepository();
        repository.Create(hero);
    }

    [Test]
    public void TestHeroCtor()
    {
        Assert.AreEqual("Blondi", hero.Name);
        Assert.AreEqual(100, hero.Level);
    }

    [Test]
    public void TestRepoCtor()
    {
        Assert.AreEqual(1, repository.Heroes.Count);
    }

    [Test]
    public void TestRepoCreateMethod()
    {
        string actual = repository.Create(new Hero("Charli", 10));
        string expected = $"Successfully added hero Charli with level 10";
        Assert.AreEqual(2, repository.Heroes.Count);
        Assert.AreEqual(expected, actual);
        Assert.Throws<ArgumentNullException>((() =>
        {
            repository.Create(null);
        }));
        Assert.Throws<InvalidOperationException>((() =>
        {
            repository.Create(new Hero("Charli", 10));
        }));
    }

    [TestCase("")]
    [TestCase(" ")]
    [TestCase(null)]
    public void TestRemoveMethod(string value)
    {

        Assert.Throws<ArgumentNullException>((() =>
        {
            repository.Remove(value);
        }));
        bool actual1 = repository.Remove("Blondi");
        Assert.AreEqual(0, repository.Heroes.Count);
        bool actual2 = repository.Remove("asd");
        Assert.AreEqual(true, actual1);
        Assert.AreEqual(false, actual2);
    }

    [Test]
    public void TestHighestLevelHeroMethod()
    {
        repository.Create(new Hero("Test", 200));
        Hero hero = new Hero("Test2", 300);
        repository.Create(hero);
        Hero highest = repository.GetHeroWithHighestLevel();
        Assert.AreEqual(hero, highest);
        // na prazno kakvo shte dade
    }

    [Test]
    public void TestGetHeroMethod()
    {
        
        Hero hero = repository.GetHero("Blondi");
        Assert.AreEqual(this.hero, hero);
        
    }
}