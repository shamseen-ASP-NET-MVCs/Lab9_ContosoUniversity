using System;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.Linq;
using Lab9_ContosoUniversity.Logging;

namespace Lab9_ContosoUniversity.DAL
{
    public class SchoolInterceptorTransientErrors : DbCommandInterceptor
    {
        //dummy errors to test resilience of queries that return more than one value
        private int _counter = 0;
        private ILogger _logger = new Logger();

        //overriding ReaderExecuting to check connection resiliency just for one type of error (20)
        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            bool throwTransientErrors = false;

            //if you search "Throw" in the students index, throw this one error.
            if (command.Parameters.Count > 0 && command.Parameters[0].Value.ToString() == "%Throw%")
            {
                throwTransientErrors = true;
                command.Parameters[0].Value = "%an%"; //first name search parameter
                command.Parameters[1].Value = "%an%";
            }

            //also check if connection has been retried 4 times. if it has, don't throw dummy error
            //because transient errors are usually resolved after a few tries (execution policy defined 4 max)
            if (throwTransientErrors && _counter < 4)
            {
                _logger.Information("Returning transient error for command: {0}", command.CommandText);
                _counter++;
                interceptionContext.Exception = CreateDummySqlException();
            }
        }

        private SqlException CreateDummySqlException()
        {
            // The instance of SQL Server you attempted to connect to does not support encryption
            var sqlErrorNumber = 20;

            var sqlErrorCtor = typeof(SqlError).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Where(c => c.GetParameters().Count() == 7).Single();
            var sqlError = sqlErrorCtor.Invoke(new object[] { sqlErrorNumber, (byte)0, (byte)0, "", "", "", 1 });

            var errorCollection = Activator.CreateInstance(typeof(SqlErrorCollection), true);
            var addMethod = typeof(SqlErrorCollection).GetMethod("Add", BindingFlags.Instance | BindingFlags.NonPublic);
            addMethod.Invoke(errorCollection, new[] { sqlError });

            var sqlExceptionCtor = typeof(SqlException).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic).Where(c => c.GetParameters().Count() == 4).Single();
            var sqlException = (SqlException)sqlExceptionCtor.Invoke(new object[] { "Dummy", errorCollection, null, Guid.NewGuid() });

            return sqlException;
        }
    }
}
