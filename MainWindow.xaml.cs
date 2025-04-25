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

namespace ProfessorsSSU
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IProfessorService _professorService;
        public MainWindow(IProfessorService professorService)
        {
            InitializeComponent();
            _professorService = professorService;
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
    }
}