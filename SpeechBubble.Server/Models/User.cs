using Microsoft.AspNetCore.Identity;

namespace SpeechBubble.Server.Models
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; } 
    }
}
