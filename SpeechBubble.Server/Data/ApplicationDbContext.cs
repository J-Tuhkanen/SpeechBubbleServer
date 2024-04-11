using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpeechBubble.Common.Data;
using SpeechBubble.Server.Models;

namespace SpeechBubble.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Room> Room { get; set; }
        public DbSet<Message> Message { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Message>()
                .Property(e => e.Timestamp)
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
