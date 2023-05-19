namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Database fullDB;
        private Database smallDB;
        [SetUp]
        public void Setup()
        {
            Person[] fullDbPeople = new[]
           {
                new Person(1,"Pesho1"),
                new Person(2,"Pesho2"),
                new Person(3,"Pesho3"),
                new Person(4,"Pesho4"),
                new Person(5,"Pesho5"),
                new Person(6,"Pesho6"),
                new Person(7,"Pesho7"),
                new Person(8,"Pesho8"),
                new Person(9,"Pesho9"),
                new Person(10,"Pesho10"),
                new Person(11,"Pesho11"),
                new Person(12,"Pesho12"),
                new Person(13,"Pesho13"),
                new Person(14,"Pesho14"),
                new Person(15,"Pesho15"),
                new Person(16,"Pesho16"),
                           };
            fullDB = new(fullDbPeople);
            Person[] smallDBPEople = new[]
           {
                new Person(1,"Pesho1"),
                new Person(2,"Pesho2"),
                new Person(3,"Pesho3")
            };
            smallDB = new(smallDBPEople);
        }

        [Test]
        public void CheckIfCtorSetsValuesForPersonProperly()
        {
            string expectedUsername = "Pesho";
            long expectedId = 12345;
            Person person = new(expectedId, expectedUsername);
            Assert.AreEqual(expectedUsername, person.UserName);
            Assert.AreEqual(expectedId, person.Id);
        }

        [Test]
        public void DataBaseCounterShouldIncreaseWhenAddingPerson()
        {
            string expectedUsername = "Pesho";
            long expectedId = 12345;
            Person person = new(expectedId, expectedUsername);
            Database db = new();
            db.Add(person);
            int expectedCount = 1;
            int actualCount = db.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void CtorDataBaseShouldAddValidPeople()
        {
            int expectedCount = 3;
            int actualCount = smallDB.Count;
            Assert.AreEqual(expectedCount, actualCount);
        }
        [Test]
        public void CtorDataBaseShouldThrowExceptionWhenUsernameExists()
        {
            Person[] people = new[]
            {
                new Person(1,"Pesho1"),
                new Person(2,"Pesho1"),

            };
            Assert.Throws<InvalidOperationException>((() =>
            {
                Database db = new(people);
            }));
        }

        [Test]
        public void CtorDataBaseShouldThrowExceptionWhenIdExists()
        {
            Person[] people = new[]
            {
                new Person(1,"Pesho1"),
                new Person(1,"Pesho2"),

            };
            Assert.Throws<InvalidOperationException>((() =>
            {
                Database db = new(people);
            }));
        }

        [Test]
        public void DataBaseShouldThrowExceptionWhenAttemptedToAddMoreThan16People()
        {
            Person[] people = new[]
            {
                new Person(1,"Pesho1"),
                new Person(2,"Pesho2"),
                new Person(3,"Pesho3"),
                new Person(4,"Pesho4"),
                new Person(5,"Pesho5"),
                new Person(6,"Pesho6"),
                new Person(7,"Pesho7"),
                new Person(8,"Pesho8"),
                new Person(9,"Pesho9"),
                new Person(10,"Pesho10"),
                new Person(11,"Pesho11"),
                new Person(12,"Pesho12"),
                new Person(13,"Pesho13"),
                new Person(14,"Pesho14"),
                new Person(15,"Pesho15"),
                new Person(16,"Pesho16"),
                new Person(17,"Pesho17"),

            };
            Assert.Throws<ArgumentException>((() =>
            {
                Database db = new(people);
            }));
        }

        [Test]
        public void DataBaseCounterShouldAdd16People()
        {
            Assert.AreEqual(16, fullDB.Count);
        }

        [Test]
        public void DataBaseAddMethodShouldThrowExceptionWhenCountIs16()
        {

            Assert.Throws<InvalidOperationException>((() =>
            {
                fullDB.Add(new Person(200, "Dobri"));
            }));
        }

        [Test]
        public void RemoveMethodShouldDecreaseCounter()
        {
            smallDB.Remove();
            Assert.AreEqual(2, smallDB.Count);
        }

        [Test]
        public void RemoveWhileDataBaseIsEmptyShouldThrowException()
        {
            Database db = new();
            Assert.Throws<InvalidOperationException>((() =>
            {
                db.Remove();
            }));
        }

        [Test]
        public void FindingWithNullUsernameShouldThrowException()
        {
            Database db = new();
            Assert.Throws<ArgumentNullException>((() =>
            {
                db.FindByUsername("");
            }));

        }

        [Test]
        public void FindingWhenNameDoesntExistsShouldThrowException()
        {
            Database db = new();
            Assert.Throws<InvalidOperationException>((() =>
            {
                db.FindByUsername("IDontExist");
            }));
        }

        [Test]
        public void FindByUsernameWithValidData_UsernamesShouldBeValid()
        {

            Person expectedPerson = new Person(2, "Pesho2");
            Person actualPerson = smallDB.FindByUsername("Pesho2");
            Assert.AreEqual(expectedPerson.UserName, actualPerson.UserName);
        }

        [Test]
        public void FindByUsernameWithValidData_IdsShouldBeValid()
        {

            Person expectedPerson = new Person(2, "Pesho2");
            Person actualPerson = smallDB.FindByUsername("Pesho2");
            Assert.AreEqual(expectedPerson.Id, actualPerson.Id);
        }

        [Test]
        public void FindByIDWithValidData_IDsShouldBeValid()
        {
            Person expectedPerson = new Person(2, "Pesho2");
            Person actualPerson = smallDB.FindById(2);
            Assert.AreEqual(expectedPerson.Id, actualPerson.Id);
        }

        [Test]
        public void FindByIDWithValidData_UsernamesShouldBeValid()
        {
            Person expectedPerson = new Person(2, "Pesho2");
            Person actualPerson = smallDB.FindById(2);
            Assert.AreEqual(expectedPerson.UserName, actualPerson.UserName);
        }

        [Test]
        public void FindByIdShouldThrowExceptionWhenValueIsNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>((() =>
            {
                Person actualPerson = smallDB.FindById(-4);
            }));
        }

        [Test]
        public void MethodFindByIdShouldThrowExceptionWhenIdIsMissingInDB()
        {
           
            Assert.Throws<InvalidOperationException>((() =>
            {
                smallDB.FindById(56);
            }));
        }

    }
}