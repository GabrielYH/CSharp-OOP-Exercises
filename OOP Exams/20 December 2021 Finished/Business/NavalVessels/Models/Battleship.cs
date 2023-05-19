using NavalVessels.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
{
    public class Battleship : Vessel, IBattleship
    {
        private const double initialArmorThickness = 300;
        public Battleship(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, initialArmorThickness)
        {
        }

        public bool SonarMode { get; private set; } //

        public override void RepairVessel()
        {
            double initialArmor = 300;
            this.ArmorThickness = initialArmor; //
        }

        public void ToggleSonarMode()
        {
            if (SonarMode == true)
            {
                SonarMode = false;
                this.MainWeaponCaliber -= 40;
                this.Speed += 5;
            }
            else if (SonarMode == false)
            {
                SonarMode = true;
                this.MainWeaponCaliber += 40;
                this.Speed -= 5;
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
            if (SonarMode == true)
            {
                sb.AppendLine($" *Sonar mode: ON");
            }
            else if (SonarMode == false)
            {
                sb.AppendLine($" *Sonar mode: OFF");
            }
            return sb.ToString().Trim();
        }
    }
}
