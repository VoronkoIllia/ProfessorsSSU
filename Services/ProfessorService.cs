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

        public List<Professor> SelectProfessors(bool? hasAcademicRank = null, bool onlyPensioners = false) 
        {
            return _appDbContext.Professors.ToList();
        }

        /// <summary>
        /// видаляє за ідентифікатором викладача з БД  
        /// </summary>
        /// <param name="professorId">Ідентифікатор викладача, якого потрібно видалити</param>
        /// <returns>
        ///     true, якщо видалення пройшло успішно <br/>
        ///     false, якщо викладача з таким ідентифікатором немає в БД
        /// </returns>
        public bool DeleteProfessor(int professorId)
        {
            Professor? professor = _appDbContext.Professors.FirstOrDefault((professor) => professor.Id == professorId);
            if (professor == null)
            {
                return false;
            }
            else 
            {
                _appDbContext.Professors.Remove(professor);
                _appDbContext.SaveChanges();
                return true;
            }
        }
    }
}
