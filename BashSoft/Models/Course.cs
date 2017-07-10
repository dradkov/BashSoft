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
            this.studentsByName = new Dictionary<string, Student>();

        }

        public string  Name
        {
            get { return this.name; }
           private  set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException(nameof(this.name), ExceptionMessages.NullOrEmptyValue);
                }
                this.name = value;
            }
        }
        public IReadOnlyDictionary<string, Student> StudentsByName
        {
            get { return studentsByName; }
            
        }
        public void EnrolledStudents(Student student)
        {
            if (this.studentsByName.ContainsKey(student.UserName))
            {
                throw new ArgumentException(ExceptionMessages.InvalidInfo);
            }
            this.studentsByName.Add(student.UserName,student);
        }
    }
}
