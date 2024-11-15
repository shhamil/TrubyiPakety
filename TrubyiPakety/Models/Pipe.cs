using System.ComponentModel.DataAnnotations;

namespace TrubyiPakety.Models
{
    public class Pipe
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Укажите номер трубы")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Укажите качество трубы")]
        public string Quality { get; set; }

        [Required(ErrorMessage = "Укажите марку стали")]
        public string SteelGrade { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Диаметр не должен быть меньше 0.1")]
        public double Diameter { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Длина не должна быть меньше 0.1")]
        public double Length { get; set; }

        [Range(0.1, double.MaxValue, ErrorMessage = "Вес не должен быть меньше 0.1")]
        public double Weight { get; set; }

        public int? PackageId { get; set; }
        public Package? Package { get; set; }
    }
}
