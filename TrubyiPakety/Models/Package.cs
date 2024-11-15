using System.ComponentModel.DataAnnotations;

namespace TrubyiPakety.Models
{
    public class Package
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите номер пакета")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Укажите дату создания пакета")]
        public DateTime Date { get; set; }

        public ICollection<Pipe> Pipes { get; set; } = new List<Pipe>();
    }
}
