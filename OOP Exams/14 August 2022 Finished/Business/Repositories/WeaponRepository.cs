using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;

namespace PlanetWars.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private HashSet<IWeapon> models;
        public WeaponRepository()
        {
            models = new HashSet<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models { get { return new ReadOnlyCollection<IWeapon>(models.ToList()); } } //

        public void AddItem(IWeapon model)
        {
            this.models.Add(model);
        }

        public IWeapon FindByName(string name)
        {
            return this.models.FirstOrDefault(w => w.GetType().Name == name); 
        }

        public bool RemoveItem(string name)
        {
            IWeapon weaponToRemove = this.Models.FirstOrDefault(w => w.GetType().Name == name);
            return this.models.Remove(weaponToRemove); // check
        }
    }
}
