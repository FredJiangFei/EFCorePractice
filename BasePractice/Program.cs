using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;

namespace EFCorePractice
{
    class Program
    {
        private static MyDbContext _context = new MyDbContext();
        static void Main(string[] args)
        {
            // _context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
            // _context.Database.EnsureCreated();
        }
       
    }
}
