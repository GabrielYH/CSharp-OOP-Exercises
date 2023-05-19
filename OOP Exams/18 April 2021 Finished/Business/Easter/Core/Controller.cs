using Easter.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Easter.Utilities.Messages;
using Easter.Repositories.Contracts;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Repositories;
using Easter.Models.Bunnies;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Eggs;
using Easter.Models.Workshops.Contracts;
using Easter.Models.Workshops;

namespace Easter.Core
{
    public class Controller : IController
    {
        private IRepository<IBunny> BunnyRepository;
        private IRepository<IEgg> EggRepository;
        public Controller()
        {
            this.BunnyRepository = new BunnyRepository();
            this.EggRepository = new EggRepository();
        }
        public string AddBunny(string bunnyType, string bunnyName)
        {
            if (bunnyType != "HappyBunny" && bunnyType != "SleepyBunny")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidBunnyType));
            }
            IBunny bunny;
            if (bunnyType == "HappyBunny")
            {
                bunny = new HappyBunny(bunnyName);
            }
            else if (bunnyType == "SleepyBunny")
            {
                bunny = new SleepyBunny(bunnyName);
            }
            else
            {
                bunny = null;
            }
            this.BunnyRepository.Add(bunny);
            return string.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            IBunny bunny = this.BunnyRepository.FindByName(bunnyName);
            IDye dye = new Dye(power);
            if (bunny == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InexistentBunny));
            }
            bunny.AddDye(dye);
            return string.Format(OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            IEgg egg = new Egg(eggName, energyRequired);
            this.EggRepository.Add(egg);
            return string.Format(OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            IEgg egg = this.EggRepository.FindByName(eggName);
            if (!this.BunnyRepository.Models.Any(b => b.Energy >= 50))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.BunniesNotReady));
            }
            List<IBunny> bunnies = BunnyRepository.Models.OrderByDescending(b => b.Energy).ToList();
            IWorkshop workshop = new Workshop();
            foreach (var bunny in bunnies.Where(b => b.Energy >= 50))
            {
                workshop.Color(egg, bunny);
                if (bunny.Energy == 0)
                {
                    this.BunnyRepository.Remove(bunny);
                }
            }
            if (egg.IsDone())
            {
                return string.Format(OutputMessages.EggIsDone, eggName);
            }
            return string.Format(OutputMessages.EggIsNotDone, eggName);

        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            int countColoredEggs = this.EggRepository.Models.Where(e => e.IsDone() == true).Count(); // check
            sb.AppendLine($"{countColoredEggs} eggs are done!");
            sb.AppendLine($"Bunnies info:");
            foreach (var bunny in this.BunnyRepository.Models)
            {
                int dyesnotFinished = bunny.Dyes.Where(d => d.IsFinished() == false).Count();
                sb.AppendLine($"Name: {bunny.Name}");
                sb.AppendLine($"Energy: {bunny.Energy}");
                sb.AppendLine($"Dyes: {dyesnotFinished} not finished");
            }
            return sb.ToString().Trim();
        }
    }
}
