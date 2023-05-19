using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Heroes.Repositories
{
    public class WeaponRepository : IRepository<IWeapon>
    {
        private List<IWeapon> models;
        public WeaponRepository()
        {
            models = new List<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models => this.models.AsReadOnly();

        public void Add(IWeapon model)
        {
            this.models.Add(model);
        }

        public IWeapon FindByName(string name)
        {
            return this.models.FirstOrDefault(w => w.Name == name);
        }

        public bool Remove(IWeapon model)
        {
            return this.models.Remove(model);
        }
    }
}
