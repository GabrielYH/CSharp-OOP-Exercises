using NavalVessels.Core.Contracts;
using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories;
using NavalVessels.Repositories.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NavalVessels.Core
{
    public class Controller : IController
    {
        private IRepository<IVessel> VesselRepository;
        private List<ICaptain> captains; // check name
        public Controller()
        {
            this.VesselRepository = new VesselRepository();
            captains = new();
        }
        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            IVessel vessel = VesselRepository.FindByName(selectedVesselName);
            ICaptain captain = this.captains.FirstOrDefault(c => c.FullName == selectedCaptainName);
            if (captain == null)
            {
                return string.Format(OutputMessages.CaptainNotFound, selectedCaptainName);
            }
            if (vessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, selectedVesselName);
            }
            if (vessel.Captain != null)
            {
                return string.Format(OutputMessages.VesselOccupied, selectedVesselName);
            }
            captain.AddVessel(vessel);
            vessel.Captain = captain;
            return string.Format(OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName);
            
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName) // neveroqten check
        {
            IVessel attackingVessel = this.VesselRepository.FindByName(attackingVesselName);
            IVessel defendingVessel = this.VesselRepository.FindByName(defendingVesselName);
            if (attackingVessel == null && defendingVessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, attackingVesselName);
            }
            else if (attackingVessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, attackingVesselName);
            }
            else if (defendingVessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, defendingVesselName);
            }

            if (attackingVessel.ArmorThickness == 0 && defendingVessel.ArmorThickness == 0)
            {
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, attackingVesselName);
            }
            else if (attackingVessel.ArmorThickness == 0)
            {
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, attackingVesselName);
            }
            else if (defendingVessel.ArmorThickness == 0)
            {
                return string.Format(OutputMessages.AttackVesselArmorThicknessZero, defendingVesselName);
            }
            attackingVessel.Attack(defendingVessel);
            attackingVessel.Captain.IncreaseCombatExperience();
            defendingVessel.Captain.IncreaseCombatExperience();
            return string.Format(OutputMessages.SuccessfullyAttackVessel, defendingVesselName, attackingVesselName, defendingVessel.ArmorThickness);
        }

        public string CaptainReport(string captainFullName)
        {
            ICaptain captain = this.captains.FirstOrDefault(c => c.FullName == captainFullName && c.Vessels.Count != 0);// check dali assignat
            return captain.Report();
        }

        public string HireCaptain(string fullName)
        {
            if (this.captains.Any(c => c.FullName == fullName))
            {
                return string.Format(OutputMessages.CaptainIsAlreadyHired, fullName);
            }
            ICaptain captain = new Captain(fullName);
            this.captains.Add(captain);
            return string.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            if (this.VesselRepository.FindByName(name) != null)
            {
                return string.Format(OutputMessages.VesselIsAlreadyManufactured, vesselType, name);
            }
            if (vesselType != "Submarine" && vesselType != "Battleship")
            {
                return string.Format(OutputMessages.InvalidVesselType);
            }
            IVessel vessel;
            if (vesselType == "Submarine")
            {
                vessel = new Submarine(name, mainWeaponCaliber, speed);
            }
            else if (vesselType == "Battleship")
            {
                vessel = new Battleship(name, mainWeaponCaliber, speed);
            }
            else
            {
                vessel = null;
            }
            this.VesselRepository.Add(vessel);
            return string.Format(OutputMessages.SuccessfullyCreateVessel, vesselType, name, mainWeaponCaliber, speed);
        }

        public string ServiceVessel(string vesselName)
        {
            IVessel vessel = this.VesselRepository.FindByName(vesselName);
            if (vessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, vesselName);
            }
            vessel.RepairVessel(); // tuk da vidim kvo shte izvika
            return string.Format(OutputMessages.SuccessfullyRepairVessel, vesselName);
        }

        public string ToggleSpecialMode(string vesselName)
        {
            IVessel vessel = this.VesselRepository.FindByName(vesselName);
            if (vessel == null)
            {
                return string.Format(OutputMessages.VesselNotFound, vesselName);
            }
            if (vessel.GetType().Name == "Submarine")
            {
                Submarine submarineVessel = vessel as Submarine;
                submarineVessel.ToggleSubmergeMode();
                return string.Format(OutputMessages.ToggleSubmarineSubmergeMode, vesselName);
            }
            else if (vessel.GetType().Name == "Battleship")
            {
                Battleship battleShipVessel = vessel as Battleship;
                battleShipVessel.ToggleSonarMode();
                return string.Format(OutputMessages.ToggleBattleshipSonarMode, vesselName);
            }
            else
            {
                return null; //
            }
        }

        public string VesselReport(string vesselName)
        {
            IVessel vessel = this.VesselRepository.FindByName(vesselName);
            return vessel.ToString();
        }
    }
}
