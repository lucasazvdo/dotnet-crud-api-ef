using System.ComponentModel.DataAnnotations;

namespace CRUDTest.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Nome { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public int Idade { get; set; }
    }
}
