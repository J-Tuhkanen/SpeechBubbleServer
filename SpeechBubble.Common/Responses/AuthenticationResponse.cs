using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SpeechBubble.Common.Responses
{
    public class AuthenticationResponse
    {
        [Required]
        public bool success { get; set; }

        [Required]
        public string token { get; set; }

        public IEnumerable<Guid> rooms { get; set; }
    }
}
