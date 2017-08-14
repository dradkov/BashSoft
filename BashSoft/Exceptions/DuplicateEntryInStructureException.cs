using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashSoft.Exceptions
{
    public class DuplicateEntryInStructureException : Exception
    {
        public const string DuplicateEntry = "The {0} already exist in {1}.";

        public DuplicateEntryInStructureException(string message)
            :base(message)
        {

        }

        public DuplicateEntryInStructureException(string entity, string structure)
            :base(string.Format( DuplicateEntry, entity, structure))
        {

        }

    }
}
