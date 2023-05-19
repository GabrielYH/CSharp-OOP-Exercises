using FrontDeskApp;
using NUnit.Framework;
using System;
using System.Linq;

namespace BookigApp.Tests
{
    public class Tests
    {
        private Room room;
        private Room room2;
        private Booking booking;
        private Hotel hotel;
        [SetUp]
        public void Setup()
        {
            room = new Room(4, 20);
            room2 = new Room(6, 20);
            booking = new Booking(1, room, 5);
            hotel = new Hotel("Berlin", 5);
            hotel.AddRoom(room);
            hotel.AddRoom(room2);
            
        }

        [Test]
        public void RoomCtor()
        {
            Assert.AreEqual(4, room.BedCapacity);
            Assert.AreEqual(20, room.PricePerNight);
        }

        [TestCase(0)]
        [TestCase(-5)]
        [TestCase(-10)]
        public void NegativeOrZeroBedCapAndPriceShouldThrow(int value)
        {
            Room room;
            Assert.Throws<ArgumentException>((() =>
            {
                room = new(value, 10);
            }));
            Assert.Throws<ArgumentException>((() =>
            {
                room = new(10, value);
            }));
        }

        [Test]
        public void BookingCtor()
        {
            Assert.AreEqual(1, booking.BookingNumber);
            Assert.AreEqual(room, booking.Room);
            Assert.AreEqual(5, booking.ResidenceDuration);
        }

        [Test]
        public void HotelCtor()
        {
            Assert.AreEqual("Berlin", hotel.FullName);
            Assert.AreEqual(5, hotel.Category);
            Assert.AreEqual(2, hotel.Rooms.Count); //
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void TestHotelWithEmptyName(string value)
        {
            Assert.Throws<ArgumentNullException>((() =>
            {
                Hotel hotel = new Hotel(value, 5);
            }));
        }

        [TestCase(0)]
        [TestCase(-4)]
        [TestCase(6)]
        [TestCase(10)]
        public void TestHotelWithInvalidValue(int value)
        {
            Assert.Throws<ArgumentException>((() =>
            {
                Hotel hotel = new Hotel("Test", value);
            }));
        }

        [Test]
        public void TurnoverShouldBe0WhenCreatingNewHotel()
        {
            Hotel hotel = new("Test", 5);
            Assert.AreEqual(0, hotel.Turnover);
        }

        [Test]
        public void TestAddRoomMethod()
        {
            hotel.AddRoom(new Room(10, 20));
            Assert.AreEqual(3, hotel.Rooms.Count);
        }

        [TestCase(0)]
        [TestCase(-4)]
        public void TestBookMethod(int value)
        {
            
            Assert.Throws<ArgumentException>((() =>
            {
                hotel.BookRoom(value, 2, 2, 100);
            }));
            Assert.Throws<ArgumentException>((() =>
            {
                hotel.BookRoom(2, -1, 2, 100);
            }));
            Assert.Throws<ArgumentException>((() =>
            {
                hotel.BookRoom(2, 2, value, 100);
            }));
            Hotel hotelTest = new Hotel("Test", 5);
            Room room = new(4, 20);
            hotelTest.AddRoom(room);
            hotelTest.BookRoom(2, 2, 2, 500);
            Booking booking = hotelTest.Bookings.FirstOrDefault(b => b.BookingNumber == 1);
            Assert.AreEqual(1, hotelTest.Bookings.Count);
            Assert.AreEqual(40, hotelTest.Turnover);
            Assert.AreEqual(1, booking.BookingNumber);
            Assert.AreEqual(2, booking.ResidenceDuration);
            Assert.AreEqual(room, booking.Room);

        }




    }
}