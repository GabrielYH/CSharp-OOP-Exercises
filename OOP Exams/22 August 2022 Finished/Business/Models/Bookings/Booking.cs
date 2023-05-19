using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models.Bookings
{
    public class Booking : IBooking
    {
        private IRoom room;
        private int residenceDuration;
        private int adultsCount;
        private int childenCount;
        private int bookingNumber;
        public Booking(IRoom room, int residenceDuration, int adultsCount, int childrenCount, int bookingNumber)
        {
            Room = room;
            ResidenceDuration = residenceDuration;
            AdultsCount = adultsCount;
            ChildrenCount = childrenCount;
            BookingNumber = bookingNumber;
        }
        public IRoom Room
        {
            get => this.room;
            private set => this.room = value;
        }

        public int ResidenceDuration
        {
            get => this.residenceDuration;
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.DurationZeroOrLess));
                }
                this.residenceDuration = value;
            }
        }

        public int AdultsCount
        {
            get => this.adultsCount;
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.AdultsZeroOrLess));
                }
                this.adultsCount = value;
            }
        }

        public int ChildrenCount
        {
            get => this.childenCount;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.ChildrenNegative));
                }
                this.childenCount = value;
            }
        }

        public int BookingNumber
        {
            get => this.bookingNumber;
            private set => this.bookingNumber = value;
        }

        public string BookingSummary()
        {
            StringBuilder sb = new();
            sb.AppendLine($"Booking number: {BookingNumber}");
            sb.AppendLine($"Room type: {Room.GetType().Name}");
            sb.AppendLine($"Adults: {AdultsCount} Children: {ChildrenCount}");
            sb.AppendLine($"Total amount paid: {TotalPaid():F2} $");
            return sb.ToString().Trim();
        }

        private double TotalPaid() => Math.Round(ResidenceDuration * Room.PricePerNight, 2); // dali da e double


    }
}
