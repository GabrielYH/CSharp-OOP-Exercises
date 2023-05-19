using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections.ObjectModel;

namespace PlanetWars.Repositories
{
    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private HashSet<IMilitaryUnit> models;
        public UnitRepository()
        {
            models = new HashSet<IMilitaryUnit>();
        }
        public IReadOnlyCollection<IMilitaryUnit> Models { get { return new ReadOnlyCollection<IMilitaryUnit>(models.ToList()); } }

        public void AddItem(IMilitaryUnit model)
        {
            this.models.Add(model);
        }

        public IMilitaryUnit FindByName(string name)
        {
            return this.models.FirstOrDefault(u => u.GetType().Name == name);
        }

        public bool RemoveItem(string name)
        {
            IMilitaryUnit unitToRemove = this.models.FirstOrDefault(u => u.GetType().Name == name);
            return this.models.Remove(unitToRemove); // check
        }
    }
}
