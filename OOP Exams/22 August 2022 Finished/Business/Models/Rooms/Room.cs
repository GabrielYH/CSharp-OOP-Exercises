using BookingApp.Models.Rooms.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models.Rooms
{
    public abstract class Room : IRoom
    {
        private int bedCapacity;
        private double pricePerNight;
        public Room(int bedCapacity)
        {
            BedCapacity = bedCapacity;
            PricePerNight = 0; // da proverq vse pak tva sus setvaneto
        }
        public int BedCapacity
        {
            get => this.bedCapacity;
            private set => this.bedCapacity = value;
        }

        public double PricePerNight
        {
            get => this.pricePerNight;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.PricePerNightNegative));
                }
                this.pricePerNight = value;
            }
        }

        public void SetPrice(double price)
        {
            PricePerNight = price;
        }
    }
}
