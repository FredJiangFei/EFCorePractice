
using System.Collections.Generic;

namespace EFCorePractice
{
    public class SecretIdentity
    {
        public int Id { get; set; }
        public string RealName { get; set; }
        public int SamuraiId { get; set; }
        public Samurai Samurai { get; set; }
    }
}