using TrubyiPakety.Models;

namespace TrubyiPakety.ViewModel
{
    public class AddPipesToPackageViewModel
    {
        public Package Package { get; set; }
        public List<Pipe> AvailablePipes { get; set; }
        public List<int> SelectedPipeIds { get; set; } = new List<int>();
    }
}
