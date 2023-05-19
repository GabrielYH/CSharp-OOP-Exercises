﻿using Gym.Models.Equipment.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Models.Equipment
{
    public abstract class Equipment : IEquipment
    {
        private double weight;
        private decimal price;
        public Equipment(double weight, decimal price)
        {
            Weight = weight;
            Price = price;
        }
        public double Weight
        {
            get => this.weight;
            private set => this.weight = value;
        }

        public decimal Price
        {
            get => this.price;
            private set => this.price = value;
        }
    }
}
