using Raiding.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raiding.Models
{
    public abstract class BaseHero : IBaseHero
    {
        public string Name { get; protected set; }

        public int Power { get; protected set; }

        public virtual string CastAbility()
        {
            return null;
        }
    }
}
