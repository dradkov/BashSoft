namespace BashSoft.IO.Commands
{
    using BashSoft.Exceptions;
    using BashSoft.Contracts;
    using BashSoft.Contracts.DatabaseInterfaces;

    public class ChangeRelativePathCommand : Command
    {
        public ChangeRelativePathCommand(string input, string[] data, IContentComparer judje, IDatabase repository, IDirectoryManager inputOutputmaneger) : base(input, data, judje, repository, inputOutputmaneger)
        {
        }

        public override void Execute()
        {

            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
               
            }
            string relativePath = this.Data[1];
            this.InputOutputmaneger.ChangeCurrentDirectoryRelative(relativePath);

        }
    }
}
