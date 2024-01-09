namespace PrMediii.Modules
{
    public class Trainer
    {
        public int ID { get; set; }
        public string TrainerName { get; set; }
        public string TrnCourse { get; set; }
        public ICollection<Module>? Modules { get; set; }
    }
}
