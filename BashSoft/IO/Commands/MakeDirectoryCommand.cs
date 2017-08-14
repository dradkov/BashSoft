﻿namespace BashSoft.IO.Commands
{
    using BashSoft.Attributes;
    using BashSoft.Exceptions;
    using BashSoft.Contracts;

    [Alias("mkdir")]
    public class MakeDirectoryCommand : Command
    {
        [Inject]
        private IDirectoryManager inputOutputManeger;

        public MakeDirectoryCommand(string input, string[] data) : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new InvalidCommandException(this.Input);
            }

            string folderName = this.Data[1];
            this.inputOutputManeger.CreateDirectoryInCurrentFolder(folderName);
        }
    }
}

