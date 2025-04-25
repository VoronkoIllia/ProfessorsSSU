using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfessorsSSU.Interfaces;
using ProfessorsSSU.Data;
using ProfessorsSSU.Models;

namespace ProfessorsSSU.Services
{
    public class ProfessorService : IProfessorService
    {
        private readonly AppDbContext _appDbContext;
        public ProfessorService(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public List<Professor> SelectProfessors()
        {
            return _appDbContext.Professors.ToList();
        }
    }
}
