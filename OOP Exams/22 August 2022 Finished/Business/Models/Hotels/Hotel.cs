using BookingApp.Models.Bookings;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models.Hotels
{
    public class Hotel : IHotel
    {
        private string fullName;
        private int category;
        private IRepository<IRoom> rooms;
        private IRepository<IBooking> bookings;
        public Hotel(string fullName, int category)
        {
            FullName = fullName;
            Category = category;
            Rooms = new RoomRepository();
            Bookings = new BookingRepository();
        }
        public string FullName
        {
            get => this.fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.HotelNameNullOrEmpty));
                }
                this.fullName = value;
            }
        }

        public int Category
        {
            get => this.category;
            private set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidCategory));
                }
                this.category = value;
            }
        }


        public double Turnover => Math.Round(Bookings.All().Sum(x => x.ResidenceDuration * x.Room.PricePerNight), 2);


        public IRepository<IRoom> Rooms
        {
            get => this.rooms;
            set => this.rooms =value;
        }
        

        public IRepository<IBooking> Bookings
        {
            get => this.bookings;
            set => this.bookings = value;
        }
        

    }
}
