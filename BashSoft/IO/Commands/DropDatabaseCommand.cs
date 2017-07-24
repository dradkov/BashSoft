namespace BashSoft.IO.Commands
{

    using BashSoft.Exceptions;
    using BashSoft.Contracts;
    using BashSoft.Contracts.DatabaseInterfaces;

    public class DropDatabaseCommand : Command
    {
        public DropDatabaseCommand(string input, string[] data, IContentComparer judje, IDatabase repository, IDirectoryManager inputOutputmaneger) : base(input, data, judje, repository, inputOutputmaneger)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 1)
            {
                throw new InvalidCommandException(this.Input);

            }
            this.Repository.UnloadData();
            OutputWriter.WriteMessageOnNewLine("Database Droped!");
        }
    }
}
