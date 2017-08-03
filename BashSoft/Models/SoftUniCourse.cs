namespace BashSoft.Models
{
    using BashSoft.Contracts;
    using BashSoft.Exceptions;
    using System.Collections.Generic;
    using System;

    public class SoftUniCourse : ICourse
    {
        private string name;
        private Dictionary<string, IStudent> studentsByName;

        public const int numberOfTaskOnExam = 5;
        public const int maxScoreOneExamTask = 100;

        public SoftUniCourse(string name)
        {
            this.Name = name;
            this.studentsByName = new Dictionary<string, IStudent>();

        }

        public string  Name
        {
            get { return this.name; }
           private  set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }
                this.name = value;
            }
        }
        public IReadOnlyDictionary<string, IStudent> StudentsByName
        {
            get { return studentsByName; }
            
        }
        public void EnrolledStudents(IStudent student)
        {
            if (this.studentsByName.ContainsKey(student.UserName))
            {
                throw new DuplicateEntryInStructureException(student.UserName,this.Name);
            }
            this.studentsByName.Add(student.UserName,student);
        }

        public int CompareTo(ICourse other)
        {
            return this.Name.CompareTo(other.Name);
        }
    }
}
