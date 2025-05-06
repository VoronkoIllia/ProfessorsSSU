using ProfessorsSSU.Models;


namespace ProfessorsSSU.Interfaces
{
    public interface IWordService
    {
        void SaveProfessorsToWordFile(string path, List<Professor> professors);
    }
}
