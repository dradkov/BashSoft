namespace BashSoft.Contracts.DatabaseInterfaces
{
   
   public interface IFilteredTaker
    {
        void FilterAndTake(string courseName, string givenFilter, int? studentsToTake = null);
    }
}
