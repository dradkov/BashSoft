namespace BashSoft.IO.Commands
{
    using BashSoft.Attributes;
    using BashSoft.Exceptions;
    using BashSoft.Contracts;
    using BashSoft.Contracts.DatabaseInterfaces;

    [Alias("cdAbs")]
    public class ChangeAbsolutePathCommand : Command
    {
        [Inject]
        private IDirectoryManager inputOutputmaneger;

        public ChangeAbsolutePathCommand(string input, string[] data) 
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string absolutePath = this.Data[1];
            this.inputOutputmaneger.ChangeCurrentDirectoryAbsolute(absolutePath);
        }
    }
}
