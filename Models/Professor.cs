namespace ProfessorsSSU.Models
{
    public class Professor
    {
        public int Id { get; set; }
        public required string Surname { get; set; }
        public required string DepartmentName { get; set; }
        public int BirthYear { get; set; }
        public int EmploymentYear { get; set;}
        public required string Position { get; set; }
        public required string AcademicDegree { get; set; }
        public string? AcademicRank { get; set; }

    }
}
