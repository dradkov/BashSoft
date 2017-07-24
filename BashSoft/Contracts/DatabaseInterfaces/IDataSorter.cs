namespace BashSoft.Contracts.DatabaseInterfaces
{
    using System.Collections.Generic;

    public interface IDataSorter
    {
        void OrderAndTake(Dictionary<string, double> studentWithMarks,
            string comparison, int studentsToTake);
    }
}
