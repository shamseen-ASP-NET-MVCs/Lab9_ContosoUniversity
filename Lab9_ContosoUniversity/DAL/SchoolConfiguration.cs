using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace Lab9_ContosoUniversity.DAL
{
    public class SchoolConfiguration : DbConfiguration
    {
        //for connection resiliency, need to know when to retry, how many times, and time between
        //configuring resiliency settings here by making class inherit from DbConfiguration
        public SchoolConfiguration()
        {
            //Azure db has these settings configured, just need to set the execution strategy
            SetExecutionStrategy("System.Data.SqlClient,", () => new SqlAzureExecutionStrategy());
        }
    }
}
