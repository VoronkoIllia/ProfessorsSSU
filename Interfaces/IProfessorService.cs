using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfessorsSSU.Models;


namespace ProfessorsSSU.Interfaces
{
    public interface IProfessorService
    {
        List<Professor> SelectProfessors();
    }
}
