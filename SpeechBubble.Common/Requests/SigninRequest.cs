using System.ComponentModel.DataAnnotations;

namespace SpeechBubble.Common.Requests
{
    public class SigninRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
