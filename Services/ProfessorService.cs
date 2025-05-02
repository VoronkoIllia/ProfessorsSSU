using ProfessorsSSU.Interfaces;
using ProfessorsSSU.Data;
using ProfessorsSSU.Models;

namespace ProfessorsSSU.Services
{
    public class ProfessorService : IProfessorService
    {

        private readonly int RETIREMENT_AGE = 60; // нижня межа пенсійного віку

        private readonly AppDbContext _appDbContext;
        public ProfessorService(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public List<Professor> SelectProfessors(bool? hasAcademicRank = null, bool onlyPensioners = false) 
        {
            int maxBirthYear = int.MaxValue;
            if (onlyPensioners)
            {
                maxBirthYear = DateTime.Now.Year - RETIREMENT_AGE;
            }

            if (hasAcademicRank == true)
            {
                return _appDbContext.Professors.Where(p => p.BirthYear < maxBirthYear && p.AcademicRank != null).ToList();
            }
            else if (hasAcademicRank == false)
            { 
                return _appDbContext.Professors.Where(p => p.BirthYear < maxBirthYear && p.AcademicRank == null).ToList();
            }

            return _appDbContext.Professors.Where(p => p.BirthYear < maxBirthYear).ToList();
        }

        public void AddNewProfessor(Professor professor) 
        {
            _appDbContext.Professors.Add(professor);
            _appDbContext.SaveChanges();
        }

        public bool UpdateProfessor(int professorId, Professor newProfessorData)
        {
            Professor? professor = _appDbContext.Professors.FirstOrDefault((professor) => professor.Id == professorId);
            if (professor == null)
            {
                return false;
            }

            _appDbContext.Entry(professor).CurrentValues.SetValues(newProfessorData);
            _appDbContext.SaveChanges();
            return true;
        }

        public bool DeleteProfessor(int professorId)
        {
            Professor? professor = _appDbContext.Professors.FirstOrDefault((professor) => professor.Id == professorId);
            if (professor == null)
            {
                return false;
            }
            
            _appDbContext.Professors.Remove(professor);
            _appDbContext.SaveChanges();
            return true;
            
        }
    }
}
