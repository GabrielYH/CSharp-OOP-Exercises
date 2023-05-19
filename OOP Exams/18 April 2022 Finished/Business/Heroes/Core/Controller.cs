using Heroes.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Heroes.Repositories.Contracts;
using Heroes.Models.Contracts;
using Heroes.Repositories;
using Heroes.Utilities.Messages;
using Heroes.Models;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private IRepository<IHero> HeroRepository;
        private IRepository<IWeapon> WeaponRepository;
        public Controller()
        {
            this.HeroRepository = new HeroRepository();
            this.WeaponRepository = new WeaponRepository();
        }
        public string AddWeaponToHero(string weaponName, string heroName)
        {
            IHero hero = this.HeroRepository.FindByName(heroName);
            if (hero == null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroDoesNotExist, heroName));
            }
            IWeapon weapon = this.WeaponRepository.FindByName(weaponName);
            if (weapon == null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponDoesNotExist, weaponName));
            }
            if (hero.Weapon != null) // big check
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroAlreadyHasWeapon, heroName));
            }
            hero.AddWeapon(weapon);
            this.WeaponRepository.Remove(weapon);
            return string.Format(OutputMessages.WeaponAddedToHero, heroName, weapon.GetType().Name.ToLower());
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            IHero hero = this.HeroRepository.FindByName(name);
            if (hero != null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroAlreadyExist, name)); //?
            }
            if (type != "Knight" && type != "Barbarian")
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroTypeIsInvalid));
            }
            IHero heroToAdd;
            if (type == "Knight")
            {
                heroToAdd = new Knight(name, health, armour);
                this.HeroRepository.Add(heroToAdd);
                return string.Format(OutputMessages.SuccessfullyAddedKnight, name);
            }
            else if (type == "Barbarian")
            {
                heroToAdd = new Barbarian(name, health, armour);
                this.HeroRepository.Add(heroToAdd);
                return string.Format(OutputMessages.SuccessfullyAddedBarbarian, name);
            }
            else
            {
                return null; //
            }
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            IWeapon weapon = this.WeaponRepository.FindByName(name);
            if (weapon != null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponAlreadyExists, name));
            }
            IWeapon weaponToAdd;
            if (type != "Mace" && type != "Claymore")
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponTypeIsInvalid));
            }
            if (type == "Mace")
            {
                weaponToAdd = new Mace(name, durability);
            }
            else if (type == "Claymore")
            {
                weaponToAdd = new Claymore(name, durability);
            }
            else
            {
                weaponToAdd = null;//
            }
            this.WeaponRepository.Add(weaponToAdd);
            return string.Format(OutputMessages.WeaponAddedSuccessfully, type.ToLower(), name); // checkkkk
        }

        public string HeroReport()
        {
            StringBuilder sb = new StringBuilder();
            List<IHero> orderedHeroes = this.HeroRepository.Models.OrderBy(h => h.GetType().Name).ThenByDescending(h => h.Health).ThenBy(h => h.Name).ToList();// check
            foreach (var hero in orderedHeroes)
            {
                sb.AppendLine($"{hero.GetType().Name}: {hero.Name}");
                sb.AppendLine($"--Health: {hero.Health}");
                sb.AppendLine($"--Armour: {hero.Armour}");
                if (hero.Weapon == null)
                {
                    sb.AppendLine($"--Weapon: Unarmed");
                }
                else
                {
                    sb.AppendLine($"--Weapon: {hero.Weapon.Name}"); // ? name ili type
                }
            }
                       
            return sb.ToString().Trim();
        }

        public string StartBattle()
        {
            Map map = new Map();
            List<IHero> aliveAndArmed = this.HeroRepository.Models.Where(h => h.Health > 0 && h.Weapon != null).ToList();
            return map.Fight(aliveAndArmed); // check
        }
    }
}
