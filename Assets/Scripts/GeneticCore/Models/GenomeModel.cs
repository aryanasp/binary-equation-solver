
namespace GeneticCore
{
    public class GenomeModel
    {
        public string Tag;
        public int Value;
        
        public GenomeModel(string tag, int value)
        {
            Tag = tag;
            Value = value;
        }

        public void AddValue()
        {
            Value *= 1000;
        }
    }
}
