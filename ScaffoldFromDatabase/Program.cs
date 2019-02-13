using System;
using ScaffoldFromDatabase.Models;

namespace ScaffoldFromDatabase
{
    // dotnet ef dbcontext scaffold "Server=.;Database=EFCore;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models
    class Program
    {
        private static EFCoreContext _context = new EFCoreContext();
        static void Main(string[] args)
        {
            AddSomeMoreSamurais();
        }

        private static void AddSomeMoreSamurais()
        {
            _context.Samurais.AddRange(
               new Samurais { Name = "Fred" }
             );
            _context.SaveChanges();
        }
    }
}
