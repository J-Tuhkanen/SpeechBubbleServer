using System;
using System.ComponentModel.DataAnnotations;

namespace SpeechBubble.Common.Data
{
    public class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
