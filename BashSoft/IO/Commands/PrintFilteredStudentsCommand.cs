namespace BashSoft.IO.Commands
{
    using BashSoft.Exceptions;
    using BashSoft.DataInfo;
    using BashSoft.Contracts;
    using BashSoft.Contracts.DatabaseInterfaces;

    public class PrintFilteredStudentsCommand : Command
    {
        public PrintFilteredStudentsCommand(string input, string[] data, IContentComparer judje, IDatabase repository, IDirectoryManager inputOutputmaneger) : base(input, data, judje, repository, inputOutputmaneger)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 5)
            {
                throw new InvalidCommandException(this.Input);
               
            }
            this.DisplayFilter();
        }
        private void DisplayFilter()
        {
            string courseName = this.Data[1];
            string filter = this.Data[2].ToLower();
            string takeCommand = this.Data[3].ToLower();
            string takeQuantity = this.Data[4].ToLower();
            this.TryParseParametersForFilterAndTake(takeCommand, takeQuantity, courseName, filter);

        }
        private void TryParseParametersForFilterAndTake(string takeCommand, string takeQuantity,
            string courseName, string filter)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    this.Repository.FilterAndTake(courseName, filter);
                }
                else
                {
                    int studentsToTake;
                    bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        this.Repository.FilterAndTake(courseName, filter, studentsToTake);
                    }
                    else
                    {
                        OutputWriter.DisplayException(ExceptionMessages.InvalidTakeQuantityParameter);
                    }
                }
            }
            else
            {
                OutputWriter.DisplayException(ExceptionMessages.InvalidTakeCommandParameter);
            }
        }

    }
}
