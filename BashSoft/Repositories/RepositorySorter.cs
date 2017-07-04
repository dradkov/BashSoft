namespace BashSoft.Repository
{
    using System.Collections.Generic;
    using System.Linq;
    using IO;
    using DataInfo;

    public class RepositorySorter
    {
        public void OrderAndTake(Dictionary<string,double> studentWithMarks,
            string comparison, int studentsToTake)
        {
            comparison = comparison.ToLower();
            
            if (comparison == "ascending")
            {
                this.PrintStudents(studentWithMarks
                    .OrderBy(x => x.Value)
                    .Take(studentsToTake)
                    .ToDictionary(pair => pair.Key, pair => pair.Value));
            }
            else if (comparison == "descending")
            {
                this.PrintStudents(studentWithMarks
                   .OrderByDescending(x => x.Value)
                   .Take(studentsToTake)
                   .ToDictionary(pair => pair.Key, pair => pair.Value));
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidComparisonQuery);
            }
        }

        private void PrintStudents(Dictionary<string, double> sortedStudents)
        {
            foreach (KeyValuePair<string, double> kvp in sortedStudents)
            {
                OutputWriter.PrintStudent(kvp);
            }
        }
    }
}