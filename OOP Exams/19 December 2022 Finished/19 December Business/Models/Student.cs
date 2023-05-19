using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Models
{
    public class Student : IStudent
    {
        private int id;
        private string firstName;
        private string lastName;
        private List<int> coveredExams;
        private IUniversity university;
        public Student(int studentId, string firstName, string lastName)
        {
            Id = studentId;
            FirstName = firstName;
            LastName = lastName;
            coveredExams = new List<int>();
            // da inicializiram university?
        }

        public int Id
        {
            get => this.id;
            private set
            {
                this.id = value;
            }
        }

        public string FirstName
        {
            get => this.firstName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.NameNullOrWhitespace));
                }
                this.firstName = value;
            }
        }

        public string LastName
        {
            get => this.lastName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.NameNullOrWhitespace));
                }
                this.lastName = value;
            }
        }

        public IReadOnlyCollection<int> CoveredExams { get => coveredExams.AsReadOnly(); }
            

        public IUniversity University { get => this.university; }
        

        public void CoverExam(ISubject subject)
        {
            this.coveredExams.Add(subject.Id);
        }

        public void JoinUniversity(IUniversity university)
        {
            this.university = university;
        }
    }
}
