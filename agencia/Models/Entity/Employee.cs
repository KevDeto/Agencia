using System.ComponentModel.DataAnnotations.Schema;

namespace agencia.Models.Entity
{
    public class Employee : Person
    {
        public String position { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal salary { get; set; }
    }
}
