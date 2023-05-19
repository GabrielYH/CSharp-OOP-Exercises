using BookingApp.Core.Contracts;
using BookingApp.Models.Bookings;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Core
{
    public class Controller : IController
    {
        private IRepository<IHotel> hotels;
        public Controller()
        {
            hotels = new HotelRepository();
        }
        public string AddHotel(string hotelName, int category)
        {
            IHotel hotel = this.hotels.Select(hotelName);
            if (hotel != null)
            {
                return string.Format(OutputMessages.HotelAlreadyRegistered, hotelName);
            }

            IHotel createdHotel = new Hotel(hotelName, category);
            this.hotels.AddNew(createdHotel);
            return string.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            if (!hotels.All().Any(h => h.Category == category))
            {
                return string.Format(OutputMessages.CategoryInvalid, category);
            }
            var orderedHotels = this.hotels.All().OrderBy(h => h.FullName).ToList();
            IRepository<IRoom> rooms = new RoomRepository();
            foreach (var hotel in orderedHotels)
            {
                foreach (var room in hotel.Rooms.All())
                {
                    if (room.PricePerNight > 0)
                    {
                        rooms.AddNew(room);
                    }
                }
            }
            var orderedRooms = rooms.All().OrderBy(r => r.BedCapacity).ToList();
            int allMembers = adults + children;
            IRoom chosenRoomWithExactCap = orderedRooms.FirstOrDefault(r => r.BedCapacity == allMembers);
            if (chosenRoomWithExactCap != null)
            {
                IHotel hotelOfTheRoom = orderedHotels.FirstOrDefault(h => h.Rooms.All().FirstOrDefault(r => r.BedCapacity == allMembers) == chosenRoomWithExactCap); // big check
                int bookingNumber = hotelOfTheRoom.Bookings.All().Count + 1; // check
                IBooking booking = new Booking(chosenRoomWithExactCap, duration, adults, children, bookingNumber);
                hotelOfTheRoom.Bookings.AddNew(booking);
                return string.Format(OutputMessages.BookingSuccessful, bookingNumber, hotelOfTheRoom.FullName);
            }
            else
            {
                IRoom chosenRoomThatSatisfies = orderedRooms.FirstOrDefault(r => r.BedCapacity >= allMembers);
                if (chosenRoomThatSatisfies == null) // check
                {
                    return string.Format(OutputMessages.RoomNotAppropriate);
                }
                IHotel hotelOfTheRoom = orderedHotels.FirstOrDefault(h => h.Rooms.All().FirstOrDefault(r => r.BedCapacity >= allMembers && r == chosenRoomThatSatisfies) != null); // big check
                int bookingNumber = hotelOfTheRoom.Bookings.All().Count + 1; // check
                IBooking booking = new Booking(chosenRoomThatSatisfies, duration, adults, children, bookingNumber);
                hotelOfTheRoom.Bookings.AddNew(booking);
                return string.Format(OutputMessages.BookingSuccessful, bookingNumber, hotelOfTheRoom.FullName);
            }
        }

        public string HotelReport(string hotelName)
        {
            IHotel hotel = this.hotels.All().FirstOrDefault(h => h.FullName == hotelName);
            if (hotel == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }
            StringBuilder hotelSB = new();
            hotelSB.AppendLine($"Hotel name: {hotelName}");
            hotelSB.AppendLine($"--{hotel.Category} star hotel");
            hotelSB.AppendLine($"--Turnover: {hotel.Turnover:f2} $");
            hotelSB.AppendLine($"--Bookings:");
            if (hotel.Bookings.All().Count == 0)
            {
                hotelSB.AppendLine();
                hotelSB.AppendLine($"none");
            }
            else
            {
                foreach (var booking in hotel.Bookings.All())
                {
                    hotelSB.AppendLine();
                    hotelSB.AppendLine(booking.BookingSummary());
                }
            }
            return hotelSB.ToString().Trim();

        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            IHotel hotel = this.hotels.Select(hotelName);
            if (hotel == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }
            if (roomTypeName != "Apartment" && roomTypeName != "DoubleBed" && roomTypeName != "Studio")
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RoomTypeIncorrect));
            }
            if (hotel.Rooms.Select(roomTypeName) == null) // not sure
            {
                return string.Format(OutputMessages.RoomTypeNotCreated);
            }
            if (hotel.Rooms.Select(roomTypeName).PricePerNight != 0) //check
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PriceAlreadySet));
            }
            hotel.Rooms.Select(roomTypeName).SetPrice(price);
            return string.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);

        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            IHotel hotel = this.hotels.Select(hotelName);
            if (hotel == null)
            {
                return string.Format(OutputMessages.HotelNameInvalid, hotelName);
            }
            if (hotel.Rooms.Select(roomTypeName) != null) // not sure
            {
                return string.Format(OutputMessages.RoomTypeAlreadyCreated);
            }
            if (roomTypeName != "Apartment" && roomTypeName != "DoubleBed" && roomTypeName != "Studio")
            {
                throw new ArgumentException(string.Format(ExceptionMessages.RoomTypeIncorrect));
            }
            IRoom room;
            if (roomTypeName == "Apartment")
            {
                room = new Apartment();
            }
            else if (roomTypeName == "DoubleBed")
            {
                room = new DoubleBed();
            }
            else if (roomTypeName == "Studio")
            {
                room = new Studio();
            }
            else
            {
                room = null;
            }
            hotel.Rooms.AddNew(room); // tuk trq ima problem zaradi private seta ppc
            return string.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
        }
    }
}
