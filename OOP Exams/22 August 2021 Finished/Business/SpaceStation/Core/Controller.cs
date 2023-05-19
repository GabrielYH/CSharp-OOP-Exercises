using SpaceStation.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SpaceStation.Repositories.Contracts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using SpaceStation.Utilities.Messages;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Mission;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        private IRepository<IAstronaut> astronauts;
        private IRepository<IPlanet> planets;
        private int exploredPlanetsCounter = 0;
        public Controller()
        {
            this.astronauts = new AstronautRepository();
            this.planets = new PlanetRepository();
        }
        public string AddAstronaut(string type, string astronautName)
        {
            if (type != "Biologist" && type != "Geodesist" && type != "Meteorologist")
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidAstronautType));
            }
            IAstronaut astronaut;
            if (type == "Biologist")
            {
                astronaut = new Biologist(astronautName);
            }
            else if (type == "Geodesist")
            {
                astronaut = new Geodesist(astronautName);
            }
            else if (type == "Meteorologist")
            {
                astronaut = new Meteorologist(astronautName);
            }
            else
            {
                astronaut = null; //
            }
            this.astronauts.Add(astronaut);
            return string.Format(OutputMessages.AstronautAdded, type, astronautName);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);
            foreach (var item in items)
            {
                planet.Items.Add(item);
            }
            this.planets.Add(planet);
            return string.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string ExplorePlanet(string planetName)
        {
            IPlanet planet = this.planets.FindByName(planetName);
            if (!this.astronauts.Models.Any(a => a.Oxygen > 60))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidAstronautCount));
            }
            IMission mission = new Mission();
            List<IAstronaut> astronauts = this.astronauts.Models.Where(a => a.Oxygen > 60).ToList();
            mission.Explore(planet, astronauts);
            int deadAstronauts = 0;
            foreach (var astro in astronauts.Where(a => a.Oxygen == 0))
            {
                deadAstronauts++;
            }
            exploredPlanetsCounter++; // ????????
            return string.Format(OutputMessages.PlanetExplored, planet.Name, deadAstronauts); // da vidq dali promenq kolekciqta

        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{exploredPlanetsCounter} planets were explored!");
            sb.AppendLine($"Astronauts info:");
            foreach (var astro in this.astronauts.Models)
            {
                sb.AppendLine($"Name: {astro.Name}");
                sb.AppendLine($"Oxygen: {astro.Oxygen}");
                if (astro.Bag.Items.Count == 0)
                {
                    sb.AppendLine($"Bag items: none");
                }
                else
                {
                    sb.AppendLine($"Bag items: {string.Join(", ", astro.Bag.Items)}");
                }
            }
            return sb.ToString().Trim();
        }

        public string RetireAstronaut(string astronautName)
        {
            IAstronaut astronaut = this.astronauts.FindByName(astronautName);
            if (astronaut == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidRetiredAstronaut, astronautName));
            }
            this.astronauts.Remove(astronaut);
            return string.Format(OutputMessages.AstronautRetired, astronautName);
        }
    }
}
