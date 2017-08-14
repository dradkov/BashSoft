namespace BashSoft.IO.Commands
{
    using BashSoft.Attributes;
    using BashSoft.Exceptions;
    using BashSoft.Contracts;

    [Alias("cdRel")]
    public class ChangeRelativePathCommand : Command
    {
        [Inject]
        private IDirectoryManager inputOutputManeger;


        public ChangeRelativePathCommand(string input, string[] data) 
            : base(input, data)
        {
        }

        public override void Execute()
        {

            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
               
            }
            string relativePath = this.Data[1];

           this.inputOutputManeger.ChangeCurrentDirectoryRelative(relativePath);

        }

       
    }
}
