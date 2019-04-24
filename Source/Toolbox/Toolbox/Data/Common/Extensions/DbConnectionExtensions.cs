using System.Data.Common;

namespace Toolbox.Data.Common.Extensions
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
