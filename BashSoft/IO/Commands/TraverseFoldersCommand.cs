namespace BashSoft.IO.Commands
{
    using BashSoft.DataInfo;
    using BashSoft.Exceptions;
    using BashSoft.Contracts;
    using BashSoft.Contracts.DatabaseInterfaces;

    public class TraverseFoldersCommand : Command
    {
        public TraverseFoldersCommand(string input, string[] data, IContentComparer judje, IDatabase repository, IDirectoryManager inputOutputmaneger) : base(input, data, judje, repository, inputOutputmaneger)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 1 || this.Data.Length == 2)
            {
                if (this.Data.Length == 1)
                {
                    this.InputOutputmaneger.TraverseDirectory(0);
                }
                else if (this.Data.Length == 2)
                {
                    int depth;
                    bool hasParsed = int.TryParse(this.Data[1], out depth);
                    if (hasParsed)
                    {
                        this.InputOutputmaneger.TraverseDirectory(depth);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.UnableToParseNumber);
                    }
                }
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }
    }
}
