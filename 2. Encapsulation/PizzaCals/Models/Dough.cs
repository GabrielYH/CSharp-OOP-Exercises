using PizzaCals.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PizzaCals.Models
{
    public class Dough
    {
        private const double BaseCaloriesPerGram = 2;
        private FlourType flourType;
        private double flourTypeCalories;
        private BakingTechnique bakingTechnique;
        private double bakingTechniqueCalories;
        private double weight;

        public Dough(string flourType, string technique, double weight)
        {
            if (!Enum.TryParse<FlourType>(flourType.ToLower(), out var type))
            {
                throw new ArgumentException("Invalid type of dough.");
            }
            this.flourType = type;
            SetFlourCalories(flourType.ToLower());
            if (!Enum.TryParse<BakingTechnique>(technique.ToLower(), out var bakingTechnique)) // da probvam tva da e v gornoto
            {
                throw new ArgumentException("Invalid type of dough.");
            }
            this.bakingTechnique = bakingTechnique;
            SetBakingTechniqueCalories(technique.ToLower());
            this.weight = ValidateWeigth(weight);
        }

        public double Calories { get { return CalculateCalories(); } }


        private double CalculateCalories()
        {
            return (BaseCaloriesPerGram * this.weight) * flourTypeCalories * bakingTechniqueCalories;
        }

        private double ValidateWeigth(double weight)
        {
            if (weight <= 0 || weight > 200)
            {
                throw new ArgumentException("Dough weight should be in the range [1..200].");
            }
            return weight;

        }

        private void SetFlourCalories(string flourType)
        {
            if (flourType == "white")
            {
                flourTypeCalories = 1.5;
            }
            else if (flourType == "wholegrain")
            {
                flourTypeCalories = 1.0;
            }
        }
        private void SetBakingTechniqueCalories(string technique)
        {
            if (technique == "crispy")
            {
                bakingTechniqueCalories = 0.9;
            }
            else if (technique == "chewy")
            {
                bakingTechniqueCalories = 1.1;
            }
            else if (technique == "homemade")
            {
                bakingTechniqueCalories = 1.0;
            }
        }

        public override string ToString()
        {
            return $"{Calories:f2}";
        }
    }
}
