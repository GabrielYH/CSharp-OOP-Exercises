using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChristmasPastryShop.Repositories
{
    public class CocktailRepository : IRepository<ICocktail>
    {
        private List<ICocktail> models;
        public CocktailRepository()
        {
            this.models = new List<ICocktail>();
        }
        public IReadOnlyCollection<ICocktail> Models { get { return this.models.AsReadOnly(); } }

        public void AddModel(ICocktail model)
        {
            this.models.Add(model);
        }
    }
}
