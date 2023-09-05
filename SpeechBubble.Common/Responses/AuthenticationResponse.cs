using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpeechBubble.Common.Responses
{
    public class AuthenticationResponse
    {
        [Required]
        public bool Success { get; set; }
        
        [Required]
        public string token { get; set; }
    }
}
