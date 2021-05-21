using Microsoft.EntityFrameworkCore;

namespace ZeroKnowledgeProofApp.Other
{
    class ZeroKnowledgeProofDbContext: DbContext
    {
        public ZeroKnowledgeProofDbContext() : base()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=./ZeroKnowledgeProofDB.db");
            }
        }
    }
}
