using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechBackend.DAL.Entity
{
    public class Attemp
    {
        public int Id { get; set; } 
        public DateTime DateTime { get; set; }
        public float Accuracy { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int SentenceId { get; set; }
        public Sentence Sentence { get; set; }
    }
}
