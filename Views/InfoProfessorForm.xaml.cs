using ProfessorsSSU.Interfaces;
using System.Windows;
using System.Windows.Controls;
using ProfessorsSSU.Models;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace ProfessorsSSU
{
    /// <summary>
    /// Interaction logic for InfoProfessorForm.xaml
    /// </summary>
    public partial class InfoProfessorForm : Window
    {
        private readonly IProfessorService _professorService;
        private readonly IAuthService _authService;
        private readonly IWordService _wordService;

        private bool isEditorAuthorized;

        private List<Professor> FilteredProfessors; // список з відібраними викладачами

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
        public InfoProfessorForm(IProfessorService professorService, IAuthService authService, IWordService wordService)
        {
            InitializeComponent();


            this._professorService = professorService;
            this._authService = authService;
            this._wordService = wordService;

            this.HasAcademicRankComboBox.ItemsSource = hasAcademicRankOptions;
            this.HasAcademicRankComboBox.SelectedValue = null;

            this.EditProfessorButton.IsEnabled = false;
            this.DeleteProfessorButton.IsEnabled = false;

            this.isEditorAuthorized = false;
            this.HideEditorTools();
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
            if (!isEditorAuthorized)
            {
                LogInForm loginForm = new LogInForm(_authService, () =>
                    {
                        this.isEditorAuthorized = true;
                        this.ShowEditorTools();
                    }
                );
                loginForm.ShowDialog();
            }
            else 
            {
                this.isEditorAuthorized = false;
                this.HideEditorTools();
                MessageBox.Show("Ви успішно вийшли з облікового запису!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
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
                this.FilteredProfessors = _professorService.SelectProfessors(hasAcademicRank, onlyPensioners);
                this.ProfessorListDG.ItemsSource = this.FilteredProfessors;

                if (this.FilteredProfessors.Count == 0)
                {
                    // виводимо повідомлення у тому випадку
                    // коли потрібних викладачів немає в БД
                    this.MessageAboutData.Visibility = Visibility.Visible;
                    this.MessageAboutData.Content = "Даних про викладачів поки що немає";

                    // блокуємо кнопку "Зберегти в Word"
                    this.SaveToWordButton.IsEnabled = false;
                }
                else 
                {
                    // ховаємо повідомлення, якщо знайдено хоча б одного потрібного викладача
                    this.MessageAboutData.Visibility = Visibility.Hidden;

                    // розблоковуємо кнопку "Зберегти в Word"
                    this.SaveToWordButton.IsEnabled = true;
                }
            } 
            catch 
            {
                // у випадку помилки з'єднання виводимо відповідне повідомлення
                MessageBox.Show("Файл professors.db переміщено або пожкоджено. Помістіть цей файл у директорію з додатком.", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ShowEditorTools() 
        {
            this.AuthButton.Content = "Розлогінитися";
            this.AddProfessorButton.Visibility = Visibility.Visible;
            this.EditProfessorButton.Visibility = Visibility.Visible;
            this.DeleteProfessorButton.Visibility = Visibility.Visible;
        }

        private void SaveToWordButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "Відібрані_викладачі.docx";

            if (saveFileDialog.ShowDialog() == true)
            {
                string saveFilePath = saveFileDialog.FileName; //отримуємо шлях збереження файлу
                
                try 
                {
                    // зберігаємо відібрані дані про викладачів у файл 
                    _wordService.SaveProfessorsToWordFile(saveFilePath, this.FilteredProfessors);
                }
                catch(Exception ex) 
                {
                    // якщо сталася помилка - сповіщаємо користувача через діалогове вікно
                    MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
        }

        private void HideEditorTools()
        {
            this.AuthButton.Content = "Авторизуватися";
            this.AddProfessorButton.Visibility = Visibility.Hidden;
            this.EditProfessorButton.Visibility = Visibility.Hidden;
            this.DeleteProfessorButton.Visibility = Visibility.Hidden;
        }

    }
}