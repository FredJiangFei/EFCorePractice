
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFCorePractice
{
    internal static class Module4Methods
    {
        private static MyDbContext _context = new MyDbContext();
        private static void AddSomeMoreSamurais()
        {
            _context.AddRange(
                new Samurai { Name = "Kambei Shimada" },
                new Samurai { Name = "Shichirōji " },
                new Samurai { Name = "Katsushirō Okamoto" },
                new Samurai { Name = "Heihachi Hayashida" },
                new Samurai { Name = "Kyūzō" },
                new Samurai { Name = "Gorōbei Katayama" }
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

        private static void RawSqlCommand()
        {
            var affected = _context.Database.ExecuteSqlCommand(
              "update samurais set Name=REPLACE(Name,'Ka','Fre')");
            Console.WriteLine($"Affected rows {affected}");
        }

        private static void RawSqlCommandWithOutput()
        {
            var procResult = new SqlParameter
            {
                ParameterName = "@procResult",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Output,
                Size = 50
            };
            _context.Database.ExecuteSqlCommand(
              "exec FindLongestName @procResult OUT", procResult);
            Console.WriteLine($"Longest name: {procResult.Value}");
        }

        private static void QueryWithNonSql()
        {
            var samurais = _context.Samurais
              .Select(s => new { newName = ReverseString(s.Name) })
              .ToList();
            samurais.ForEach(s => Console.WriteLine(s.newName));
            Console.WriteLine();
        }

        private static string ReverseString(string value)
        {
            var stringChar = value.AsEnumerable();
            return string.Concat(stringChar.Reverse());
        }
    }
}