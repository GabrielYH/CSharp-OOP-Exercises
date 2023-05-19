using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
{
    public class Captain : ICaptain
    {
        private string fullName;
        private int combatExperience;
        private List<IVessel> vessels;
        public Captain(string fullName)
        {
            FullName = fullName;
            vessels = new List<IVessel>();
        }
        public string FullName
        {
            get => this.fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(string.Format(ExceptionMessages.InvalidCaptainName));
                }
                this.fullName = value;
            }
        }

        public int CombatExperience
        {
            get => this.combatExperience;
            private set => this.combatExperience = value;
        }

        public ICollection<IVessel> Vessels
        {
            get => this.vessels;
            private set => this.vessels = (List<IVessel>)value; //
        }

        public void AddVessel(IVessel vessel)
        {
            if (vessel == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.InvalidVesselForCaptain));
            }
            this.Vessels.Add(vessel);
        }

        public void IncreaseCombatExperience()
        {
            this.CombatExperience += 10;
        }

        public string Report()
        {
            StringBuilder sb = new();
            sb.AppendLine($"{FullName} has {CombatExperience} combat experience and commands {Vessels.Count} vessels.");
            if (Vessels.Count != 0)
            {
                foreach (var ves in Vessels)
                {
                    sb.AppendLine(ves.ToString()); // bi trqlo da e ok
                }
            }
            return sb.ToString().Trim();
        }
    }
}
