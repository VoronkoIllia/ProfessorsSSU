using ProfessorsSSU.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
