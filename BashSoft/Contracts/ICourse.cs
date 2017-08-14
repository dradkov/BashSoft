namespace BashSoft.Contracts
{
    using System.Collections.Generic;
    using System;

    public interface ICourse : IComparable<ICourse>
    {
        string Name { get; }

        IReadOnlyDictionary<string, IStudent> StudentsByName { get; }

        void EnrolledStudents(IStudent student);

    }
}

