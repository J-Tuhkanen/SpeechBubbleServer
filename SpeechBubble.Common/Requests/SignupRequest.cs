using System.ComponentModel.DataAnnotations;

namespace SpeechBubble.Common.Requests
{
    public class SignupRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
