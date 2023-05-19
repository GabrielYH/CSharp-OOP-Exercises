using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Repositories;
using AquaShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AquaShop.Utilities.Messages;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Decorations;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Models.Fish;

namespace AquaShop.Core
{
    public class Controller : IController
    {
        private IRepository<IDecoration> DecorationRepository;
        private List<IAquarium> aquariums;
        public Controller()
        {
            this.DecorationRepository = new DecorationRepository();
            aquariums = new List<IAquarium>();
        }

        public string AddAquarium(string aquariumType, string aquariumName)
        {
            if (aquariumType != "FreshwaterAquarium" && aquariumType != "SaltwaterAquarium")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidAquariumType));
            }
            IAquarium aquarium;
            if (aquariumType == "FreshwaterAquarium")
            {
                aquarium = new FreshwaterAquarium(aquariumName);
            }
            else if (aquariumType == "SaltwaterAquarium")
            {
                aquarium = new SaltwaterAquarium(aquariumName);
            }
            else
            {
                aquarium = null;//
            }
            this.aquariums.Add(aquarium);
            return string.Format(OutputMessages.SuccessfullyAdded, aquariumType);
        }

        public string AddDecoration(string decorationType)
        {
            if (decorationType != "Ornament" && decorationType != "Plant")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidDecorationType));
            }
            IDecoration decoration;
            if (decorationType == "Ornament")
            {
                decoration = new Ornament();
            }
            else if (decorationType == "Plant")
            {
                decoration = new Plant();
            }
            else
            {
                decoration = null;//
            }
            this.DecorationRepository.Add(decoration);
            return string.Format(OutputMessages.SuccessfullyAdded, decorationType);
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            IAquarium aquarium = aquariums.FirstOrDefault(a => a.Name == aquariumName);
            if (fishType != "FreshwaterFish" && fishType != "SaltwaterFish")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidFishType));
            }
            IFish fish;
            if (fishType == "FreshwaterFish")
            {
                fish = new FreshwaterFish(fishName, fishSpecies, price);
            }
            else if (fishType == "SaltwaterFish")
            {
                fish = new SaltwaterFish(fishName, fishSpecies, price);
            }
            else
            {
                fish = null;//
            }

            if (fishType == "FreshwaterFish" && aquarium.GetType().Name == "FreshwaterAquarium")
            {
                aquarium.AddFish(fish);
                return string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
            }
            else if (fishType == "SaltwaterFish" && aquarium.GetType().Name == "SaltwaterAquarium")
            {
                aquarium.AddFish(fish);
                return string.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);
            }
            else
            {
                return string.Format(OutputMessages.UnsuitableWater); //
            }
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aquarium = this.aquariums.FirstOrDefault(a => a.Name == aquariumName);
            decimal sum = 0;
            foreach (var deco in aquarium.Decorations)
            {
                sum += deco.Price;
            }
            foreach (var fish in aquarium.Fish)
            {
                sum += fish.Price;
            }
            return string.Format(OutputMessages.AquariumValue, aquariumName, sum);
        }

        public string FeedFish(string aquariumName)
        {
            IAquarium aquarium = this.aquariums.FirstOrDefault(a => a.Name == aquariumName);
            aquarium.Feed();
            return string.Format(OutputMessages.FishFed, aquarium.Fish.Count);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IAquarium aquarium = this.aquariums.FirstOrDefault(a => a.Name == aquariumName);
            IDecoration decoration = this.DecorationRepository.FindByType(decorationType);
            if (decoration == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentDecoration, decorationType));
            }
            aquarium.AddDecoration(decoration);
            this.DecorationRepository.Remove(decoration);
            return string.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var aq in aquariums)
            {
                sb.AppendLine($"{aq.GetInfo()}");
            }
            return sb.ToString().Trim();
        }
    }
}
