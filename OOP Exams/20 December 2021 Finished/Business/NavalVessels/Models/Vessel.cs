using NavalVessels.Models.Contracts;
using NavalVessels.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Models
{
    public abstract class Vessel : IVessel
    {
        private string name;
        private double mainWeaponCaliber;
        private double speed;
        private double armorThickness;
        private ICaptain captain;
        private List<string> targets;
        public Vessel(string name, double mainWeaponCaliber, double speed, double armorThickness)
        {
            Name = name;
            MainWeaponCaliber = mainWeaponCaliber;
            Speed = speed;
            ArmorThickness = armorThickness;
            targets = new List<string>();
        }
        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(ExceptionMessages.InvalidVesselName);
                }
                this.name = value;
            }
        }

        public ICaptain Captain
        {
            get => this.captain;
            set // reve che ne trq e private
            {
                if (value == null)
                {
                    throw new NullReferenceException(string.Format(ExceptionMessages.InvalidCaptainToVessel));
                }
                this.captain = value;
            }
        }
        public double ArmorThickness
        {
            get => this.armorThickness;
            set => this.armorThickness = value; // reve che ne trq e private
        }

        public double MainWeaponCaliber
        {
            get => this.mainWeaponCaliber;
            protected set => this.mainWeaponCaliber = value;
        }

        public double Speed
        {
            get => this.speed;
            protected set => this.speed = value;
        }

        public ICollection<string> Targets
        {
            get => this.targets;
            private set => this.targets = (List<string>)value; // checkkkkkkk
        }

        public void Attack(IVessel target)
        {
            if (target == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.InvalidTarget));
            }
            if (target.ArmorThickness - this.MainWeaponCaliber < 0)
            {
                target.ArmorThickness = 0;
            }
            else
            {
                target.ArmorThickness -= this.MainWeaponCaliber;
            }
            this.Targets.Add(target.Name);
        }

        public virtual void RepairVessel() // big check i dali da e virtual
        {
            double initialArmor;
            if (this.GetType().Name == "Battleship")
            {
                initialArmor = 300;
                this.ArmorThickness = initialArmor;
            }
            else if (this.GetType().Name == "Submarine")
            {
                initialArmor = 200;
                this.ArmorThickness = initialArmor;
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
            return sb.ToString().Trim();
        }
    }
}
