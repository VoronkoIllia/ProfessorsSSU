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

            this.EditProfessorButton.IsEnabled = false;
            this.DeleteProfessorButton.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }


        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            LogInForm loginForm = new LogInForm();
            loginForm.Show();
        }

        private void AddProfessorButton_Click(object sender, RoutedEventArgs e)
        {
            EditProfessorForm editProfessorForm = new EditProfessorForm(_professorService);
            editProfessorForm.Show();
        }

        private void EditProfessorButton_Click(object sender, RoutedEventArgs e)
        {
            Professor p = (Professor)this.ProfessorListDG.SelectedItem;
            EditProfessorForm editProfessorForm = new EditProfessorForm(_professorService, (Professor)this.ProfessorListDG.SelectedItem);
            editProfessorForm.Show();
        }

        //потрібно для того, щоб користувач випадково не натиснув Редагувати чи Видалити
        //коли викладача ще не обрано
        private void ProfessorListDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.EditProfessorButton.IsEnabled = this.ProfessorListDG.SelectedItem != null;
            this.DeleteProfessorButton.IsEnabled = this.ProfessorListDG.SelectedItem != null;   
        }

        private void RefreshDataGrid() 
        {
            this.ProfessorListDG.ItemsSource = _professorService.SelectProfessors();
        }
    }
}