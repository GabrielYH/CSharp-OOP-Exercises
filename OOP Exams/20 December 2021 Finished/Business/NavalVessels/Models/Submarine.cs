using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
{
    public class Submarine : Vessel, ISubmarine
    {
        private const double initialArmorThickness = 200;
        public Submarine(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, initialArmorThickness)
        {
        }

        public bool SubmergeMode { get; private set; }

        public override void RepairVessel()
        {
            double initialArmor = 200;
            this.ArmorThickness = initialArmor; //
        }
        public void ToggleSubmergeMode()
        {
            if (SubmergeMode == true)
            {
                SubmergeMode = false;
                this.MainWeaponCaliber -= 40;
                this.Speed += 4;
            }
            else if (SubmergeMode == false)
            {
                SubmergeMode = true;
                this.MainWeaponCaliber += 40;
                this.Speed -= 4;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine($"- {Name}");
            sb.AppendLine($" *Type: {this.GetType().Name}");
            sb.AppendLine($" *Armor thickness: {ArmorThickness}");
            sb.AppendLine($" *Main weapon caliber: {MainWeaponCaliber}");
            sb.AppendLine($" *Speed: {Speed} knots");
            if (Targets.Count == 0)//
            {
                sb.AppendLine($" *Targets: None");
            }
            else
            {
                sb.AppendLine($" *Targets: {string.Join(", ", Targets)}");
            }
            if (SubmergeMode == true)
            {
                sb.AppendLine($" *Submerge mode: ON");
            }
            else if (SubmergeMode == false)
            {
                sb.AppendLine($" *Submerge mode: OFF");
            }
            return sb.ToString().Trim();
        }
    }
}
