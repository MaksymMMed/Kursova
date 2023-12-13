using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeechBackend.DAL.Entity
{
    public class Sentence
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public List<Attemp> Attemps { get; set; }
    }
}
