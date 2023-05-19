using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SpaceStation.Repositories.Contracts;
using SpaceStation.Models.Planets.Contracts;

namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> models;
        public PlanetRepository()
        {
            models = new List<IPlanet>();
        }
        public IReadOnlyCollection<IPlanet> Models => this.models.AsReadOnly();

        public void Add(IPlanet model)
        {
            this.models.Add(model);
        }

        public IPlanet FindByName(string name)
        {
            return this.models.FirstOrDefault(p => p.Name == name);
        }

        public bool Remove(IPlanet model)
        {
            return this.models.Remove(model);
        }
    }
}
