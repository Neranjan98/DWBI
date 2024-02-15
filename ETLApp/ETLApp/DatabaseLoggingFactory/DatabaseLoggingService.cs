using ETLApp.DataAccess;
using ETLApp.Models;
using FunctionApp1.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETLApp.LoggingFactory
{
    public class DatabaseLoggingService : IDatabaseLog
    {
        private LoggingContext _context;

        public DatabaseLoggingService(LoggingContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void Log(string message)
        {
            _context.Add<DatabaseLog>(new DatabaseLog
            {
                RunStatus = message,
                RunTime = DateTime.Now
            });

            _context.SaveChanges();
        }
    }
}
