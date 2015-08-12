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
    public class SchoolInterceptorLogging : DbCommandInterceptor
    {
        private ILogger _logger = new Logger(); //to write information log to
        private readonly Stopwatch _stopwatch = new Stopwatch(); //to write elapsed time in log

        //command ExecuteScalar is called when query will only return a single value (if more, then the first field of row)
        //when that is first called, intercept and start the stopwatch
        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            base.ScalarExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        //once it is finished running, stop the watch, if there was an exception, log the exception, otherwise log the request
        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
                _logger.Error(interceptionContext.Exception, "Error executing command: {0}", command.CommandText);

            else
                _logger.TraceApi("SQL Database", "SchoolInterceptor.ScalarExecuted", _stopwatch.Elapsed, "Command: {0}", command.CommandText);

            base.ScalarExecuted(command, interceptionContext);
        }

        //command ExecuteNonQuery is called when using Insert, Update, or Delete b/c not returning anything
        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            base.NonQueryExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        //once it is finished running, stop the watch, if there was an exception, log the exception, otherwise log the request
        public override void NonQueryExecuted(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
                _logger.Error(interceptionContext.Exception, "Error executing command: {0}", command.CommandText);

            else
                _logger.TraceApi("SQL Database", "SchoolInterceptor.NonQueryExecuted", _stopwatch.Elapsed, "Command: {0}", command.CommandText);

            base.NonQueryExecuted(command, interceptionContext);
        }

        //ExecuteReader is used when query returns more than one record ==> set of rows
        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            base.ReaderExecuting(command, interceptionContext);
            _stopwatch.Restart();
        }

        //once it is finished running, stop the watch, if there was an exception, log the exception, otherwise log the request
        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
        {
            _stopwatch.Stop();
            if (interceptionContext.Exception != null)
            {
                _logger.Error(interceptionContext.Exception, "Error executing command: {0}", command.CommandText);
            }
            else
            {
                _logger.TraceApi("SQL Database", "SchoolInterceptor.ReaderExecuted", _stopwatch.Elapsed, "Command: {0}: ", command.CommandText);
            }
            base.ReaderExecuted(command, interceptionContext);
        }

    }
}
