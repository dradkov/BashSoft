namespace BashSoft.IO
{
    using Repository;
    using Testing;
    using System.IO;
    using BashSoft.Exceptions;
    using BashSoft.IO.Commands;
    using BashSoft.Contracts;
    using BashSoft.Contracts.DatabaseInterfaces;

    public class CommandInterpreter: IInterpreter
    {
        private IContentComparer judge;
        private IDatabase repository;
        private IDirectoryManager inputOutputManager;

        public CommandInterpreter(IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
        {
            this.judge = judge;
            this.repository = repository;
            this.inputOutputManager = inputOutputManager;

        }

        public void InterpretCommand(string input)
        {
            string[] data = input.Split();
            string commandName = data[0].ToLower();

            try
            {
                IExecutable command = this.ParseCommand(input,commandName,data);
                command.Execute();
            }
           
            catch (DirectoryNotFoundException ex)
            {

                OutputWriter.DisplayException(ex.Message);
            }
            
        }

        private IExecutable ParseCommand(string input, string command, string[] data)
        {
            switch (command)
            {
                case "open":
                    return new OpenFileCommand(input, data, judge, this.repository, this.inputOutputManager);

                case "mkdir":
                    return new MakeDirectoryCommand(input, data, judge, this.repository, this.inputOutputManager);

                case "Is":
                    return new TraverseFoldersCommand(input, data, judge, this.repository, this.inputOutputManager);
                case "cmp":
                    return new CompareFilesCommand(input, data, judge, this.repository, this.inputOutputManager);
                case "cdRel":
                    return new ChangeRelativePathCommand(input, data, judge, this.repository, this.inputOutputManager);
                case "cdAbs":
                    return new ChangeAbsolutePathCommand(input, data, judge, this.repository, this.inputOutputManager);
                case "readDB":
                    return new ReadDatabaseCommand(input, data, judge, this.repository, this.inputOutputManager);

                case "help":
                    return new GetHelpCommand(input, data, judge, this.repository, this.inputOutputManager);

                case "filter":
                    return new PrintFilteredStudentsCommand(input, data, judge, this.repository, this.inputOutputManager);
                case "order":
                    return new PrintOrderedStudentsCommand(input, data, judge, this.repository, this.inputOutputManager);
                case "dropdb":
                    return new DropDatabaseCommand(input, data, judge, this.repository, this.inputOutputManager);
                case "download":
                    return new DownloadRequestCommand(input, data, judge, this.repository, this.inputOutputManager);
                case "downloadAsynch":
                    return new DownloadAsyncRequest(input, data, judge, this.repository, this.inputOutputManager);
                case "show":
                    return new ShowCourseCommand(input, data, judge, this.repository, this.inputOutputManager);
                case "display":
                    return new DisplayCommand(input, data, judge, this.repository, this.inputOutputManager);
                default:
                    throw new InvalidCommandException(input);
                   
            }
        }

    }
}