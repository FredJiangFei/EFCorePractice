
using System.Collections.Generic;

namespace EFCorePractice
{
    public class SamuraiBattle
    {
        public int BattleId { get; set; }
        public Battle Battle { get; set; }
        public int SamuraiId { get; set; }
        public Samurai Samurai { get; set; }
    }
}