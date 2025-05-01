using System.Windows;
using ProfessorsSSU.Models;
using ProfessorsSSU.Interfaces;

namespace ProfessorsSSU
{
    public partial class EditProfessorForm : Window
    {
        private readonly IProfessorService _professorService;
        private readonly Professor? editingProfessor;

        private readonly Action AfterUpdateAction;

        private readonly List<string> AcademicDegreeOptions = ["Доктор філософії", "Доктор наук"];
        private readonly List<string> AcademicRankOptions = ["немає", "Старший дослідник", "Доцент", "Професор"];

        public EditProfessorForm(IProfessorService professorService, Action afterUpdateAction, Professor? editingProfessor = null)
        {
            InitializeComponent();

            this._professorService = professorService;
            this.editingProfessor = editingProfessor;
            this.AfterUpdateAction = afterUpdateAction;

            this.AcademicDegreeComboBox.ItemsSource = AcademicDegreeOptions;
            this.AcademicRankComboBox.ItemsSource = AcademicRankOptions;

            if (this.editingProfessor != null) //якщо форма використовується для редагування
            {
                this.FormHeaderLabel.Content = "Редагування даних про викладача";

                //підтягуємо дані про обраного викладача, якщо форма використовується для редагування
                this.SurnameTextBox.Text = this.editingProfessor.Surname;
                this.DepartmentTextBox.Text = this.editingProfessor.DepartmentName;
                this.BirthYearTextBox.Text = this.editingProfessor.BirthYear.ToString();
                this.EmploymentYearTextBox.Text = this.editingProfessor.EmploymentYear.ToString();
                this.PositionTextBox.Text = this.editingProfessor.Position;
                this.AcademicDegreeComboBox.SelectedIndex = this.AcademicDegreeOptions.IndexOf(this.editingProfessor.AcademicDegree);
                this.AcademicRankComboBox.SelectedIndex = this.editingProfessor.AcademicRank == null ? 0 : this.AcademicRankOptions.IndexOf(this.editingProfessor.AcademicRank);
            }
            else //якщо форма використовується для додавання даних
            {
                this.FormHeaderLabel.Content = "Додавання нового викладача";
            }
            
        }

        //функція, що здійснює валідацію усіх полів форми
        private bool ValidateFields() 
        {
            return !string.IsNullOrEmpty(this.SurnameTextBox.Text.Trim()) && !string.IsNullOrEmpty(this.SurnameTextBox.Text.Trim())
                    && this.BirthYearTextBox.Text.Trim().All(char.IsDigit) && this.EmploymentYearTextBox.Text.Trim().All(char.IsDigit)
                    && !string.IsNullOrEmpty(this.PositionTextBox.Text.Trim()) && this.AcademicDegreeComboBox.SelectedItem != null
                    && this.AcademicRankComboBox.SelectedItem != null;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateFields())
            {
                MessageBox.Show("Поля заповненні некоректно! Переконайтеся, що всі поля не є порожніми, та заповнені в потрібному форматі.", "Помилка введених даних", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // сценарій для оновлення даних у БД після редагування професора 
            if (this.editingProfessor != null)
            {
                //перезаписуємо поля сутності Professor тими даними, що ввів користувач
                this.editingProfessor.Surname = this.SurnameTextBox.Text;
                this.editingProfessor.DepartmentName = this.DepartmentTextBox.Text;
                this.editingProfessor.BirthYear = Convert.ToInt32(this.BirthYearTextBox.Text);
                this.editingProfessor.EmploymentYear = Convert.ToInt32(this.EmploymentYearTextBox.Text);
                this.editingProfessor.Position = this.PositionTextBox.Text;
                this.editingProfessor.AcademicDegree = this.AcademicDegreeComboBox.SelectedItem.ToString();
                this.editingProfessor.AcademicRank = this.AcademicRankComboBox.SelectedItem.ToString();
                
                try
                {
                    bool updateResult = _professorService.UpdateProfessor(this.editingProfessor.Id, this.editingProfessor);
                    if (!updateResult)
                    {
                        MessageBox.Show("Оновлення даних неможливе! Викладач відсутній в БД.", "Помилка", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    }
                }
                catch
                {
                    MessageBox.Show("Файл professors.db переміщено або пожкоджено. Помістіть цей файл у директорію з додатком.", "Помилка", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    return;
                }
            }
            else // сценарій для збереження даних у БД після створення професора
            {
                // створюємо нову сутність Professor та зберігаємо в її поля введені дані про нового викладача
                Professor newProfessor = new Professor 
                {
                    Surname = this.SurnameTextBox.Text,
                    DepartmentName = this.DepartmentTextBox.Text,
                    BirthYear = Convert.ToInt32(this.BirthYearTextBox.Text),
                    EmploymentYear = Convert.ToInt32(this.EmploymentYearTextBox.Text),
                    Position = this.PositionTextBox.Text,
                    AcademicDegree = this.AcademicDegreeComboBox.SelectedItem.ToString(),
                    AcademicRank = this.AcademicRankComboBox.SelectedItem.ToString()
                };
                
                try
                {
                    _professorService.AddNewProfessor(newProfessor);
                }
                catch
                {
                    MessageBox.Show("Файл professors.db переміщено або пожкоджено. Помістіть цей файл у директорію з додатком.", "Помилка", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    return;
                }
            }

            // виконуємо дії, які потрібно виконати для оновлення інтерфейсу після редагування чи додавання
            this.AfterUpdateAction();

            // після успішного виконання усіх операцій закриваємо форму
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}