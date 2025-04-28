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
using ProfessorsSSU.Models;
using ProfessorsSSU.Interfaces;

namespace ProfessorsSSU
{
    /// <summary>
    /// Логика взаимодействия для EditProfessorForm.xaml
    /// </summary>
    public partial class EditProfessorForm : Window
    {
        private readonly IProfessorService _professorService;
        private readonly Professor? editingProfessor;

        private readonly List<string> AcademicDegreeOptions = ["Доктор філософії", "Доктор наук"];
        private readonly List<string> AcademicRankOptions = ["немає", "Старший дослідник", "Доцент", "Професор"];

        public EditProfessorForm(IProfessorService professorService, Professor? editingProfessor = null)
        {
            InitializeComponent();

            this._professorService = professorService;
            this.editingProfessor = editingProfessor;

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
    }
}
