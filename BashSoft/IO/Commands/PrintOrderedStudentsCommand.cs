﻿namespace BashSoft.IO.Commands
{
    using BashSoft.Attributes;
    using BashSoft.Exceptions;
    using BashSoft.DataInfo;
    using BashSoft.Contracts.DatabaseInterfaces;

    [Alias("order")]
    public class PrintOrderedStudentsCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public PrintOrderedStudentsCommand(string input, string[] data) : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length != 5)
            {
                throw new InvalidCommandException(this.Input);
               
            }
            this.DisplayOrder();
        }
        public void DisplayOrder()
        {
            string courseName = this.Data[1];
            string comparison = this.Data[2].ToLower();
            string takeCommand = this.Data[3].ToLower();
            string takeQuantity = this.Data[4].ToLower();
            this.TryParseParametersForOrderAndTake(takeCommand, takeQuantity, courseName, comparison);

        }
        private void TryParseParametersForOrderAndTake(string takeCommand, string takeQuantity,
           string courseName, string comparison)
        {
            if (takeCommand == "take")
            {
                if (takeQuantity == "all")
                {
                    this.repository.OrderAndTake(courseName, comparison);
                }
                else
                {
                    int studentsToTake;
                    bool hasParsed = int.TryParse(takeQuantity, out studentsToTake);
                    if (hasParsed)
                    {
                        this.repository.OrderAndTake(courseName, comparison, studentsToTake);
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
