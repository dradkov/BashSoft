namespace BashSoft.IO.Commands
{
    using BashSoft.Exceptions;
    using BashSoft.Contracts;
    using BashSoft.Contracts.DatabaseInterfaces;

    public class CompareFilesCommand : Command
    {
        public CompareFilesCommand(string input, string[] data, IContentComparer judje, IDatabase repository, IDirectoryManager inputOutputmaneger) : base(input, data, judje, repository, inputOutputmaneger)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 3)
            {
                throw new InvalidCommandException(this.Input);
               
            }
            string firstPath = this.Data[1];
            string secondPath = this.Data[2];
            this.Judge.CompareContent(firstPath, secondPath);
        }
    }
}
