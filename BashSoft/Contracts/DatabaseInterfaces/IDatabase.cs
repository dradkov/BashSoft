namespace BashSoft.Contracts.DatabaseInterfaces
{
   public interface IDatabase : IRequester,IFilteredTaker,IOrderedTaker
    {
        void LoadData(string fileName);

        void UnloadData();
    }
}
