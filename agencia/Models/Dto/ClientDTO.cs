using System.ComponentModel.DataAnnotations;

namespace agencia.Models.Dto
{
    public class ClientDTO
    {
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
        public String nacionality { get; set; }
        public DateOnly birthDate { get; set; }
    }
}
