using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfessorsSSU.Models
{
    public class Editor
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
    }
}
