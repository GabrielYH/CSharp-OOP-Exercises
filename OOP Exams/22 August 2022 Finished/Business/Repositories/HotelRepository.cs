using BookingApp.Models.Hotels.Contacts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repositories
{
    public class HotelRepository : IRepository<IHotel>
    {
        private List<IHotel> hotels;
        public HotelRepository()
        {
            hotels = new List<IHotel>();
        }
        public void AddNew(IHotel model)
        {
            this.hotels.Add(model);
        }

        public IReadOnlyCollection<IHotel> All()
        {
            return this.hotels.AsReadOnly();
        }

        public IHotel Select(string hotelName)
        {
            return this.hotels.FirstOrDefault(h => h.FullName == hotelName);
        }
    }
}
