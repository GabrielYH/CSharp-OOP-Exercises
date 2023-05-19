using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Formula1.Repositories
{
    public class FormulaOneCarRepository : IRepository<IFormulaOneCar>
    {
        private List<IFormulaOneCar> models;
        public IReadOnlyCollection<IFormulaOneCar> Models { get { return this.models.AsReadOnly(); } }
        public FormulaOneCarRepository()
        {
            models = new List<IFormulaOneCar>();
        }

        public void Add(IFormulaOneCar model)
        {
            this.models.Add(model);
        }

        public IFormulaOneCar FindByName(string name)
        {
            return models.FirstOrDefault(c=> c.Model== name);
        }

        public bool Remove(IFormulaOneCar model)
        {
            return this.models.Remove(model);
        }
    }
}
