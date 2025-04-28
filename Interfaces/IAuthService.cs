using ProfessorsSSU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfessorsSSU.Interfaces
{
    public interface IAuthService
    {
        bool CheckIfEditorExists(Editor editor);
    }
}
