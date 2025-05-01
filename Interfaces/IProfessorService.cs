using ProfessorsSSU.Models;


namespace ProfessorsSSU.Interfaces
{
    public interface IProfessorService
    {
        List<Professor> SelectProfessors(bool? hasAcademicRank = null, bool onlyPensioners = false);
        
        void AddNewProfessor(Professor professor);
        bool UpdateProfessor(int professorId, Professor newProfessorData);
        bool DeleteProfessor(int professorId);
    }
}
