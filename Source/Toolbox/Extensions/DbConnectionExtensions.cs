using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Extensions
{
    public static class DbConnectionExtensions
    {
        public static DbCommand CreateCommand(this DbConnection connection, string commandText)
        {
            var command = connection.CreateCommand();

            command.CommandText = commandText;

            return command;
        }
    }
}
