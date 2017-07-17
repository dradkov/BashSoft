using BashSoft.IO.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BashSoft.IO;
using BashSoft.Repository;
using BashSoft.Testing;
using BashSoft.Exceptions;

public class MakeDirectoryCommand : Command
{
    public MakeDirectoryCommand(string input, string[] data, Tester judje, StudentRepository repository, IOManager inputOutputmaneger) : base(input, data, judje, repository, inputOutputmaneger)
    {
    }

    public override void Execute()
    {
        if (this.Data.Length != 2)
        {
            throw new InvalidCommandException(this.Input);
        }


        string folderName = this.Data[1];
        this.InputOutputmaneger.CreateDirectoryInCurrentFolder(folderName);

    }
}

