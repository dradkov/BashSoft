namespace BashSoft.IO.Commands
{
    using BashSoft.Attributes;
    using BashSoft.DataInfo;
    using BashSoft.Exceptions;
    using BashSoft.Contracts;

    [Alias("Is")]
    public class TraverseFoldersCommand : Command
    {
        [Inject]
        private IDirectoryManager inputOutputManeger;

        public TraverseFoldersCommand(string input, string[] data) : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length == 1 || this.Data.Length == 2)
            {
                if (this.Data.Length == 1)
                {
                    this.inputOutputManeger.TraverseDirectory(0);
                }
                else if (this.Data.Length == 2)
                {
                    int depth;
                    bool hasParsed = int.TryParse(this.Data[1], out depth);
                    if (hasParsed)
                    {
                        this.inputOutputManeger.TraverseDirectory(depth);
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
