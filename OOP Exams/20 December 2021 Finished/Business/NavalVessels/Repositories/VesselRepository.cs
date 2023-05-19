using NavalVessels.Models.Contracts;
using NavalVessels.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Repositories
{
    public class VesselRepository : IRepository<IVessel>
    {
        private List<IVessel> models; // dali trq e hashset
        public VesselRepository()
        {
            models = new();
        }
        public IReadOnlyCollection<IVessel> Models => this.models.AsReadOnly();

        public void Add(IVessel model)
        {
            this.models.Add(model);
        }

        public IVessel FindByName(string name)
        {
            return this.models.FirstOrDefault(v => v.Name == name);
        }

        public bool Remove(IVessel model)
        {
            return this.models.Remove(model);
        }
    }
}
