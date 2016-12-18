using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Oracle.Extensions
{
    public static class OracleCommandExtensions
    {
        public static OracleParameter CreateParameter(this OracleCommand command, string name, object value, OracleDbType type)
        {
            var parameter = command.CreateParameter();

            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.OracleDbType = type;

            return parameter;
        }

        public static void AddParameter(this OracleCommand command, string name, object value, OracleDbType type)
        {
            var parameter = command.CreateParameter();

            parameter.ParameterName = name;
            parameter.Value = value;
            parameter.OracleDbType = type;

            command.Parameters.Add(parameter);
        }
    }
}
