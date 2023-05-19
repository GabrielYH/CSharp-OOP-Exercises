using PlanetWars.Core.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PlanetWars.Utilities.Messages;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.Planets;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private IRepository<IPlanet> planets;
        public Controller()
        {
            planets = new PlanetRepository();
        }
        public string AddUnit(string unitTypeName, string planetName)
        {
            IPlanet planet = this.planets.FindByName(planetName);
            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }
            if (unitTypeName != "SpaceForces" && unitTypeName != "StormTroopers" && unitTypeName != "AnonymousImpactUnit")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            }
            
            if (planet.Army.Any(w => w.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));
            }
            IMilitaryUnit unit;
            if (unitTypeName == "SpaceForces")
            {
                unit = new SpaceForces();
            }
            else if (unitTypeName == "StormTroopers")
            {
                unit = new StormTroopers();
            }
            else if (unitTypeName == "AnonymousImpactUnit")
            {
                unit = new AnonymousImpactUnit();
            }
            else
            {
                unit = null;
            }
            planet.Spend(unit.Cost);
            planet.AddUnit(unit);
            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            IPlanet planet = this.planets.FindByName(planetName);
            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }
            if (weaponTypeName != "BioChemicalWeapon" && weaponTypeName != "NuclearWeapon" && weaponTypeName != "SpaceMissiles")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }
            if (planet.Weapons.Any(w => w.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
            }
            IWeapon weapon;
            if (weaponTypeName == "BioChemicalWeapon")
            {
                weapon = new BioChemicalWeapon(destructionLevel);
            }
            else if (weaponTypeName == "NuclearWeapon")
            {
                weapon = new NuclearWeapon(destructionLevel);
            }
            else if (weaponTypeName == "SpaceMissiles")
            {
                weapon = new SpaceMissiles(destructionLevel);
            }
            else
            {
                weapon = null;
            }
            planet.Spend(weapon.Price);
            planet.AddWeapon(weapon);
            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string CreatePlanet(string name, double budget)
        {
            IPlanet planet = this.planets.FindByName(name);
            if (planet != null)
            {
                return string.Format(OutputMessages.ExistingPlanet, name);
            }
            IPlanet planetToCreate = new Planet(name, budget);
            this.planets.AddItem(planetToCreate);
            return string.Format(OutputMessages.NewPlanet, name);
        }

        public string ForcesReport()
        {
            List<IPlanet> orderedPlanets = this.planets.Models.OrderByDescending(p => p.MilitaryPower).ThenBy(p => p.Name).ToList();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"***UNIVERSE PLANET MILITARY REPORT***");
            foreach (var planet in orderedPlanets)
            {
                sb.AppendLine($"Planet: {planet.Name}");
                sb.AppendLine($"--Budget: {planet.Budget} billion QUID");
                if (planet.Army.Count == 0)
                {
                    sb.AppendLine($"--Forces: No units");
                }
                else
                {
                    sb.AppendLine($"--Forces: {string.Join(", ", planet.Army.Select(u => u.GetType().Name))}");
                }
                if (planet.Weapons.Count == 0)
                {
                    sb.AppendLine($"--Combat equipment: No weapons");
                }
                else
                {
                    sb.AppendLine($"--Combat equipment: {string.Join(", ", planet.Weapons.Select(w => w.GetType().Name))}");
                }
                sb.AppendLine($"--Military Power: {planet.MilitaryPower}");
            }
            return sb.ToString().Trim();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet firstPlanet = this.planets.FindByName(planetOne);
            IPlanet secondPlanet = this.planets.FindByName(planetTwo);
            if (firstPlanet.MilitaryPower == secondPlanet.MilitaryPower)
            {
                if (firstPlanet.Weapons.Any(w => w.GetType().Name == "NuclearWeapon") && secondPlanet.Weapons.Any(w => w.GetType().Name == "NuclearWeapon"))
                {
                    firstPlanet.Spend(firstPlanet.Budget / 2);
                    secondPlanet.Spend(secondPlanet.Budget / 2);
                    return string.Format(OutputMessages.NoWinner);
                }
                else if (!firstPlanet.Weapons.Any(w => w.GetType().Name == "NuclearWeapon") && !secondPlanet.Weapons.Any(w => w.GetType().Name == "NuclearWeapon"))
                {
                    firstPlanet.Spend(firstPlanet.Budget / 2);
                    secondPlanet.Spend(secondPlanet.Budget / 2);
                    return string.Format(OutputMessages.NoWinner);
                }
                else
                {
                    if (firstPlanet.Weapons.Any(w => w.GetType().Name == "NuclearWeapon") && !secondPlanet.Weapons.Any(w => w.GetType().Name == "NuclearWeapon"))
                    {
                        firstPlanet.Spend(firstPlanet.Budget / 2);
                        firstPlanet.Profit(secondPlanet.Budget / 2);
                        double forcesSum = 0;
                        foreach (var force in secondPlanet.Army)
                        {
                            forcesSum += force.Cost;
                        }
                        firstPlanet.Profit(forcesSum);
                        double weaponCostsSum = 0;
                        foreach (var wep in secondPlanet.Weapons)
                        {
                            weaponCostsSum += wep.Price;
                        }
                        firstPlanet.Profit(weaponCostsSum);
                        planets.RemoveItem(planetTwo);
                        return string.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);
                    }
                    else if (secondPlanet.Weapons.Any(w => w.GetType().Name == "NuclearWeapon") && !firstPlanet.Weapons.Any(w => w.GetType().Name == "NuclearWeapon"))
                    {
                        secondPlanet.Spend(secondPlanet.Budget / 2);
                        secondPlanet.Profit(firstPlanet.Budget / 2);
                        double forcesSum = 0;
                        foreach (var force in firstPlanet.Army)
                        {
                            forcesSum += force.Cost;
                        }
                        secondPlanet.Profit(forcesSum);
                        double weaponCostsSum = 0;
                        foreach (var wep in firstPlanet.Weapons)
                        {
                            weaponCostsSum += wep.Price;
                        }
                        secondPlanet.Profit(weaponCostsSum);
                        planets.RemoveItem(planetOne);
                        return string.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            else if (firstPlanet.MilitaryPower > secondPlanet.MilitaryPower)
            {
                firstPlanet.Spend(firstPlanet.Budget / 2);
                firstPlanet.Profit(secondPlanet.Budget / 2);
                double forcesSum = 0;
                foreach (var force in secondPlanet.Army)
                {
                    forcesSum += force.Cost;
                }
                firstPlanet.Profit(forcesSum);
                double weaponCostsSum = 0;
                foreach (var wep in secondPlanet.Weapons)
                {
                    weaponCostsSum += wep.Price;
                }
                firstPlanet.Profit(weaponCostsSum);
                planets.RemoveItem(planetTwo);
                return string.Format(OutputMessages.WinnigTheWar, planetOne, planetTwo);

            }
            else if (firstPlanet.MilitaryPower < secondPlanet.MilitaryPower)
            {
                secondPlanet.Spend(secondPlanet.Budget / 2);
                secondPlanet.Profit(firstPlanet.Budget / 2);
                double forcesSum = 0;
                foreach (var force in firstPlanet.Army)
                {
                    forcesSum += force.Cost;
                }
                secondPlanet.Profit(forcesSum);
                double weaponCostsSum = 0;
                foreach (var wep in firstPlanet.Weapons)
                {
                    weaponCostsSum += wep.Price;
                }
                secondPlanet.Profit(weaponCostsSum);
                planets.RemoveItem(planetOne);
                return string.Format(OutputMessages.WinnigTheWar, planetTwo, planetOne);
            }
            else
            {
                return null;
            }
        }

        public string SpecializeForces(string planetName)
        {
            IPlanet planet = this.planets.FindByName(planetName);
            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }
            if (planet.Army.Count == 0)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.NoUnitsFound));
            }
            planet.TrainArmy();
            planet.Spend(1.25);
            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }
    }
}
