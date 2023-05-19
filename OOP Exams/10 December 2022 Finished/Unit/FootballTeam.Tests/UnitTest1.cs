using NUnit.Framework;
using System;
using System.Linq;

namespace FootballTeam.Tests
{
    public class Tests
    {
        private FootballPlayer player1;
        private FootballPlayer player2;
        private FootballTeam team;
        [SetUp]
        public void Setup()
        {
            player1 = new FootballPlayer("Pesho", 10, "Forward");
            player2 = new FootballPlayer("Dobri", 11, "Forward");
            team = new FootballTeam("Levski", 16);
            team.AddNewPlayer(player1);
            team.AddNewPlayer(player2);
        }

        [Test]
        public void TestFootballPlayerCtor()
        {
            Assert.AreEqual("Pesho", player1.Name);
            Assert.AreEqual(10, player1.PlayerNumber);
            Assert.AreEqual("Forward", player1.Position);
            Assert.AreEqual(0, player1.ScoredGoals);
        }

        [TestCase("")]
        [TestCase(null)]
        public void TestFootballWithInvalidDataForName(string value)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                FootballPlayer player = new FootballPlayer(value, 10, "Forward");
            }));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(22)]
        [TestCase(40)]
        public void TestPlayerNumInvalid(int value)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                FootballPlayer player = new FootballPlayer("Pesho", value, "Forward");
            }));
        }

        [TestCase("Goalkeepa")]
        [TestCase("Midfielda")]
        [TestCase("asd")]
        public void TestPosition(string value)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                FootballPlayer player = new FootballPlayer("Pesho", 10, value);
            }));
        }

        [Test]
        public void TestMethodScoreGoal()
        {
            Assert.AreEqual(0, player1.ScoredGoals);
            player1.Score();
            Assert.AreEqual(1, player1.ScoredGoals);
            player1.Score();
            player1.Score();
            Assert.AreEqual(3, player1.ScoredGoals);
        }

        [Test]
        public void TestFootballTeamCtor()
        {
            Assert.AreEqual("Levski", team.Name);
            Assert.AreEqual(16, team.Capacity);
            Assert.AreEqual(2, team.Players.Count);
        }

        [TestCase("")]
        [TestCase(null)]
        public void TestFootballTeamNameInvalid(string value)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                FootballTeam teamTest = new FootballTeam(value, 16);
            }));
        }

        [TestCase(0)]
        [TestCase(5)]
        [TestCase(14)]
        public void TestFootballTeamCapacityInvalid(int value)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                FootballTeam teamTest = new FootballTeam("CSKA", value);
            }));
        }

        [Test]
        public void TestFootballTeamAddNewPlayerMethod()
        {
            FootballPlayer test = new FootballPlayer("Test", 1, "Goalkeeper");
            string actual = team.AddNewPlayer(test);
            string expected = $"Added player Test in position Goalkeeper with number 1";
            Assert.AreEqual(3, team.Players.Count);
            Assert.AreEqual(expected, actual);


        }

        [Test]
        public void TestFootballTeamAddNewPlayerMethod_WhenFullCapacity()
        {
            FootballTeam fullTeam = new FootballTeam("CSKA", 15);
            FootballPlayer test1 = new FootballPlayer("Test1", 1, "Goalkeeper");
            FootballPlayer test2 = new FootballPlayer("Test2", 2, "Goalkeeper");
            FootballPlayer test3 = new FootballPlayer("Test3", 3, "Goalkeeper");
            FootballPlayer test4 = new FootballPlayer("Test4", 4, "Goalkeeper");
            FootballPlayer test5 = new FootballPlayer("Test5", 5, "Goalkeeper");
            FootballPlayer test6 = new FootballPlayer("Test6", 6, "Goalkeeper");
            FootballPlayer test7 = new FootballPlayer("Test7", 7, "Goalkeeper");
            FootballPlayer test8 = new FootballPlayer("Test8", 8, "Goalkeeper");
            FootballPlayer test9 = new FootballPlayer("Test9", 9, "Goalkeeper");
            FootballPlayer test10 = new FootballPlayer("Test10", 10, "Goalkeeper");
            FootballPlayer test11 = new FootballPlayer("Test11", 11, "Goalkeeper");
            FootballPlayer test12 = new FootballPlayer("Test12", 12, "Goalkeeper");
            FootballPlayer test13 = new FootballPlayer("Test13", 13, "Goalkeeper");
            FootballPlayer test14 = new FootballPlayer("Test14", 14, "Goalkeeper");
            FootballPlayer test15 = new FootballPlayer("Test15", 15, "Goalkeeper");
            FootballPlayer test16 = new FootballPlayer("Test16", 16, "Goalkeeper");
            fullTeam.AddNewPlayer(test1);
            fullTeam.AddNewPlayer(test2);
            fullTeam.AddNewPlayer(test3);
            fullTeam.AddNewPlayer(test4);
            fullTeam.AddNewPlayer(test5);
            fullTeam.AddNewPlayer(test6);
            fullTeam.AddNewPlayer(test7);
            fullTeam.AddNewPlayer(test8);
            fullTeam.AddNewPlayer(test9);
            fullTeam.AddNewPlayer(test10);
            fullTeam.AddNewPlayer(test11);
            fullTeam.AddNewPlayer(test12);
            fullTeam.AddNewPlayer(test13);
            fullTeam.AddNewPlayer(test14);
            fullTeam.AddNewPlayer(test15);

            string actual = fullTeam.AddNewPlayer(test16);
            string expected = $"No more positions available!";
            Assert.AreEqual(15, fullTeam.Players.Count);
            Assert.AreEqual(expected, actual);
        }

        [TestCase("Pesho")]
        public void TestPickPlayerMethod(string name)
        {
            FootballPlayer current = team.PickPlayer(name);
            Assert.AreEqual("Pesho", current.Name);
            Assert.AreEqual(10, current.PlayerNumber);
            Assert.AreEqual("Forward", current.Position);
            Assert.AreEqual(0, current.ScoredGoals);
            Assert.AreEqual(player1, current);
        }

        [TestCase(10)]
        public void TestPlayerScoreMethod(int playerNumber)
        {
            FootballPlayer current = team.Players.FirstOrDefault(p => p.PlayerNumber == playerNumber);
            Assert.AreEqual("Pesho", current.Name);
            Assert.AreEqual(10, current.PlayerNumber);
            Assert.AreEqual("Forward", current.Position);
            Assert.AreEqual(0, current.ScoredGoals);
            Assert.AreEqual(player1, current);

            string actual=  team.PlayerScore(playerNumber);
            string expected = $"Pesho scored and now has 1 for this season!";
            Assert.AreEqual(expected, actual);
        }
    }
}