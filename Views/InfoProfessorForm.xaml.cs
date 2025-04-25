using ProfessorsSSU.Interfaces;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProfessorsSSU.Models;
using System.Collections.ObjectModel;

namespace ProfessorsSSU
{
    /// <summary>
    /// Interaction logic for InfoProfessorForm.xaml
    /// </summary>
    public partial class InfoProfessorForm : Window
    {
        private IProfessorService _professorService;

        private ObservableCollection<ComboBoxItem> hasAcademicRankOptions = 
            [
                new ComboBoxItem{Display = "Усі", Value = null},
                new ComboBoxItem{Display = "З вченим званням", Value = "True"},
                new ComboBoxItem{Display = "Без вченого звання", Value = "False"},
            ];
        private class ComboBoxItem 
        {
            public required string Display { get; set; }
            public string? Value { get; set; }
        }
        public InfoProfessorForm(IProfessorService professorService)
        {
            InitializeComponent();
            _professorService = professorService;
            this.HasAcademicRankComboBox.ItemsSource = hasAcademicRankOptions;
            this.HasAcademicRankComboBox.SelectedValue = null;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //try {
            //List<Professor> professors = new();
            //professors.Add(new Professor(){Id = 1, Surname = "Vasff", BirthYear = 2001, EmploymentYear = 2023, AcademicRank="hui", AcademicDegree = "AAAA", DepartmentName = "dsdsdsdd", Position = "dsdsdsds" });
            ProfessorListDG.ItemsSource = _professorService.SelectProfessors();
            //ProfessorListDG.ItemsSource = professors;
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }


        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            LogInForm loginForm = new LogInForm();
            loginForm.Show();
        }

        private void AddProfessorButton_Click(object sender, RoutedEventArgs e)
        {
            EditProfessorForm editProfessorForm = new EditProfessorForm();
            editProfessorForm.Show();
        }

        private void EditProfessorButton_Click(object sender, RoutedEventArgs e)
        {
            EditProfessorForm editProfessorForm = new EditProfessorForm();
            editProfessorForm.Show();
        }
    }
}