﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Models
{
    public class University : IUniversity
    {
        private int id;
        private string name;
        private string category;
        private int capacity;
        private List<int> requiredSubjects;
        public University(int universityId, string universityName, string category, int capacity,
ICollection<int> requiredSubjects)
        {
            Id = universityId;
            Name = universityName;
            Category = category;
            Capacity = capacity;
            this.requiredSubjects = requiredSubjects.ToList<int>();
        }
        public int Id
        {
            get => this.id;
            private set
            {
                this.id = value;
            }
        }

        public string Name
        {
            get => this.name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.NameNullOrWhitespace));
                }
                this.name = value;
            }
        }

        public string Category
        {
            get => this.category;
            private set
            {
                if (value != "Technical" && value != "Economical" && value != "Humanity")
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.CategoryNotAllowed, value));
                }
                this.category = value;
            }
        }

        public int Capacity
        {
            get => this.capacity;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.CapacityNegative));
                }
                this.capacity = value;
            }

        }

        public IReadOnlyCollection<int> RequiredSubjects { get => this.requiredSubjects.AsReadOnly(); }
    }
}
