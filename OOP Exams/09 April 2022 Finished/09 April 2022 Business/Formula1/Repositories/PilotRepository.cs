using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Repositories
{
    public class PilotRepository : IRepository<IPilot>
    {
        private List<IPilot> models;
        public IReadOnlyCollection<IPilot> Models { get { return this.models.AsReadOnly(); } }
        public PilotRepository()
        {
            models = new List<IPilot>();
        }

        public void Add(IPilot pilot)
        {
            this.models.Add(pilot);
        }

        public IPilot FindByName(string fullName)
        {
            return models.FirstOrDefault(c => c.FullName == fullName);
        }

        public bool Remove(IPilot pilot)
        {
            return this.models.Remove(pilot);
        }
    }
}
