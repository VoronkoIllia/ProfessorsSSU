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
            if (!ValidateFields())
            {
                // якщо не всі поля заповнені - повідомляємо користувача про це
                MessageBox.Show("Переконайтеся, що всі поля заповнені!", "Помилка введення даних", MessageBoxButton.OK, MessageBoxImage.Error);
                return; 
            }

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
                    // якщо авторизація пройшла успішно - показуємо інструменти адміністратора
                    // та повідомляємо користувача про успіх
                    MessageBox.Show("Авторизація пройшла успішно!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.AfterSuccessAuthAction();
                    this.Close();
                }
                else 
                {
                    // інакше повідомляємо про некоректні облікові дані
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
