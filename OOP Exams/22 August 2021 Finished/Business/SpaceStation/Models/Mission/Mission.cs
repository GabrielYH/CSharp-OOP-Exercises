using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            List<string> copy = (List<string>)planet.Items;
            foreach (var astro in astronauts.Where(a => a.Oxygen != 0))
            {
                while (astro.Oxygen > 0)
                {
                    if (planet.Items.Count == 0)
                    {
                        return;
                    }
                    for (int i = 0; i < copy.Count; i++)
                    {
                        string item = copy[i];
                        if (astro.Oxygen == 0)
                        {
                            break;
                        }
                        astro.Bag.Items.Add(item); // tuk s take a breath dali e predi ili sled 
                        planet.Items.Remove(item); // check
                        i--;
                        astro.Breath();

                    }
                }

            }
        }
    }
}
