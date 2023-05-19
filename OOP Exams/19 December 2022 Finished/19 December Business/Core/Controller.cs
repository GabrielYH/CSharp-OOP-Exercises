using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UniversityCompetition.Core.Contracts;
using UniversityCompetition.Models;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories;
using UniversityCompetition.Repositories.Contracts;
using UniversityCompetition.Utilities.Messages;

namespace UniversityCompetition.Core
{
    public class Controller : IController
    {
        private IRepository<ISubject> SubjectRepository;
        private IRepository<IStudent> StudentRepository;
        private IRepository<IUniversity> UniversityRepository;
        private int studentCounter = 0;
        //private int subjectCounter = 0;
        //private int universityCounter = 0;
        public Controller()
        {
            SubjectRepository = new SubjectRepository();
            StudentRepository = new StudentRepository();
            UniversityRepository = new UniversityRepository();
        }
        public string AddStudent(string firstName, string lastName)
        {
            if (this.StudentRepository.Models.Any(s => s.FirstName == firstName && s.LastName == lastName))
            {
                return string.Format(OutputMessages.AlreadyAddedStudent, firstName, lastName);
            }
            studentCounter++;
            IStudent student = new Student(studentCounter, firstName, lastName);
            this.StudentRepository.AddModel(student);
            return string.Format(OutputMessages.StudentAddedSuccessfully, firstName, lastName, this.StudentRepository.GetType().Name);
        }

        public string AddSubject(string subjectName, string subjectType)
        {
            if (subjectType != "HumanitySubject" && subjectType != "EconomicalSubject" && subjectType != "TechnicalSubject")
            {
                return string.Format(OutputMessages.SubjectTypeNotSupported, subjectType);
            }
            if (this.SubjectRepository.FindByName(subjectName) != null)
            {
                return string.Format(OutputMessages.AlreadyAddedSubject, subjectName);
            }
            //subjectCounter++;
            ISubject subject;
            if (subjectType == "HumanitySubject")
            {
                subject = new HumanitySubject(this.SubjectRepository.Models.Count+1, subjectName);
            }
            else if (subjectType == "EconomicalSubject")
            {
                subject = new EconomicalSubject(this.SubjectRepository.Models.Count + 1, subjectName);
            }
            else if (subjectType == "TechnicalSubject")
            {
                subject = new TechnicalSubject(this.SubjectRepository.Models.Count + 1, subjectName);
            }
            else
            {
                subject = null;
            }
            this.SubjectRepository.AddModel(subject);
            return string.Format(OutputMessages.SubjectAddedSuccessfully, subjectType, subjectName, this.SubjectRepository.GetType().Name);
        }

        public string AddUniversity(string universityName, string category, int capacity, List<string> requiredSubjects)
        {
            if (this.UniversityRepository.FindByName(universityName) != null)
            {
                return string.Format(OutputMessages.AlreadyAddedUniversity, universityName);
            }
            List<int> requiredSubjectsIds = new();
            foreach (var subject in this.SubjectRepository.Models)
            {
                if (requiredSubjects.Contains(subject.Name))
                {
                    requiredSubjectsIds.Add(subject.Id);
                }
            }
            //universityCounter++;
            IUniversity university = new University(this.UniversityRepository.Models.Count+1, universityName, category, capacity, requiredSubjectsIds);
            this.UniversityRepository.AddModel(university);
            return string.Format(OutputMessages.UniversityAddedSuccessfully, universityName, this.UniversityRepository.GetType().Name);
        }

        public string ApplyToUniversity(string studentName, string universityName)
        {
            string[] splitted = studentName.Split(" ",StringSplitOptions.RemoveEmptyEntries);
            string firstName = splitted[0];
            string lastName = splitted[1];
            IStudent student = this.StudentRepository.FindByName(studentName);
            if (student == null)
            {
                return string.Format(OutputMessages.StudentNotRegitered, firstName,lastName);
            }
            IUniversity university = this.UniversityRepository.FindByName(universityName);
            if (university == null)
            {
                return string.Format(OutputMessages.UniversityNotRegitered, universityName);
            }
            bool hasCoveredAllRequiredExams = university.RequiredSubjects.All(e => student.CoveredExams.Contains(e));
            if (hasCoveredAllRequiredExams == false)
            {
                return string.Format(OutputMessages.StudentHasToCoverExams, studentName, universityName);
            }
            if (student.University == university)
            {
                return string.Format(OutputMessages.StudentAlreadyJoined, student.FirstName, student.LastName, universityName);
            }
            student.JoinUniversity(university);
            return string.Format(OutputMessages.StudentSuccessfullyJoined, student.FirstName, student.LastName, universityName);
        }

        public string TakeExam(int studentId, int subjectId)
        {
            IStudent student = this.StudentRepository.FindById(studentId);
            if (student == null)
            {
                return string.Format(OutputMessages.InvalidStudentId);
            }
            ISubject subject = this.SubjectRepository.FindById(subjectId);
            if (subject == null)
            {
                return string.Format(OutputMessages.InvalidSubjectId);
            }
            if (student.CoveredExams.Any(e => e == subjectId))
            {
                return string.Format(OutputMessages.StudentAlreadyCoveredThatExam, student.FirstName, student.LastName, subject.Name);
            }
            student.CoverExam(subject);
            return string.Format(OutputMessages.StudentSuccessfullyCoveredExam, student.FirstName, student.LastName, subject.Name);
        }

        public string UniversityReport(int universityId)
        {
            IUniversity university = this.UniversityRepository.FindById(universityId);
            var studentsInThisUniversity = this.StudentRepository.Models.Where(s => s.University == university);
            int studentsCount = studentsInThisUniversity.Count();
            StringBuilder sb = new();
            sb.AppendLine($"*** {university.Name} ***");
            sb.AppendLine($"Profile: {university.Category}");
            sb.AppendLine($"Students admitted: {studentsCount}");
            sb.AppendLine($"University vacancy: {university.Capacity - studentsCount}");
            return sb.ToString().Trim();
        }
    }
}
