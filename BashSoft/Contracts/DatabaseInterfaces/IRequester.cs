namespace BashSoft.Contracts.DatabaseInterfaces
{
    public interface IRequester
    {
        void GetStudentScoresFromCourse(string courseName, string studentName);

        void GetStudentsFromCourse(string courseName);
    }
}
