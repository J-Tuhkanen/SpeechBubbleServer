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

        public string[] rooms { get; set; } = new[] 
        { 
            "694a54ea-4816-48ea-8944-7c1d3b867f53",
            "f8fcddec-a2ee-4ecb-8e6f-df32e1932759"
        };
    }
}
