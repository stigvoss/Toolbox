using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Data.Common.Extensions
{
    public static class DbCommandExtensions
    {
        public static DbParameter CreateParameter(this DbCommand command, string name, object value, DbType type)
        {
            var parameter = command.CreateParameter();

            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.DbType = type;

            return parameter;
        }

        public static void AddParameter(this DbCommand command, string name, object value, DbType type)
        {
            var parameter = command.CreateParameter();

            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.DbType = type;

            command.Parameters.Add(parameter);
        }
    }
}
