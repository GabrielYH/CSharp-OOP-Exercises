using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Heroes.Repositories
{
    public class HeroRepository : IRepository<IHero>
    {
        private List<IHero> models;
        public HeroRepository()
        {
            models = new List<IHero>();
        }
        public IReadOnlyCollection<IHero> Models => this.models.AsReadOnly();

        public void Add(IHero model)
        {
            this.models.Add(model);
        }

        public IHero FindByName(string name)
        {
            return this.models.FirstOrDefault(h => h.Name == name);
        }

        public bool Remove(IHero model)
        {
            return this.models.Remove(model);
        }
    }
}
