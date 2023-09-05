using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpeechBubble.Server.Models;

namespace SpeechBubble.Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
    }
}
