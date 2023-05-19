using Heroes.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Heroes.Utilities.Messages;

namespace Heroes.Models
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            List<IHero> knights = players.Where(h => h.GetType() == typeof(Knight)).ToList();
            List<IHero> barbarians = players.Where(h => h.GetType() == typeof(Barbarian)).ToList();
            int numberOfDeadKnights = 0;
            int numberOfDeadBarbs = 0;

            while (knights.Count != 0 && barbarians.Count != 0)
            {
                for (int i = 0; i < knights.Count; i++)
                {
                    for (int j = 0; j < barbarians.Count; j++)
                    {
                        barbarians[j].TakeDamage(knights[i].Weapon.DoDamage());
                        if (barbarians[j].Health == 0)
                        {
                            numberOfDeadBarbs++;
                            barbarians.Remove(barbarians[j]);
                            j--;
                        }
                    }
                }

                for (int i = 0; i < barbarians.Count; i++)
                {
                    for (int j = 0; j < knights.Count; j++)
                    {
                        knights[j].TakeDamage(barbarians[i].Weapon.DoDamage());
                        if (knights[j].Health == 0)
                        {
                            numberOfDeadKnights++;
                            knights.Remove(knights[j]);
                            j--;
                        }
                    }
                }
            }

            if (knights.Count == 0)
            {
                return string.Format(OutputMessages.MapFigthBarbariansWin, numberOfDeadBarbs);
            }
            else if (barbarians.Count == 0)
            {
                return string.Format(OutputMessages.MapFightKnightsWin, numberOfDeadKnights);
            }
            else
            {
                return null;
            }
        }
    }
}
