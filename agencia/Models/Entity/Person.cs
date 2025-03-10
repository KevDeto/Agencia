using System.ComponentModel.DataAnnotations;

namespace agencia.Models.Entity
{
    public class Person
    {
        [Key]
        public long Id { get; set; }
        public String name { get; set; }
        public String lastName { get; set; }
        [Required]
        public String dni { get; set; }
        [Required]
        [EmailAddress]
        public String email { get; set; }
        public String telephone { get; set; }
        public String adress { get; set; }
    }
}
