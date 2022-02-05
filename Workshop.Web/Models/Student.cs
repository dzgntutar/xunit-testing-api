using System.ComponentModel.DataAnnotations;

namespace Workshop.Web.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Department { get; set; }
    }
}
