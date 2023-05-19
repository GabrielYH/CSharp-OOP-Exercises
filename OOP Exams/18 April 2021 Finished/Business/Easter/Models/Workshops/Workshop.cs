using Easter.Models.Bunnies.Contracts;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Easter.Models.Workshops
{
    public class Workshop : IWorkshop
    {
        public Workshop()
        {

        }
        public void Color(IEgg egg, IBunny bunny)
        {
            if (bunny.Energy > 0 && bunny.Dyes.Any(d => d.Power > 0)) // check
            {
                while (egg.IsDone() == false && bunny.Energy > 0 && bunny.Dyes.Any(d => d.Power > 0)) //
                {
                    foreach (var dye in bunny.Dyes.Where(d => d.Power > 0))
                    {
                        while (dye.Power > 0)
                        {
                            if (egg.IsDone() || bunny.Energy == 0 || !bunny.Dyes.Any(d => d.Power > 0))
                            {
                                return;
                            }
                            bunny.Work();
                            dye.Use();
                            egg.GetColored();
                        }
                    }

                }
            }
        }
    }
}
