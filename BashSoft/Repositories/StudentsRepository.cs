namespace BashSoft.Repository
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using IO;
    using DataInfo;
    using BashSoft.Models;
    using System;
    using System.Linq;
    using BashSoft.Contracts;
    using BashSoft.Contracts.DatabaseInterfaces;
    using BashSoft.Interfaces;
    using BashSoft.DataStructures;

    public class StudentRepository : IDatabase
    {

        private bool isDataInitialized;
        private IDataFilter filter;
        private IDataSorter sorter;
        private Dictionary<string, ICourse> courses;
        private Dictionary<string, IStudent> students;

        public StudentRepository(IDataFilter filter, IDataSorter sorter)
        {
            this.sorter = sorter;
            this.filter = filter;
           
        }

        public void LoadData(string fileName)
        {
            if (this.isDataInitialized)
            {
                throw new ArgumentException(ExceptionMessages.DataNotInitializedExceptionMessage);
             

            }

            this.students = new Dictionary<string, IStudent>();
            this.courses = new Dictionary<string, ICourse>();
            this.ReadData(fileName);
        }

        public void UnloadData()
        {
            if (!this.isDataInitialized)
            {
                throw new ArgumentException(ExceptionMessages.DataNotInitializedExceptionMessage);
            }
          
            this.students = null;
            this.courses = null;
            this.isDataInitialized = false;

        }

        private void ReadData(string fileName)
        {
            string path = SessionData.currentPath;
            if (Directory.Exists(path))
            {
                path += @"\" + fileName;
                string pattern = @"([A-Z][a-zA-Z#\++]*_[A-Z][a-z]{2}_\d{4})\s+([A-Za-z]+\d{2}_\d{2,4})\s([\s0-9]+)";
                Regex regex = new Regex(pattern);
                string[] allInputLines = File.ReadAllLines(path);
                for (int line = 0; line < allInputLines.Length; line++)
                {
                    if (!string.IsNullOrEmpty(allInputLines[line])
                        && regex.IsMatch(allInputLines[line]))
                    {
                        Match currentMatch = regex.Match(allInputLines[line]);
                        string courseName = currentMatch.Groups[1].Value;
                        string userName = currentMatch.Groups[2].Value;
                        string scoresStr = currentMatch.Groups[3].Value;
                        try
                        {
                            var score = scoresStr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(int.Parse)
                            .ToArray();

                            if (score.Any(x => x > 100 || x < 0))
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidScore);
                                continue;
                            }

                            if (score.Length > SoftUniCourse.numberOfTaskOnExam)
                            {
                                OutputWriter.DisplayException(ExceptionMessages.InvalidNumberOfScores);
                                continue;
                            }

                            if (!this.students.ContainsKey(userName))
                            {
                                this.students.Add(userName, new SoftUniStudent(userName));
                            }

                            if (!this.courses.ContainsKey(courseName))
                            {
                                this.courses.Add(courseName, new SoftUniCourse(courseName));
                            }

                            var course = this.courses[courseName];
                            var student = this.students[userName];

                            student.EnrollInCourse(course);
                            student.SetMarkOnCourse(courseName, score);
                            course.EnrolledStudents(student);
                        }
                        catch (Exception fex)
                        {

                           throw new ArgumentException(fex.Message + $"at line : {line}");
                        }
                        

                    }
                    
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidPath);
            }
            isDataInitialized = true;
            OutputWriter.WriteMessageOnNewLine("Data read!");
        }

        private bool IsQueryForCoursePossible(string courseName)
        {
            if (isDataInitialized)
            {
                if (this.courses.ContainsKey(courseName))
                {
                    return true;
                }
            }
            else
            {
                OutputWriter.DisplayException
                    (ExceptionMessages.DataNotInitializedExceptionMessage);
            }
            return false;
        }

        private bool IsQueryForStudentPossible(string courseName, string studentName)
        {
            if (this.IsQueryForCoursePossible(courseName)
                && this.courses[courseName].StudentsByName.ContainsKey(studentName))
            {
                return true;
            }

            else
            {
                OutputWriter.DisplayException
                    (ExceptionMessages.InexistingStudentInDataBase);
            }
            return false;
        }

        public void GetStudentScoresFromCourse(string courseName, string studentName)
        {
            if (IsQueryForStudentPossible(courseName, studentName))
            {
                OutputWriter.PrintStudent(new KeyValuePair<string, double> (studentName,this.courses[courseName].StudentsByName[studentName].MarksByCourseName[courseName]));
            }
        }

        public void GetStudentsFromCourse(string courseName)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                OutputWriter.WriteMessageOnNewLine(string.Format("{0}:", courseName));
                foreach (var studentGradesEntry in this.courses[courseName].StudentsByName)
                {
                    this.GetStudentScoresFromCourse(courseName, studentGradesEntry.Key);
                }
            }
        }

        public void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }
            }

            var marks = this.courses[courseName]
               .StudentsByName.ToDictionary(x => x.Key, x => x.Value.MarksByCourseName[courseName]);


            this.filter.FilterAndTake(marks, givenFilter, studentsToTake.Value);
        }

        public void OrderAndTake(string courseName, string comparison, int? studentsToTake = null)
        {
            if (IsQueryForCoursePossible(courseName))
            {
                if (studentsToTake == null)
                {
                    studentsToTake = this.courses[courseName].StudentsByName.Count;
                }
            }

            var marks = this.courses[courseName]
               .StudentsByName.ToDictionary(x => x.Key, x => x.Value.MarksByCourseName[courseName]);

            this.sorter.OrderAndTake(marks, comparison, studentsToTake.Value);
        }

        public ISimpleOrderedBag<ICourse> GetAllCoursesSorted(IComparer<ICourse> cmp)
        {
            SimpleSortedList<ICourse> sortedCourses = new SimpleSortedList<ICourse>(cmp);
            sortedCourses.AddAll(this.courses.Values);
            return sortedCourses;
        }

        public ISimpleOrderedBag<IStudent> GetAllStudentsSorted(IComparer<IStudent> cmp)
        {
            SimpleSortedList<IStudent> sortedStudents = new SimpleSortedList<IStudent>(cmp);
            sortedStudents.AddAll(this.students.Values);

            return sortedStudents;
        }
    }
}