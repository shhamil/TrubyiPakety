using TrubyiPakety.Models;

namespace TrubyiPakety.ViewModel
{
    public class PipeIndexViewModel
    {
        public IEnumerable<Pipe> Pipes { get; set; }
        public int TotalCount { get; set; }
        public int GoodCount { get; set; }
        public int DefectiveCount { get; set; }
        public double TotalWeight { get; set; }
    }
}
