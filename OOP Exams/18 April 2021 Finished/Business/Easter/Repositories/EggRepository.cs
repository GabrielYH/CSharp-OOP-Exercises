using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Easter.Repositories.Contracts;
using Easter.Models.Eggs.Contracts;

namespace Easter.Repositories
{
    public class EggRepository : IRepository<IEgg>
    {
        private List<IEgg> models;
        public EggRepository()
        {
            models = new List<IEgg>();
        }
        public IReadOnlyCollection<IEgg> Models => this.models.AsReadOnly();

        public void Add(IEgg model)
        {
            this.models.Add(model);
        }

        public IEgg FindByName(string name)
        {
            return this.models.FirstOrDefault(e => e.Name == name);
        }

        public bool Remove(IEgg model)
        {
            return this.models.Remove(model);
        }
    }
}
