using ProfessorsSSU.Interfaces;
using System.Windows;
using System.Windows.Controls;
using ProfessorsSSU.Models;
using System.Collections.ObjectModel;

namespace ProfessorsSSU
{
    /// <summary>
    /// Interaction logic for InfoProfessorForm.xaml
    /// </summary>
    public partial class InfoProfessorForm : Window
    {
        private readonly IProfessorService _professorService;
        private readonly IAuthService _authService;

        private ObservableCollection<ComboBoxItem> hasAcademicRankOptions = 
            [
                new ComboBoxItem{Display = "Усі", Value = null},
                new ComboBoxItem{Display = "З вченим званням", Value = true},
                new ComboBoxItem{Display = "Без вченого звання", Value = false},
            ];
        private class ComboBoxItem 
        {
            public required string Display { get; set; }
            public bool? Value { get; set; }
        }
        public InfoProfessorForm(IProfessorService professorService, IAuthService authService)
        {
            InitializeComponent();

            this._professorService = professorService;
            this._authService = authService;

            this.HasAcademicRankComboBox.ItemsSource = hasAcademicRankOptions;
            this.HasAcademicRankComboBox.SelectedValue = null;

            this.EditProfessorButton.IsEnabled = false;
            this.DeleteProfessorButton.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = "Інформація про викладачів";
            RefreshDataGrid();
        }

        private void Filter_Changed(object sender, RoutedEventArgs e)
        {
            RefreshDataGrid();
        }

        private void AuthButton_Click(object sender, RoutedEventArgs e)
        {
            LogInForm loginForm = new LogInForm(_authService);
            loginForm.Show();
        }

        private void AddProfessorButton_Click(object sender, RoutedEventArgs e)
        {
            EditProfessorForm editProfessorForm = new EditProfessorForm(_professorService, this.RefreshDataGrid);
            editProfessorForm.ShowDialog();
        }

        private void EditProfessorButton_Click(object sender, RoutedEventArgs e)
        {
            EditProfessorForm editProfessorForm = new EditProfessorForm(_professorService, this.RefreshDataGrid, (Professor)this.ProfessorListDG.SelectedItem);
            editProfessorForm.ShowDialog();
        }

        private void DeleteProfessorButton_Click(object sender, RoutedEventArgs e)
        {
            Professor deletingProfessor = (Professor)this.ProfessorListDG.SelectedItem;
            MessageBoxResult result = MessageBox.Show($"Ви точно бажаєте видалити викладача {deletingProfessor.Surname}?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            // якщо користувач натиснув "Ні" - завершуємо роботу функції
            if (result == MessageBoxResult.No)
            {
                return;
            }
            
            try 
            {
                bool isDeletingSuccessfully = _professorService.DeleteProfessor(deletingProfessor.Id);
                if (!isDeletingSuccessfully)
                { 
                    MessageBox.Show("Видалення неможливе! Викладач відсутній в БД.", "Помилка", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                }
            }
            catch 
            {
                MessageBox.Show("Файл professors.db переміщено або пожкоджено. Помістіть цей файл у директорію з додатком.", "Помилка", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                return;
            }
            RefreshDataGrid();
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
            // отримуємо параметри фільтрації
            bool? hasAcademicRank = ((ComboBoxItem)this.HasAcademicRankComboBox.SelectedItem).Value;
            bool onlyPensioners = (bool)this.OnlyPensionersCheckbox.IsChecked;
           
            
            try 
            {
                //виконуємо фільтрацію
                List<Professor> professors = _professorService.SelectProfessors(hasAcademicRank, onlyPensioners);
                this.ProfessorListDG.ItemsSource = professors;
                if (professors.Count == 0)
                {
                    // виводимо повідомлення у тому випадку
                    // коли потрібних викладачів немає в БД
                    this.MessageAboutData.Visibility = Visibility.Visible;
                    this.MessageAboutData.Content = "Даних про викладачів поки що немає";
                }
                else 
                {
                    // ховаємо повідомлення, якщо знайдено хоча б одного потрібного викладача
                    this.MessageAboutData.Visibility = Visibility.Hidden;
                }
            } 
            catch 
            {
                // у випадку помилки з'єднання виводимо відповідне повідомлення
                MessageBox.Show("Файл professors.db переміщено або пожкоджено. Помістіть цей файл у директорію з додатком.", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}