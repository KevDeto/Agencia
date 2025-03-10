using System.ComponentModel.DataAnnotations;

namespace agencia.Models.Dto
{
    public class PersonDTO
    {
        public long Id { get; set; }
        public String name { get; set; }
        public String lastName { get; set; }
        public String dni { get; set; }
        public String email { get; set; }
        public String telephone { get; set; }
        public String adress { get; set; }
    }
}
