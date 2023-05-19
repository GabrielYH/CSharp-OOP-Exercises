using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Cocktails;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private IRepository<IBooth> booths;
        public Controller()
        {
            booths = new BoothRepository();
        }
        public string AddBooth(int capacity)
        {
            int boothId = this.booths.Models.Count + 1;
            IBooth booth = new Booth(boothId, capacity);
            this.booths.AddModel(booth);
            return string.Format(OutputMessages.NewBoothAdded, boothId, capacity);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            ICocktail coctail;
            if (cocktailTypeName == "MulledWine")
            {
                coctail = new MulledWine(cocktailName, size);
            }
            else if (cocktailTypeName == "Hibernation")
            {
                coctail = new Hibernation(cocktailName, size);
            }
            else
            {
                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }
            if (size != "Small" && size != "Middle" && size != "Large")
            {
                return string.Format(OutputMessages.InvalidCocktailSize, size);
            }
            if (booth.CocktailMenu.Models.Any(c => c.Name == cocktailName && c.Size == size))
            {
                return string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
            }
            booth.CocktailMenu.AddModel(coctail);
            return string.Format(OutputMessages.NewCocktailAdded, size, cocktailName, cocktailTypeName);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            IDelicacy delicacy;
            if (delicacyTypeName == "Gingerbread")
            {
                delicacy = new Gingerbread(delicacyName);
            }
            else if (delicacyTypeName == "Stolen")
            {
                delicacy = new Stolen(delicacyName);
            }
            else
            {
                return string.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }
            if (booth.DelicacyMenu.Models.Any(d => d.Name == delicacyName))
            {
                return string.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            }
            booth.DelicacyMenu.AddModel(delicacy);
            return string.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName, delicacyName);


        }

        public string BoothReport(int boothId)
        {
            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            return booth.ToString();
        }

        public string LeaveBooth(int boothId)
        {
            IBooth booth = this.booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            double currentBill = booth.CurrentBill;
            booth.Charge();
            booth.ChangeStatus();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format(OutputMessages.GetBill, currentBill.ToString("F2")));
            sb.AppendLine(string.Format(OutputMessages.BoothIsAvailable, boothId));
            return sb.ToString().Trim();
        }

        public string ReserveBooth(int countOfPeople)
        {
            List<IBooth> orderedBooths = this.booths.Models.Where(b => b.IsReserved == false && b.Capacity >= countOfPeople).OrderBy(b => b.Capacity).ThenByDescending(b => b.BoothId).ToList();
            IBooth booth;
            if (orderedBooths.Count == 0)
            {
                return string.Format(OutputMessages.NoAvailableBooth, countOfPeople);
            }
            booth = orderedBooths[0];
            booth.ChangeStatus();
            return string.Format(OutputMessages.BoothReservedSuccessfully, booth.BoothId, countOfPeople);
        }

        public string TryOrder(int boothId, string order)
        {
            string[] sequence = order.Split("/", StringSplitOptions.RemoveEmptyEntries);
            string itemTypeName = sequence[0];
            string itemName = sequence[1];
            int orderedPiecesCount = int.Parse(sequence[2]);
            string size = string.Empty;
            if (itemTypeName == "MulledWine" || itemTypeName == "Hibernation")
            {
                size = sequence[3];
            }

            List<IBooth> orderedBooths = this.booths.Models.OrderBy(b => b.Capacity).ToList();
            IBooth booth = orderedBooths.FirstOrDefault(b => b.BoothId == boothId);
            ICocktail coctail;
            IDelicacy delicacy;
            
            if (itemTypeName == "MulledWine" || itemTypeName == "Hibernation")
            {
                coctail = booth.CocktailMenu.Models.FirstOrDefault(c => c.Name == itemName); // size imashe
                if (coctail == null)
                {
                    return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
                }
                else
                {
                    if (booth.CocktailMenu.Models.Any(c => c.GetType().Name == itemTypeName && c.Name == itemName && c.Size == size))// check
                    {
                        booth.UpdateCurrentBill(coctail.Price * orderedPiecesCount);
                        return string.Format(OutputMessages.SuccessfullyOrdered, boothId, orderedPiecesCount, itemName);
                    }
                    else
                    {
                        return string.Format(OutputMessages.CocktailStillNotAdded, size, itemName);
                    }
                    
                }
            }
            else if (itemTypeName == "Stolen" || itemTypeName == "Gingerbread")
            {
                delicacy = booth.DelicacyMenu.Models.FirstOrDefault(d => d.GetType().Name == itemTypeName && d.Name == itemName);
                if (delicacy == null)
                {
                    return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
                }
                else
                {
                    booth.UpdateCurrentBill(delicacy.Price * orderedPiecesCount);
                    return string.Format(OutputMessages.SuccessfullyOrdered, boothId, orderedPiecesCount, itemName);
                }
            }
            else
            {
                return string.Format(OutputMessages.NotRecognizedType, itemTypeName);
            }


        }
    }
}
