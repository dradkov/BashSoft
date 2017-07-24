namespace BashSoft.IO.Commands
{

    using BashSoft.Exceptions;
    using System.Diagnostics;
    using BashSoft.DataInfo;
    using BashSoft.Contracts;
    using BashSoft.Contracts.DatabaseInterfaces;

    public class OpenFileCommand : Command
    {
        public OpenFileCommand(string input, string[] data, IContentComparer judje, IDatabase repository, IDirectoryManager inputOutputmaneger)
            : base(input, data, judje, repository, inputOutputmaneger)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }
           
            string fileName = this.Data[1];
            Process.Start(SessionData.currentPath + "\\" + fileName);
        }
    }
}
