using ProfessorsSSU.Data;
using ProfessorsSSU.Interfaces;
using ProfessorsSSU.Models;

namespace ProfessorsSSU.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        public AuthService(AppDbContext appDbContext) 
        {
            this._appDbContext = appDbContext;
        }

    
        public bool CheckIfEditorExists(Editor verifiedEditor)
        {
            Editor? result = _appDbContext.Editors.FirstOrDefault(editor => editor.Name == verifiedEditor.Name && editor.Password == verifiedEditor.Password);
            return result != null;
        }
    }
}
