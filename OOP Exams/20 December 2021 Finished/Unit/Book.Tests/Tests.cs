namespace Book.Tests
{
    using System;
    using NUnit.Framework;
    using static System.Net.Mime.MediaTypeNames;

    public class Tests
    {
        private Book book;
        [SetUp]
        public void Setup()
        {
            book = new Book("Sushi", "Pesho");
        }

        [Test]
        public void TestCtor()
        {
            Assert.AreEqual("Sushi", book.BookName);
            Assert.AreEqual("Pesho", book.Author);
            Assert.AreEqual(0, book.FootnoteCount);
        }

        [TestCase(null)]
        [TestCase("")]
        public void TestNameInvalid(string value)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                Book book = new Book(value, "Pesho");
            }));
        }

        [TestCase(null)]
        [TestCase("")]
        public void TestAuthorInvalid(string value)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                Book book = new Book("Pesho", value);
            }));
        }

        [Test]
        public void TestAddFootnoteMethod()
        {
            book.AddFootnote(1, "asd");
            Assert.AreEqual(1, book.FootnoteCount);
            Assert.Throws<InvalidOperationException>((() =>
            {
                book.AddFootnote(1, "asd");
            }));
        }

        [Test]
        public void TestFindFootnoteMethod()
        {
            book.AddFootnote(1, "asd");
            string actual = book.FindFootnote(1);
            string expected = $"Footnote #1: asd";
            Assert.AreEqual(expected, actual);
            Assert.Throws<InvalidOperationException>((() =>
            {
                book.FindFootnote(2);
            }));
        }

        [Test]
        public void TestAlterFootnoteMethod()
        {
            book.AddFootnote(1, "asd");
            book.AlterFootnote(1, "asg");
            string actual = book.FindFootnote(1);
            string expected = $"Footnote #1: asg";
            Assert.AreEqual(expected, actual);
            Assert.Throws<InvalidOperationException>((() =>
            {
                book.AlterFootnote(2, "asdasf");
            }));
        }
    }
}