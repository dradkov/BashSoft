﻿namespace BashSoft.IO.Commands
{
    using System;
    using System.Collections.Generic;
    using BashSoft.Contracts;
    using BashSoft.Contracts.DatabaseInterfaces;
    using BashSoft.Exceptions;
    using BashSoft.Interfaces;
    using BashSoft.Attributes;
   

    [Alias("display")]
    public class DisplayCommand : Command
    {
        [Inject]
        private IDatabase repository;

        public DisplayCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override void Execute()
        {
            if (this.Data.Length!=3)
            {
                throw new InvalidCommandException(this.Input);
            }

            var entityToDisplay = this.Data[1];
            var sortType = this.Data[2];

            if (entityToDisplay.Equals("students",StringComparison.OrdinalIgnoreCase))
            {
                IComparer<IStudent> studentComparator = this.CreateStudentComparator(sortType);
                ISimpleOrderedBag<IStudent> list = this.repository.GetAllStudentsSorted(studentComparator);
                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }
            else if (entityToDisplay.Equals("courses", StringComparison.OrdinalIgnoreCase))
            {
                IComparer<ICourse> courseComparator = this.CreateSCourseComparator(sortType);
                ISimpleOrderedBag<ICourse> list = this.repository.GetAllCoursesSorted(courseComparator);
                OutputWriter.WriteMessageOnNewLine(list.JoinWith(Environment.NewLine));
            }
            else
            {
                throw new InvalidCommandException(this.Input);
            }
        }

        private IComparer<ICourse> CreateSCourseComparator(string sortType)
        {
            if (sortType.Equals("ascending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<ICourse>
                    .Create((studentOne, studentTwo) => studentOne.CompareTo(studentTwo));
            }
            if (sortType.Equals("descending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<ICourse>
                    .Create((studentOne, studentTwo) => studentTwo.CompareTo(studentOne));
            }

            throw new InvalidCommandException(this.Input);

        }

        private IComparer<IStudent> CreateStudentComparator(string sortType)
        {
            if (sortType.Equals("ascending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<IStudent>
                    .Create((studentOne,studentTwo)=>studentOne.CompareTo(studentTwo));
            }
            if (sortType.Equals("descending", StringComparison.OrdinalIgnoreCase))
            {
                return Comparer<IStudent>
                    .Create((studentOne, studentTwo) => studentTwo.CompareTo(studentOne));
            }

             throw new InvalidCommandException(this.Input);

        }
    }
}
