
namespace GeneticAlgorithm.Core
{
    public class GenomeModel
    {
        public string Tag;
        public short Value;
        
        public GenomeModel(string tag, short value)
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
