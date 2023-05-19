using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AquaShop.Models.Aquariums
{
    public abstract class Aquarium : IAquarium
    {
        private string name;
        private int capacity;
        private int comfort;
        private List<IDecoration> decorations;
        private List<IFish> fish;
        public Aquarium(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            decorations = new List<IDecoration>();
            fish = new List<IFish>();
        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidAquariumName));
                }
                this.name = value;
            }
        }

        public int Capacity
        {
            get => this.capacity;
            private set => this.capacity = value;
        }

        public int Comfort // tuka dali ne trq da e settera
        {
            get
            {
                int sum = 0;
                foreach (var item in Decorations)
                {
                    sum += item.Comfort;
                }
                return sum;
            }
        }

        public ICollection<IDecoration> Decorations => this.decorations;


        public ICollection<IFish> Fish => this.fish;


        public void AddDecoration(IDecoration decoration)
        {
            Decorations.Add(decoration);
        }

        public void AddFish(IFish fish)
        {
            if (Fish.Count < Capacity)
            {
                Fish.Add(fish);
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.NotEnoughCapacity));
            }
        }

        public void Feed()
        {
            foreach (var currFish in Fish)
            {
                currFish.Eat();
            }
        }

        public string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{Name} ({this.GetType().Name}):");
            if (Fish.Count == 0)
            {
                sb.AppendLine($"Fish: none");
            }
            else
            {
                sb.AppendLine($"Fish: {string.Join(", ", Fish.Select(f => f.Name))}"); //
            }
            sb.AppendLine($"Decorations: {Decorations.Count}");
            sb.AppendLine($"Comfort: {Comfort}");
            return sb.ToString().Trim();
        }

        public bool RemoveFish(IFish fish)
        {
            return Fish.Remove(fish);
        }
    }
}
