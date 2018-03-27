using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace Toolbox.Data.Common.Extensions
{
    public static class DbReaderExtensions
    {
        public static bool IsDBNull(this DbDataReader reader, string fieldName)
        {
            int ordinal = reader.GetOrdinal(fieldName);

            return reader.IsDBNull(ordinal);
        }

        public static T GetFieldValue<T>(this DbDataReader reader, string fieldName)
        {
            int ordinal = reader.GetOrdinal(fieldName);

            return reader.GetFieldValue<T>(ordinal);
        }
    }
}
