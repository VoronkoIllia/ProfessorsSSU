using ProfessorsSSU.Interfaces;
using ProfessorsSSU.Models;
using System.Windows;

namespace ProfessorsSSU
{
    public partial class LogInForm : Window
    {
        private readonly IAuthService _authService;
        private readonly Action AfterSuccessAuthAction;

        public LogInForm(IAuthService authService, Action afterSuccessAuthAction)
        {
            InitializeComponent();

            this.Title = "Авторизація";

            this._authService = authService;
            this.AfterSuccessAuthAction = afterSuccessAuthAction;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            // зчитуємо облікові дані користувача
            Editor editor = new Editor 
            {
                Name = this.LoginTextBox.Text,
                Password = this.PasswordTextBox.Password
            };

            try 
            {
                bool authResult = _authService.CheckIfEditorExists(editor);

                if (authResult)
                {
                    MessageBox.Show("Авторизація пройшла успішно!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.AfterSuccessAuthAction();
                    this.Close();
                }
                else 
                {
                    MessageBox.Show("Некоректний логін або пароль!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } 
            catch 
            {
                // у випадку помилки з'єднання виводимо відповідне повідомлення
                MessageBox.Show("Файл professors.db переміщено або пожкоджено. Помістіть цей файл у директорію з додатком.", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool ValidateFields() 
        {
            return !string.IsNullOrEmpty(this.LoginTextBox.Text) && !string.IsNullOrEmpty(this.PasswordTextBox.Password);
        }
    }
}
