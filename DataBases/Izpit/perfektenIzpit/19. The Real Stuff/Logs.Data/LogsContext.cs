using Logs.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs.Data
{
    public class LogsContext : DbContext
    {
        public LogsContext() : base("SearchLogsDB")
        {

        }

        public DbSet<Log> Logs { get; set; }

    }
}
