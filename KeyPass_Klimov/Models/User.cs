using System.ComponentModel.DataAnnotations;

namespace KeyPass_Klimov.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime? LastAuth { get; set; }
    }
}
