using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Models.Booths
{
    public class Booth : IBooth
    {
        private int boothId;
        private int capacity;
        private IRepository<IDelicacy> delicacyMenu;
        private IRepository<ICocktail> coctailMenu;
        private double currentBill;
        private double turnover;
        public Booth(int boothId, int capacity)
        {
            BoothId = boothId;
            Capacity = capacity;
            this.delicacyMenu = new DelicacyRepository();
            this.coctailMenu = new CocktailRepository();
            CurrentBill = 0;
            Turnover = 0;
        }
        public int BoothId
        {
            get => this.boothId;
            private set => this.boothId = value;
            
        }

        public int Capacity
        {
            get => this.capacity;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.CapacityLessThanOne));
                }
                this.capacity = value;
            }
        }

        public IRepository<IDelicacy> DelicacyMenu
        {
            get => this.delicacyMenu;
            private set => this.delicacyMenu = value;
        }

        public IRepository<ICocktail> CocktailMenu
        {
            get => this.coctailMenu;
           private set => this.coctailMenu = value;
        }

        public double CurrentBill
        {
            get => this.currentBill;
            private set => this.currentBill = value;
        }

        public double Turnover
        {
            get => this.turnover;
            private set => this.turnover = value;
        }

        public bool IsReserved { get; private set; }

        public void ChangeStatus()
        {
            if (IsReserved == true)
            {
                IsReserved = false;
            }
            else if (IsReserved == false)
            {
                IsReserved = true;
            }
        }

        public void Charge()
        {
            Turnover += CurrentBill;
            CurrentBill = 0;
        }

        public void UpdateCurrentBill(double amount)
        {
            CurrentBill += amount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Booth: {BoothId}");
            sb.AppendLine($"Capacity: {Capacity}");
            sb.AppendLine($"Turnover: {Turnover:f2} lv");
            sb.AppendLine($"-Cocktail menu:");
            foreach (var coctail in CocktailMenu.Models)
            {
                sb.AppendLine($"--{coctail}");
            }
            sb.AppendLine($"-Delicacy menu:");
            foreach (var delicacy in DelicacyMenu.Models)
            {
                sb.AppendLine($"--{delicacy}");
            }

            return sb.ToString().Trim();
        }
    }
}
