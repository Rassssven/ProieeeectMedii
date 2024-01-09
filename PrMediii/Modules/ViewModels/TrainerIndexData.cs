using System.Security.Policy;

namespace PrMediii.Modules.ViewModels
{
    public class TrainerIndexData
    {
        public IEnumerable<Trainer> Trainers { get; set; }
        public IEnumerable<Module> Modules { get; set; }
    }
}
