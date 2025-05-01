using ProfessorsSSU.Interfaces;
using System.Windows;

namespace ProfessorsSSU
{
    public partial class LogInForm : Window
    {
        private readonly IAuthService _authService;

        public LogInForm(IAuthService authService)
        {
            InitializeComponent();
            this._authService = authService;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
