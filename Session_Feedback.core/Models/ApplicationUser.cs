using Newtonsoft.Json;
using Session_Feedback.core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryLayer.Models
{
    [Table("Questions")]
    public class ApplicationUser 
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}
