using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProfessorsSSU.Interfaces;
using ProfessorsSSU.Models;
using Microsoft.Office.Interop.Word;
using System.IO;


namespace ProfessorsSSU.Services
{
    public class WordService : IWordService
    {
        private Application wordApp;
        private Document wordDoc;

        public void SaveProfessorsToWordFile(string path, List<Professor> professors)
        {
            try 
            {
                wordApp = new Application();
                wordDoc = wordApp.Documents.Add(Directory.GetCurrentDirectory() + "\\Відібрані_дані_про_викладачів.dotx");
                wordApp.Visible = true;
            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message + char.ConvertFromUtf32(13) + "Помістіть файл Відібрані_дані_про_викладачів.dot"+ char.ConvertFromUtf32(13)+"у каталог з exe-файлом і повторіть збереження");
            }
            
            WriteProfessorsToTable(professors, 1);
            
            try
            {
                wordDoc.SaveAs2(path);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + char.ConvertFromUtf32(13)+"Помилка збереження даних");
            }
        }
        private void WriteProfessorsToTable(List<Professor> professors, int numTable)
        {
            for(int i = 0; i < professors.Count; i++) 
            {
                wordDoc.Tables[numTable].Rows.Add();

                wordDoc.Tables[numTable].Cell(2 + i, 1).Range.Text = professors[i].Id.ToString();
                wordDoc.Tables[numTable].Cell(2 + i, 2).Range.Text = professors[i].Surname;
                wordDoc.Tables[numTable].Cell(2 + i, 3).Range.Text = professors[i].DepartmentName;
                wordDoc.Tables[numTable].Cell(2 + i, 4).Range.Text = professors[i].BirthYear.ToString();
                wordDoc.Tables[numTable].Cell(2 + i, 5).Range.Text = professors[i].EmploymentYear.ToString();
                wordDoc.Tables[numTable].Cell(2 + i, 6).Range.Text = professors[i].Position;
                wordDoc.Tables[numTable].Cell(2 + i, 7).Range.Text = professors[i].AcademicDegree;
                wordDoc.Tables[numTable].Cell(2 + i, 8).Range.Text = professors[i].AcademicRank == null ? "немає" : professors[i].AcademicRank;
            }
        }
        ~WordService() 
        {
            if (this.wordDoc != null)
            {
                wordDoc.Close(WdSaveOptions.wdPromptToSaveChanges);
            }
            if (wordApp != null)
            {
                wordApp.Quit(WdSaveOptions.wdPromptToSaveChanges);
            }
        }
    }
}
