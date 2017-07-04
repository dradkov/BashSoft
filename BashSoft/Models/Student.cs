using BashSoft.DataInfo;
using BashSoft.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.Models
{
    public class Student
    {
        private string userName;
        private Dictionary<string, Course> enrolledCourses;
        private Dictionary<string, double> marksByCourseName;


        public Student(string userName)
        {
            this.UserName = userName;
            this.EnrolledCourses = new Dictionary<string, Course>();
            this.MarksByCourseName = new Dictionary<string, double>();

        }

        public string UserName
        {
            get { return this.userName; }
            set { this.userName = value; }
        }

        public Dictionary<string, Course> EnrolledCourses
        {
            get { return this.enrolledCourses; }
            set { this.enrolledCourses = value; }
        }

      
        public Dictionary<string, double> MarksByCourseName
        {
            get { return this.marksByCourseName; }
            set { this.marksByCourseName = value; }
        }

        public void EnrollInCourse(Course course)
        {
            if (this.enrolledCourses.ContainsKey(course.Name))
            {
                OutputWriter.DisplayException(string.Format(ExceptionMessages.StudentAlreadyEnrolledInGivenCourse,
                    this.userName, course.Name));
                return;
            }
            this.enrolledCourses.Add(course.Name, course);
        }
        public void SetMarkOnCourse(string courseName, params int[] scores)
        {
            if (!this.enrolledCourses.ContainsKey(courseName))
            {
                OutputWriter.DisplayException(ExceptionMessages.NotEnrolledInCourse);
                return;
            }
            if (scores.Length> Course.numberOfTaskOnExam )
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                return;
            }
            this.marksByCourseName.Add(courseName,CalculateMark(scores));
        }

        private double CalculateMark(int[] scores)
        {
            double percentageOfSolvedExam =
                scores.Sum() / (double)(Course.numberOfTaskOnExam * Course.maxScoreOneExamTask);
            double mark = percentageOfSolvedExam * 4 + 2;
            return mark;
        }
    } 
}
