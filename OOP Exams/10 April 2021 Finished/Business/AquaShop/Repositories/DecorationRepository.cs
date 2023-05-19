using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AquaShop.Repositories.Contracts;
using AquaShop.Models.Decorations.Contracts;

namespace AquaShop.Repositories
{
    public class DecorationRepository : IRepository<IDecoration>
    {
        private List<IDecoration> models;
        public DecorationRepository()
        {
            models = new List<IDecoration>();
        }
        public IReadOnlyCollection<IDecoration> Models => this.models.AsReadOnly();

        public void Add(IDecoration model)
        {
            this.models.Add(model);
        }

        public IDecoration FindByType(string type)
        {
            return this.models.FirstOrDefault(d => d.GetType().Name == type); // tuk dali typeof
        }

        public bool Remove(IDecoration model)
        {
            return this.models.Remove(model);
        }
    }
}
