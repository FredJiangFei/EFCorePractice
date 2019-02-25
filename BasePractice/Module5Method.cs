
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFCorePractice
{
    internal static class Module5Methods
    {
        private static MyDbContext _context = new MyDbContext();

        private static void AddManyToManyWithFks()
        {
            var sb = new SamuraiBattle { SamuraiId = 1, BattleId = 1 };
            _context.SamuraiBattles.Add(sb);
            _context.SaveChanges();
        }

        private static void EagerLoadWithInclude()
        {
            var samuraiWithQuotes = _context.Samurais.Include(s => s.Quotes).ToList();
        }

        private static void EagerLoadManyToManyAkaChildrenGrandchildren()
        {
            var samuraiWithBattles = _context.Samurais
              .Include(s => s.SamuraiBattles)
              .ThenInclude(sb => sb.Battle).ToList();
        }

        private static void EagerLoadFilteredManyToManyAkaChildrenGrandchildren()
        {
            var samuraiWithBattles = _context.Samurais
              .Include(s => s.SamuraiBattles)
              .ThenInclude(sb => sb.Battle)
              .Where(s => s.Name == "Kyūzō").ToList();
        }

        private static void EagerLoadWithMultipleBranches()
        {
            var samurais = _context.Samurais
              .Include(s => s.SecretIdentity)
              .Include(s => s.Quotes).ToList();
        }

        private static void EagerLoadWithFromSql()
        {
            var samurais = _context.Samurais.FromSql("select * from samurais")
              .Include(s => s.Quotes)
              .ToList();
        }

        private static void EagerLoadFilterChildrenNope()
        {
            //this won't work. No filtering, no sorting on Include
            // var samurais = _context.Samurais
            //   .Include(s => s.Quotes.Where(q => q.Text.Contains("happy")))
            //   .ToList();

            var samurais = _context.Samurais
            .Where(s => s.Quotes.Any(q => q.Text.Contains("happy")))
            .ToList();

            // var samurais = _context.Samurais
            // .Select(s => new
            // {
            //     Samurai = s,
            //     Quotes = s.Quotes
            //             .Where(q => q.Text.Contains("happy"))
            //             .ToList()
            // })
            // .ToList();

            Console.WriteLine(samurais.Count());
        }

        private static void RelatedObjectsFixUp()
        {
            var samurai = _context.Samurais.Find(5);
            var quotes = _context.Quotes.Where(q => q.SamuraiId == 5).ToList();
            Console.WriteLine(samurai.Quotes.Count());
        }

        private static void ExplicitLoad()
        {
            var samurai = _context.Samurais.FirstOrDefault(x => x.Id == 5);
            _context.Entry(samurai).Collection(s => s.Quotes).Load();
            _context.Entry(samurai).Reference(s => s.SecretIdentity).Load();

            Console.WriteLine(samurai.Quotes.Count());
        }

        private static void ExplicitLoadWithChildFilter()
        {
            var samurai = _context.Samurais.FirstOrDefault(x => x.Id == 5);
            _context.Entry(samurai)
              .Collection(s => s.Quotes)
               .Query()
              .Where(q => q.Text.Contains("happy"))
              .Load();

            Console.WriteLine(samurai.Quotes.Count());

        }
    }
}