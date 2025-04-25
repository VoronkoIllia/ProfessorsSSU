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
    /// <summary>
    /// Логика взаимодействия для EditProfessorForm.xaml
    /// </summary>
    public partial class EditProfessorForm : Window
    {
        private readonly List<string> AcademicDegreeOptions = ["Доктор філософії", "Доктор наук"];
        private readonly List<string> AcademicRankOptions = ["Старший дослідник", "Доцент", "Професор"];
        public EditProfessorForm()
        {
            InitializeComponent();
            this.AcademicDegreeComboBox.ItemsSource = AcademicDegreeOptions;
            this.AcademicRankComboBox.ItemsSource = AcademicRankOptions;
        }
    }
}
