using BashSoft.DataInfo;
using BashSoft.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.Models
{
   public class Course
    {
        private string name;
        private Dictionary<string, Student> studentsByName;

        public const int numberOfTaskOnExam = 5;
        public const int maxScoreOneExamTask = 100;

        public Course(string name)
        {
            this.Name = name;
            this.StudentsByName = new Dictionary<string, Student>();

        }

        public string  Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public Dictionary<string, Student> StudentsByName
        {
            get { return this.studentsByName; }
            set { this.studentsByName = value; }
        }
        public void EnrolledStudents(Student student)
        {
            if (this.studentsByName.ContainsKey(student.UserName))
            {
                OutputWriter.DisplayException(string.Format(ExceptionMessages.StudentAlreadyEnrolledInGivenCourse,student.UserName,this.Name));
                return;
            }
            this.studentsByName.Add(student.UserName,student);
        }
    }
}
