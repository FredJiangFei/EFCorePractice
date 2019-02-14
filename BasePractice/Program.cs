using System;
using System.Collections.Generic;
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
            _context.GetService<ILoggerFactory>().AddProvider(new MyLoggerProvider());
            RawSqlQuery();
        }

        private static void AddSomeMoreSamurais()
        {
            _context.AddRange(
               new Samurai { Name = "Kambei Shimada" },
               new Samurai { Name = "Shichirōji " }
             );
            _context.SaveChanges();
        }

        private static void InsertMultipleSamurais()
        {
            var samurai = new Samurai { Name = "Julie" };
            var samuraiSammy = new Samurai { Name = "Sampson" };
            _context.Samurais.AddRange(new List<Samurai> { samurai, samuraiSammy });
            _context.SaveChanges();
        }

        private static void DeleteWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Kambei Shimada");
            _context.Samurais.Remove(samurai);
            //alternates:
            // _context.Remove(samurai);
            // _context.Entry(samurai).State=EntityState.Deleted;
            // _context.Samurais.Remove(_context.Samurais.Find(1));
            _context.SaveChanges();
        }

        private static void RawSqlQuery()
        {
            // var samurais= _context.Samurais.FromSql("Select * from Samurais")
            //              .OrderByDescending(s => s.Name)
            //              .Where(s=>s.Name.Contains("Julie"))
            //              .ToList();
            var namePart = "Julie";
            var samurais = _context.Samurais
              .FromSql("EXEC FilterSamuraiByNamePart {0}", namePart)
              .OrderByDescending(s => s.Name).ToList();

            samurais.ForEach(s => Console.WriteLine(s.Name));
        }
    }
}
