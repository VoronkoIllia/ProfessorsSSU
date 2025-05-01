using ProfessorsSSU.Models;

namespace ProfessorsSSU.Interfaces
{
    public interface IAuthService
    {
        bool CheckIfEditorExists(Editor editor);
    }
}
