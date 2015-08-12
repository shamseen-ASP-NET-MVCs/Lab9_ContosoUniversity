using System;
using System.Diagnostics;
using System.Text;

namespace Lab9_ContosoUniversity.Logging
{
    public class Logger : ILogger
    {
        //building a message for methods to pass into TraceInformation if there isn't a pre-formatted
        //message already passed in (first overload of each method)
        //basically appends the array of objects and the exception message.
        private static string FormatExceptionMessage(Exception exception, string fmt, object[] vars)
        {
            var sb = new StringBuilder();
            sb.Append(string.Format(fmt, vars));
            sb.Append(" Exception: ");
            sb.Append(exception.ToString());
            return sb.ToString();
        }

        //writing informative message
        public void Information(string message)
        {
            //writes message to Trace listeners (in Trace.Listeners collection)
            Trace.TraceInformation(message);
        }
        public void Information(string fmt, params object[] vars)
        {
            //gives listeners an array of objects formatted with fmt
            Trace.TraceInformation(fmt, vars);
        }
        public void Information(Exception exception, string fmt, params object[] vars)
        {
            Trace.TraceInformation(FormatExceptionMessage(exception, fmt, vars));
        }

        //writing warning 
        public void Warning(string message)
        {
            Trace.TraceWarning(message);
        }
        public void Warning(string fmt, params object[] vars)
        {
            Trace.TraceWarning(fmt, vars);
        }
        public void Warning (Exception exception, string fmt, params object[] vars)
        {
            Trace.TraceWarning(FormatExceptionMessage(exception, fmt, vars));
        }

        //writes error message
        public void Error(string message)
        {
            Trace.TraceError(message);
        }
        public void Error(string fmt, params object[] vars)
        {
            Trace.TraceError(fmt, vars);
        }
        public void Error(Exception exception, string fmt, params object[] vars)
        {
            Trace.TraceError(FormatExceptionMessage(exception, fmt, vars));
        }

        //takes info and makes a message that shows the component(external service it's requesting to), method that's intercepting,
        //time elapsed, and properties. then shows it as an informative message to trace listeners
        public void TraceApi (string componentName, string method, TimeSpan timespan, string properties)
        {
            string message = String.Concat("Component:", componentName, "; Method:", method, ";Timespan:", 
                timespan.ToString(), ";Properties:", properties);

            Trace.TraceInformation(message);

        }

        //overload if properties aren't passed in
        public void TraceApi (string componentName, string method, TimeSpan timespan)
        {
            TraceApi(componentName, method, timespan, "");
        }

        //overload if properties are passed in as an array of strings
        public void TraceApi(string componentName, string method, TimeSpan timespan, string fmt, params object[] vars)
        {
            TraceApi(componentName, method, timespan, string.Format(fmt, vars));
        }
    }
}
