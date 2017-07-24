namespace BashSoft.Contracts.DatabaseInterfaces
{
    public interface IOrderedTaker
    {
        void OrderAndTake(string courseName, string comparison, int? studentsToTake = null);
    }
}
